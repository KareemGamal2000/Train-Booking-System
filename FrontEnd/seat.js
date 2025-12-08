// تعريف متغير لتخزين إجمالي المقاعد
let TOTAL_SEATS = 60;

// مقاعد محجوزة بالفعل
let preBooked = []; 
let bookingId = null; 

// **عنوان API (يجب استبداله بعنوان خادمك الحقيقي)**
const BASE_API_URL = 'https://localhost:7192/api/Booking';
const MOCK_MODE = false; // تم تعطيل وضع المحاكاة

const seatsGrid = document.getElementById('seatsGrid');
const selectedSeatElem = document.getElementById('selectedSeat');
const bookBtn = document.getElementById('bookBtn');

// عناصر العرض لبيانات الرحلة
const tripRouteDisplay = document.getElementById('tripRouteDisplay');
const trainNameDisplay = document.getElementById('trainNameDisplay');
const classNameDisplay = document.getElementById('classNameDisplay');
const totalSeatsDisplay = document.getElementById('totalSeatsDisplay');


let selected = null;
// لحفظ بيانات الرحلة المهمة (ID, ClassID, StopIDs, StopNames)
let tripData = {}; 

/**
 * دالة لتهيئة شاشة المقاعد وعرض بيانات الرحلة.
 */
function initSeatSelection() {
    // عرض حالة تسجيل الدخول
    updateUserStatus();
    
    // 1. قراءة البيانات من localStorage
    const trainName = localStorage.getItem('selectedTrainName') || 'N/A';
    const className = localStorage.getItem('selectedClassName') || 'N/A';
    const fromName = localStorage.getItem('searchFromStationName') || 'محطة الانطلاق';
    const toName = localStorage.getItem('searchToStationName') || 'محطة الوصول';
    const classPrice = localStorage.getItem('selectedClassPrice') || 0;
    
    // ** قراءة جميع المعرفات المطلوبة لنداءات الـ API **
    tripData.tripId = parseInt(localStorage.getItem('selectedTripId')) || 0; 
    tripData.classId = parseInt(localStorage.getItem('selectedClassId')) || 0; 
    tripData.departureStopId = parseInt(localStorage.getItem('departureStopId')) || 0; 
    tripData.arrivalStopId = parseInt(localStorage.getItem('arrivalStopId')) || 0; 
    
    // ** تخزين السعر في tripData **
    tripData.classPrice = classPrice; 

    // 2. تحديث بيانات العرض في الصفحة (سيتم تحديث TOTAL_SEATS بعد جلب البيانات)
    tripRouteDisplay.textContent = `رحلة من ${fromName} إلى ${toName}`;
    trainNameDisplay.textContent = trainName;
    classNameDisplay.textContent = className;
    classNameDisplay.textContent += ` (${classPrice} ج.م)`;
    totalSeatsDisplay.textContent = `جاري التحميل...`;

    // 3. التحقق من وجود المعرفات الأساسية قبل المتابعة
    console.log('Trip Data:', tripData);
    
    if (!tripData.tripId || !tripData.classId || !tripData.departureStopId || !tripData.arrivalStopId) {
        console.error('Missing trip data:', {
            tripId: tripData.tripId,
            classId: tripData.classId,
            departureStopId: tripData.departureStopId,
            arrivalStopId: tripData.arrivalStopId
        });
        alert("خطأ: بيانات الرحلة غير مكتملة. يرجى العودة لصفحة البحث.");
        bookBtn.disabled = true;
        bookBtn.textContent = 'بيانات ناقصة';
        return;
    }

    // 4. جلب المقاعد المتاحة من API
    fetchAvailableSeats();
}

/**
 * الخطوة 0: جلب المقاعد المتاحة من الـ localStorage أو API وتحديث شبكة المقاعد.
 */
async function fetchAvailableSeats() {
    const statusMessage = document.createElement('p');
    statusMessage.textContent = 'جاري جلب المقاعد المتاحة...';
    seatsGrid.innerHTML = '';
    seatsGrid.appendChild(statusMessage);

    if (MOCK_MODE) {
        // محاكاة استجابة API (يجب حذف هذا الجزء عند استخدام الـ API الحقيقي)
        await new Promise(resolve => setTimeout(resolve, 800));
        preBooked = [3, 7, 15, 22, 30, 31]; // مقاعد وهمية محجوزة
        buildSeatsGrid();
        statusMessage.remove();
        return;
    }

    try {
        // ** محاولة تحميل البيانات من localStorage أولاً **
        const cachedSeatsData = localStorage.getItem('availableSeatsData');
        let data = null;
        
        if (cachedSeatsData) {
            console.log('Loading available seats from localStorage');
            data = JSON.parse(cachedSeatsData);
            
            // حفظ بيانات المقاعد في متغير عام للاستخدام لاحقاً
            window.availableSeatsCache = data;
        } else {
            // إذا لم تكن البيانات محفوظة، نحاول جلبها من API
            console.log('No cached data, fetching from API');
            const url = `${BASE_API_URL}/available-seats?tripId=${tripData.tripId}&classId=${tripData.classId}&departureStopId=${tripData.departureStopId}&arrivalStopId=${tripData.arrivalStopId}`;
            
            const token = localStorage.getItem('authToken') || '';
            const headers = {
                'Content-Type': 'application/json'
            };
            
            // إضافة Authorization header فقط إذا كان Token موجوداً
            if (token) {
                headers['Authorization'] = `Bearer ${token}`;
            }
            
            const response = await fetch(url, {
                method: 'GET',
                headers: headers
            });

            if (!response.ok) {
                const errorData = await response.json().catch(() => ({}));
                
                // معالجة خاصة لخطأ 401
                if (response.status === 401) {
                    throw new Error('يجب تسجيل الدخول أولاً للحجز. يرجى تسجيل الدخول والمحاولة مرة أخرى.');
                }
                
                throw new Error(errorData.message || 'فشل جلب المقاعد');
            }

            data = await response.json();
            
            // حفظ البيانات في localStorage للاستخدام المستقبلي
            localStorage.setItem('availableSeatsData', JSON.stringify(data));
            window.availableSeatsCache = data;
        }
        
        // تحديث: الاستجابة تحتوي على AvailableSeatsDto
        // البنية: { tripID, classID, totalAvailableSeats, seats: [{seatID, seatNumber, coachID, isAvailable}] }
        
        if (data.seats && Array.isArray(data.seats) && data.seats.length > 0) {
            // تحديد إجمالي المقاعد من أكبر رقم مقعد في البيانات
            const maxSeatNumber = Math.max(...data.seats.map(seat => seat.seatNumber));
            TOTAL_SEATS = maxSeatNumber;
            
            console.log(`Total seats determined from API: ${TOTAL_SEATS}`);
            console.log(`Available seats: ${data.seats.length}`);
            console.log(`Seat numbers range: 1-${TOTAL_SEATS}`);
            
            // جميع المقاعد من 1 إلى TOTAL_SEATS
            const allSeatNumbers = Array.from({length: TOTAL_SEATS}, (_, i) => i + 1);
            
            // المقاعد المتاحة من API
            const availableSeatNumbers = data.seats.map(seat => seat.seatNumber);
            
            // المقاعد المحجوزة = كل المقاعد - المقاعد المتاحة
            preBooked = allSeatNumbers.filter(num => !availableSeatNumbers.includes(num));
            
            console.log(`Pre-booked seats: ${preBooked.length}`);
            
            // تحديث العرض
            const availableCount = data.totalAvailableSeats || data.seats.length;
            const bookedCount = TOTAL_SEATS - availableCount;
            totalSeatsDisplay.textContent = `إجمالي ${TOTAL_SEATS} مقعد (${availableCount} متاح، ${bookedCount} محجوز)`;
        } else {
            // إذا لم يكن هناك بيانات، استخدم قيمة افتراضية
            TOTAL_SEATS = 60;
            preBooked = [];
            totalSeatsDisplay.textContent = `إجمالي ${TOTAL_SEATS} مقعد`;
            console.warn('No seats data received, using default: 60 seats');
        }
        
        buildSeatsGrid();
        statusMessage.remove();

    } catch (error) {
        console.error('API Error Details:', {
            message: error.message,
            tripData: tripData,
            url: `${BASE_API_URL}/available-seats?tripId=${tripData.tripId}&classId=${tripData.classId}&departureStopId=${tripData.departureStopId}&arrivalStopId=${tripData.arrivalStopId}`
        });
        
        // التحقق مما إذا كانت المشكلة في المصادقة
        const isAuthError = error.message.includes('تسجيل الدخول') || error.message.includes('401');
        
        statusMessage.innerHTML = `
            <div style="color: #f44336; text-align: center; padding: 20px;">
                <h3>خطأ في جلب المقاعد</h3>
                <p style="font-size: 1.1em; font-weight: bold; margin: 15px 0;">${error.message}</p>
                ${isAuthError ? `
                    <p style="font-size: 0.95em; margin: 15px 0; color: #ff9800;">
                        💡 يرجى تسجيل الدخول أولاً قبل الحجز
                    </p>
                    <button onclick="window.location.href='Login.html'" style="margin: 10px 5px; padding: 10px 25px; background: #4caf50; color: white; border: none; border-radius: 5px; cursor: pointer; font-weight: bold;">
                        تسجيل الدخول
                    </button>
                ` : `
                    <p style="font-size: 0.9em; margin-top: 10px;">
                        يرجى التحقق من:
                        <br>• تشغيل الـ API على https://localhost:7192
                        <br>• صحة بيانات الرحلة
                    </p>
                `}
                <button onclick="window.location.href='Search.html'" style="margin: 10px 5px; padding: 10px 25px; background: #d4af37; border: none; border-radius: 5px; cursor: pointer;">
                    العودة للرحلات
                </button>
            </div>
        `;
        bookBtn.disabled = true;
    }
}


/**
 * بناء شبكة المقاعد في الواجهة الأمامية.
 */
function buildSeatsGrid() {
    seatsGrid.innerHTML = ''; 
    
    // تحديد عدد الأعمدة (للتنسيق)
    let columns = 6;
    if (TOTAL_SEATS <= 30) {
        columns = 4;
    } else if (window.innerWidth < 900) {
        columns = 4;
    }

    seatsGrid.style.gridTemplateColumns = `repeat(${columns}, 1fr)`;

    for (let i = 1; i <= TOTAL_SEATS; i++) {
        const btn = document.createElement('button');
        btn.className = 'seat';
        btn.type = 'button';
        btn.setAttribute('data-seat', i);
        btn.setAttribute('aria-label', `مقعد رقم ${i}`);
        btn.innerHTML = `<span class="seat-number">${i}</span>`;

        // تمييز المقاعد المحجوزة
        if (preBooked.includes(i)) {
            btn.classList.add('booked');
            btn.disabled = true;
            btn.setAttribute('aria-disabled', 'true');
        }

        // معالج حدث النقر لاختيار المقعد
        btn.addEventListener('click', () => {
    
            if (btn.classList.contains('booked')) return;

            const prev = seatsGrid.querySelector('.seat.selected');
            if (prev) prev.classList.remove('selected');

     
            btn.classList.add('selected');
            selected = i;
            selectedSeatElem.textContent = selected;
            bookBtn.disabled = false;
        });


        // معالج حدث الضغط على المفاتيح (لإمكانية الوصول)
        btn.addEventListener('keydown', (e) => {
            if (e.key === 'Enter' || e.key === ' ') {
                e.preventDefault();
                btn.click();
            }
        });

        seatsGrid.appendChild(btn);
    }
}


/**
 * فتح modal تفاصيل الحجز (بعد إنشاء الحجز)
 */
function showBookingModal() {
    if (!selected || !bookingId) return;
    
    // ملء تفاصيل الحجز في المودال
    const trainName = localStorage.getItem('selectedTrainName') || 'N/A';
    const className = localStorage.getItem('selectedClassName') || 'N/A';
    const fromStation = localStorage.getItem('searchFromStationName') || 'محطة الانطلاق';
    const toStation = localStorage.getItem('searchToStationName') || 'محطة الوصول';
    const classPrice = localStorage.getItem('selectedClassPrice') || 0;
    
    // عرض رقم الحجز إذا كان متوفراً
    const bookingRef = bookingId.toString().substring(0, 8).toUpperCase();
    
    document.getElementById('modal-train-name').textContent = trainName;
    document.getElementById('modal-from-station').textContent = fromStation;
    document.getElementById('modal-to-station').textContent = toStation;
    document.getElementById('modal-class-name').textContent = className;
    document.getElementById('modal-seat-number').textContent = selected;
    document.getElementById('modal-total-price').textContent = `${classPrice} ج.م`;
    
    // إظهار رقم الحجز في العنوان
    const modalTitle = document.querySelector('.modal-header h2');
    modalTitle.innerHTML = `تم تأكيد الحجز! 🎉<br><small style="font-size: 14px; opacity: 0.9;">رقم الحجز: ${bookingRef}</small>`;
    
    // إظهار المودال
    const modal = document.getElementById('bookingConfirmModal');
    modal.style.display = 'flex';
    
    // إضافة مستمع لإغلاق المودال عند الضغط خارجه
    modal.onclick = function(event) {
        if (event.target === modal) {
            closeBookingModal();
        }
    };
    
    // إضافة مستمع لإغلاق المودال عند الضغط على Escape
    document.addEventListener('keydown', handleEscapeKey);
    
    // منع التمرير في الخلفية
    document.body.style.overflow = 'hidden';
}

/**
 * معالج مفتاح Escape
 */
function handleEscapeKey(event) {
    if (event.key === 'Escape') {
        closeBookingModal();
    }
}

/**
 * إغلاق modal تفاصيل الحجز
 */
function closeBookingModal() {
    const modal = document.getElementById('bookingConfirmModal');
    modal.style.display = 'none';
    
    // إزالة مستمع مفتاح Escape
    document.removeEventListener('keydown', handleEscapeKey);
    
    // السماح بالتمرير مرة أخرى
    document.body.style.overflow = 'auto';
    
    // إعادة تحميل المقاعد لتحديث الحالة
    if (bookingId) {
        // مسح الاختيار الحالي
        selected = null;
        selectedSeatElem.textContent = '—';
        bookBtn.disabled = true;
        bookBtn.textContent = 'احجز المقعد';
        
        // مسح بيانات المقاعد المحفوظة وإعادة جلبها
        localStorage.removeItem('availableSeatsData');
        fetchAvailableSeats();
    }
}

/**
 * المتابعة للدفع من المودال
 */
async function confirmAndProceedToPayment() {
    const confirmBtn = document.getElementById('confirmBookingBtn');
    const originalText = confirmBtn.textContent;
    
    confirmBtn.disabled = true;
    confirmBtn.classList.add('loading');
    confirmBtn.textContent = 'جاري بدء عملية الدفع...';
    
    // إضافة overlay للمودال لمنع التفاعل أثناء المعالجة
    const modalContent = document.querySelector('.modal-content');
    modalContent.style.pointerEvents = 'none';
    modalContent.style.opacity = '0.7';
    
    try {
        // الحصول على السعر من localStorage
        const classPrice = parseFloat(localStorage.getItem('selectedClassPrice')) || 0;
        
        if (!bookingId) {
            throw new Error('معرف الحجز غير موجود');
        }
        
        if (classPrice <= 0) {
            throw new Error('السعر غير صحيح');
        }
        
        // إنشاء طلب الدفع
        const paymentRequest = {
            bookingID: bookingId,
            amount: classPrice,
            paymentMethod: "Card"
        };
        
        console.log('=== Payment Initiation ===');
        console.log('Booking ID:', bookingId);
        console.log('Amount:', classPrice, 'EGP');
        console.log('Payment Request:', paymentRequest);
        
        // استدعاء API الدفع
        const PAYMENT_API_URL = 'https://localhost:7192/api/Payment';
        const token = localStorage.getItem('authToken');
        
        console.log('API URL:', `${PAYMENT_API_URL}/initiate`);
        console.log('Token present:', !!token);
        
        const response = await fetch(`${PAYMENT_API_URL}/initiate`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(paymentRequest)
        });
        
        if (!response.ok) {
            const errorData = await response.json().catch(() => ({}));
            throw new Error(errorData.message || `فشل بدء عملية الدفع (${response.status})`);
        }
        
        const paymentResult = await response.json();
        console.log('Payment initiated successfully:', paymentResult);
        
        // التحقق من نجاح العملية
        if (paymentResult.success === false) {
            throw new Error(paymentResult.message || 'فشل بدء عملية الدفع');
        }
        
        // حفظ بيانات الدفع
        localStorage.setItem('currentBookingId', bookingId);
        localStorage.setItem('selectedSeatNumber', selected);
        localStorage.setItem('paymentData', JSON.stringify(paymentResult));
        
        // مسح بيانات المقاعد المخزنة مؤقتاً
        localStorage.removeItem('availableSeatsData');
        
        // التحقق من وجود رابط الدفع
        if (paymentResult.paymentUrl || paymentResult.iframeUrl || paymentResult.url) {
            // التوجه لرابط الدفع الخارجي (Paymob)
            const paymentUrl = paymentResult.paymentUrl || paymentResult.iframeUrl || paymentResult.url;
            console.log('Redirecting to payment gateway:', paymentUrl);
            confirmBtn.textContent = 'جاري التحويل لبوابة الدفع...';
            
            // إغلاق المودال قبل التحويل
            closeBookingModal();
            
            // التحويل لبوابة الدفع
            window.location.href = paymentUrl;
        } else {
            // الانتقال إلى صفحة ملخص الحجز
            console.log('No payment URL found, redirecting to summary');
            confirmBtn.textContent = 'جاري التحويل...';
            window.location.href = 'BookingSummary.html';
        }
        
    } catch (error) {
        console.error('Payment Error:', error);
        
        // إعادة تفعيل المودال
        const modalContent = document.querySelector('.modal-content');
        modalContent.style.pointerEvents = 'auto';
        modalContent.style.opacity = '1';
        
        confirmBtn.disabled = false;
        confirmBtn.classList.remove('loading');
        confirmBtn.textContent = originalText;
        
        // معالجة خاصة لخطأ المصادقة
        if (error.message.includes('401') || error.message.includes('مصرح') || error.message.includes('Unauthorized')) {
            alert('انتهت جلستك. يرجى تسجيل الدخول مرة أخرى.');
            localStorage.removeItem('authToken');
            window.location.href = 'Login.html';
        } else {
            alert(`خطأ في عملية الدفع: ${error.message}\n\nيمكنك المحاولة مرة أخرى أو إغلاق النافذة.`);
        }
    }
}

/**
 * دالة رئيسية لتنفيذ عملية الحجز - إنشاء الحجز أولاً ثم عرض المودال.
 */
async function processBooking() {
    if (!selected) return;

    // التحقق من تسجيل الدخول قبل الحجز
    const token = localStorage.getItem('authToken');
    console.log('Token check:', {
        exists: !!token,
        length: token ? token.length : 0,
        preview: token ? token.substring(0, 20) + '...' : 'null'
    });
    
    if (!token || token.trim() === '') {
        const confirmLogin = confirm('يجب تسجيل الدخول أولاً للحجز.\n\nهل تريد الانتقال لصفحة تسجيل الدخول؟');
        if (confirmLogin) {
            // حفظ البيانات الحالية للعودة بعد تسجيل الدخول
            localStorage.setItem('pendingSeatSelection', selected);
            window.location.href = 'Login.html';
        }
        return;
    }

    // تعطيل زر الحجز وعرض رسالة تحميل
    bookBtn.disabled = true;
    bookBtn.textContent = 'جاري إنشاء الحجز...';

    try {
        // إنشاء الحجز أولاً
        await createBooking();
        
        // إذا نجح الحجز، عرض المودال بالتفاصيل
        showBookingModal();
        
        // إعادة تفعيل الزر
        bookBtn.textContent = 'احجز المقعد';
        bookBtn.disabled = false;
        
    } catch (error) {
        console.error('Booking Error:', error);
        
        // معالجة خاصة لخطأ المصادقة
        if (error.message.includes('الجلسة') || error.message.includes('تسجيل الدخول')) {
            const confirmLogin = confirm(`${error.message}\n\nهل تريد الانتقال لصفحة تسجيل الدخول؟`);
            if (confirmLogin) {
                localStorage.setItem('pendingSeatSelection', selected);
                window.location.href = 'Login.html';
                return;
            }
        } else {
            alert(`فشل الحجز: ${error.message}`);
        }
        
        bookBtn.textContent = 'احجز المقعد';
        bookBtn.disabled = false;
    }
}

/**
 * إنشاء الحجز (يتم استدعاءها قبل عرض المودال)
 */
async function createBooking() {
    // التحقق من صحة البيانات قبل الإرسال
    console.log('Raw trip data before booking:', {
        tripId: tripData.tripId,
        classId: tripData.classId,
        departureStopId: tripData.departureStopId,
        arrivalStopId: tripData.arrivalStopId,
        selected: selected
    });
    
    // التحقق من وجود جميع القيم المطلوبة
    if (!tripData.tripId || tripData.tripId === 0) {
        throw new Error("معرف الرحلة غير صحيح. يرجى العودة لصفحة البحث.");
    }
    if (!tripData.classId || tripData.classId === 0) {
        throw new Error("معرف الدرجة غير صحيح. يرجى العودة لصفحة البحث.");
    }
    if (!tripData.departureStopId || tripData.departureStopId === 0) {
        throw new Error("محطة المغادرة غير صحيحة. يرجى العودة لصفحة البحث.");
    }
    if (!tripData.arrivalStopId || tripData.arrivalStopId === 0) {
        throw new Error("محطة الوصول غير صحيحة. يرجى العودة لصفحة البحث.");
    }
        
        // الحصول على معرف المقعد الفعلي من البيانات المحفوظة
        let seatID = null;
        
        // محاولة الحصول على SeatID من البيانات المحفوظة
        if (window.availableSeatsCache && window.availableSeatsCache.seats) {
            const selectedSeat = window.availableSeatsCache.seats.find(seat => seat.seatNumber === selected);
            if (selectedSeat) {
                seatID = selectedSeat.seatID;
                console.log(`Found seat ID ${seatID} for seat number ${selected}`);
            }
        }
        
        // إذا لم نجد معرف المقعد في الكاش، نحاول جلبه من localStorage
        if (!seatID) {
            const cachedData = localStorage.getItem('availableSeatsData');
            if (cachedData) {
                const seatsData = JSON.parse(cachedData);
                const selectedSeat = seatsData.seats?.find(seat => seat.seatNumber === selected);
                if (selectedSeat) {
                    seatID = selectedSeat.seatID;
                    console.log(`Found seat ID ${seatID} from localStorage for seat number ${selected}`);
                }
            }
        }
        
        if (!seatID) {
            throw new Error(`لم يتم العثور على معرف المقعد رقم ${selected}. يرجى تحديث الصفحة والمحاولة مرة أخرى.`);
        }
        
        // إنشاء الحجز مباشرة مع المقاعد المختارة (POST /api/Booking/create)
        const createDto = {
            tripID: parseInt(tripData.tripId),
            classID: parseInt(tripData.classId),
            departureStopID: parseInt(tripData.departureStopId),
            arrivalStopID: parseInt(tripData.arrivalStopId),
            numberOfSeats: 1,
            selectedSeatIDs: [parseInt(seatID)] // استخدام SeatID الفعلي
        };
        
        console.log('Creating booking with:', createDto);
        console.log('Request will be sent to:', `${BASE_API_URL}/create`);
        const bookingResponse = await apiCall(`${BASE_API_URL}/create`, 'POST', createDto);
        console.log('Booking created:', bookingResponse);
        
        // الحصول على معرف الحجز من الاستجابة
        bookingId = bookingResponse.bookingID || bookingResponse.bookingId; 

        if (!bookingId) {
            console.error('No booking ID in response:', bookingResponse);
            throw new Error("فشل إنشاء الحجز.");
        }
        
        console.log('Booking created successfully with ID:', bookingId);

        // حفظ معرف الحجز في localStorage للاستخدام لاحقاً
        localStorage.setItem('currentBookingId', bookingId);
        localStorage.setItem('selectedSeatNumber', selected);
        
        // الحجز تم بنجاح - سيتم عرض المودال بعد ذلك
}


/**
 * دالة مساعدة لعمل نداءات API مع معالجة الأخطاء.
 */
async function apiCall(url, method = 'GET', body = null) {
    if (MOCK_MODE) {
        // محاكاة لعملية API
        await new Promise(resolve => setTimeout(resolve, 500));
        if (url.includes('create')) {
            // محاكاة معرف حجز صالح
            return { bookingId: `MOCK-${Date.now()}` }; 
        }
        if (url.includes('select-seats') || url.includes('confirm')) {
            return { message: "Success" };
        }
        return { message: "Mock response" }; 
    }
    
    // يجب توفير الـ Token هنا إذا كان الـ Endpoint يتطلب Authorize
    const token = localStorage.getItem('authToken') || ''; 
    
    const options = {
        method: method,
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}` 
        }
    };

    if (body) {
        options.body = JSON.stringify(body);
    }

    console.log('API Request:', { url, method, body });
    
    const response = await fetch(url, options);

    console.log('API Response Status:', response.status, response.statusText);

    let data;
    try {
        data = await response.json();
        console.log('API Response Data:', data);
    } catch (parseError) {
        // في حال كانت الاستجابة فارغة (مثل POST confirm)
        console.error('Failed to parse JSON response:', parseError);
        data = { message: response.statusText || 'حدث خطأ غير معروف' };
    }

    if (!response.ok) {
        // معالجة خاصة لخطأ 401
        if (response.status === 401) {
            localStorage.removeItem('authToken'); // حذف Token غير الصالح
            throw new Error('انتهت صلاحية الجلسة. يرجى تسجيل الدخول مرة أخرى.');
        }
        
        // معالجة خاصة لخطأ 400 (Bad Request)
        if (response.status === 400) {
            const errorMsg = data.message || 'بيانات الطلب غير صحيحة';
            const details = data.details ? `\nالتفاصيل: ${data.details}` : '';
            console.error('400 Bad Request:', errorMsg, details);
            throw new Error(errorMsg + details);
        }
        
        // معالجة خاصة لخطأ 500 (Internal Server Error)
        if (response.status === 500) {
            const errorMsg = data.message || 'حدث خطأ في الخادم';
            const details = data.details || '';
            console.error('500 Internal Server Error:', {
                message: errorMsg,
                details: details,
                fullResponse: data
            });
            throw new Error(errorMsg + (details ? `\nالتفاصيل: ${details}` : ''));
        }
        
        console.error('HTTP Error:', response.status, data);
        throw new Error(data.message || `HTTP Error ${response.status}`);
    }

    return data;
}


/**
 * تحديث عرض حالة المستخدم (مسجل دخول أم لا)
 */
function updateUserStatus() {
    const userStatusElement = document.getElementById('userStatus');
    if (!userStatusElement) return;
    
    const token = localStorage.getItem('authToken');
    const userEmail = localStorage.getItem('userEmail');
    const username = localStorage.getItem('username');
    
    if (token && token.trim() !== '') {
        const displayName = username || userEmail || 'مستخدم';
        userStatusElement.innerHTML = `
            <span style="color: #4caf50;">✓</span> 
            مرحباً، ${displayName}
        `;
        userStatusElement.style.background = 'rgba(76, 175, 80, 0.2)';
    } else {
        userStatusElement.innerHTML = `
            <span style="color: #ff9800;">⚠</span> 
            <a href="Login.html" style="color: #d4af37; text-decoration: none;">تسجيل الدخول</a>
        `;
        userStatusElement.style.background = 'rgba(255, 152, 0, 0.2)';
    }
}

// معالج حدث زر "احجز المقعد"
bookBtn.addEventListener('click', processBooking);

// جعل الدوال متاحة عالمياً للاستخدام من HTML
window.showBookingModal = showBookingModal;
window.closeBookingModal = closeBookingModal;
window.confirmAndProceedToPayment = confirmAndProceedToPayment;

// عند تحميل النافذة، ابدأ بتهيئة الصفحة
window.addEventListener('load', () => {
    initSeatSelection();
    // تركيز على أول مقعد متاح لسهولة الاستخدام
    const firstAvailable = seatsGrid.querySelector('.seat:not(.booked)');
    if (firstAvailable) firstAvailable.focus();
});