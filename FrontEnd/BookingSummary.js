// ========== Constants & Configuration ==========
const BASE_API_URL = 'https://localhost:7192/api';
const BOOKING_API = `${BASE_API_URL}/Booking`;
const PAYMENT_API = `${BASE_API_URL}/Payment`;

// ========== DOM Elements ==========
const loadingIndicator = document.getElementById('loadingIndicator');
const errorMessage = document.getElementById('errorMessage');
const errorText = document.getElementById('errorText');
const summaryContent = document.getElementById('summaryContent');
const payBtn = document.getElementById('payBtn');

// Booking info elements
const bookingReference = document.getElementById('bookingReference');
const bookingStatus = document.getElementById('bookingStatus');
const trainName = document.getElementById('trainName');
const className = document.getElementById('className');

// Route elements
const departureStation = document.getElementById('departureStation');
const departureTime = document.getElementById('departureTime');
const arrivalStation = document.getElementById('arrivalStation');
const arrivalTime = document.getElementById('arrivalTime');
const journeyDuration = document.getElementById('journeyDuration');

// Tickets and total elements
const ticketsContainer = document.getElementById('ticketsContainer');
const ticketCount = document.getElementById('ticketCount');
const totalPrice = document.getElementById('totalPrice');

// ========== Global Variables ==========
let currentBookingData = null;

// ========== Initialization ==========
document.addEventListener('DOMContentLoaded', async () => {
    const bookingId = localStorage.getItem('currentBookingId');
    
    if (!bookingId) {
        showError('لم يتم العثور على معرف الحجز. يرجى البدء من جديد.');
        return;
    }

    await loadBookingSummary(bookingId);
});

// ========== Main Functions ==========

/**
 * جلب وعرض ملخص الحجز
 */
async function loadBookingSummary(bookingId) {
    try {
        showLoading();

        // استدعاء API للحصول على ملخص الحجز
        const summary = await apiCall(`${BOOKING_API}/${bookingId}/summary`, 'GET');
        
        if (!summary) {
            throw new Error('فشل تحميل بيانات الحجز');
        }

        currentBookingData = summary;
        displayBookingSummary(summary);
        
        hideLoading();
        summaryContent.style.display = 'block';

    } catch (error) {
        console.error('Error loading booking summary:', error);
        showError(error.message || 'حدث خطأ أثناء تحميل تفاصيل الحجز');
    }
}

/**
 * عرض بيانات الحجز في الواجهة
 */
function displayBookingSummary(summary) {
    // معلومات الحجز الأساسية
    bookingReference.textContent = summary.bookingReference || summary.bookingID?.substring(0, 8).toUpperCase();
    
    // تحديث حالة الحجز مع التنسيق
    const status = summary.bookingStatus || 'Pending';
    bookingStatus.textContent = getStatusText(status);
    bookingStatus.className = `value status-badge ${status.toLowerCase()}`;
    
    // معلومات القطار والدرجة
    trainName.textContent = summary.trainName || localStorage.getItem('selectedTrainName') || '—';
    className.textContent = summary.className || localStorage.getItem('selectedClassName') || '—';

    // معلومات المحطات
    departureStation.textContent = summary.departureStation || localStorage.getItem('searchFromStationName') || '—';
    arrivalStation.textContent = summary.arrivalStation || localStorage.getItem('searchToStationName') || '—';
    
    // الأوقات
    departureTime.textContent = formatTime(summary.departureTime);
    arrivalTime.textContent = formatTime(summary.arrivalTime);
    
    // حساب المدة
    const duration = calculateDuration(summary.departureTime, summary.arrivalTime);
    journeyDuration.textContent = duration;

    // عرض التذاكر
    displayTickets(summary.tickets || []);

    // المجموع
    ticketCount.textContent = (summary.tickets?.length || 0) + ' تذكرة';
    totalPrice.textContent = (summary.totalPrice || 0).toFixed(2) + ' ج.م';

    // تفعيل زر الدفع
    if (status === 'Pending' || status === 'Confirmed') {
        payBtn.disabled = false;
    }
}

/**
 * عرض التذاكر
 */
function displayTickets(tickets) {
    ticketsContainer.innerHTML = '';

    if (!tickets || tickets.length === 0) {
        ticketsContainer.innerHTML = '<p style="text-align: center; color: var(--text-muted);">لا توجد تذاكر</p>';
        return;
    }

    tickets.forEach((ticket, index) => {
        const ticketDiv = document.createElement('div');
        ticketDiv.className = 'ticket-item';
        ticketDiv.innerHTML = `
            <div class="ticket-info">
                <div class="ticket-detail">
                    <span class="label">رقم التذكرة</span>
                    <span class="value">#${index + 1}</span>
                </div>
                <div class="ticket-detail">
                    <span class="label">رقم المقعد</span>
                    <span class="value">${ticket.seatNumber || ticket.seatID || '—'}</span>
                </div>
                <div class="ticket-detail">
                    <span class="label">الدرجة</span>
                    <span class="value">${ticket.className || '—'}</span>
                </div>
            </div>
            <div class="ticket-price">${(ticket.price || 0).toFixed(2)} ج.م</div>
        `;
        ticketsContainer.appendChild(ticketDiv);
    });
}

/**
 * بدء عملية الدفع
 */
async function initiatePayment() {
    if (!currentBookingData) {
        alert('لا توجد بيانات حجز متاحة');
        return;
    }

    payBtn.disabled = true;
    payBtn.innerHTML = `
        <svg class="btn-icon" viewBox="0 0 24 24" fill="none" stroke="currentColor">
            <circle cx="12" cy="12" r="10"/>
            <path d="M12 6v6l4 2"/>
        </svg>
        جاري المعالجة...
    `;

    try {
        const bookingId = currentBookingData.bookingID;
        
        // طلب بدء عملية الدفع
        const paymentRequest = {
            bookingId: bookingId,
            amount: currentBookingData.totalPrice,
            currency: 'EGP'
        };

        const paymentResponse = await apiCall(`${PAYMENT_API}/initiate`, 'POST', paymentRequest);

        if (paymentResponse && paymentResponse.success) {
            // إذا كان هناك رابط دفع، التوجه إليه
            if (paymentResponse.paymentUrl) {
                window.location.href = paymentResponse.paymentUrl;
            } else if (paymentResponse.iframeToken) {
                // أو عرض iframe للدفع
                displayPaymentIframe(paymentResponse.iframeToken);
            } else {
                // تأكيد الحجز مباشرة إذا لم يكن هناك دفع مطلوب
                await confirmBooking(bookingId);
                alert('تم تأكيد الحجز بنجاح! ✅');
                window.location.href = 'Home.html';
            }
        } else {
            throw new Error(paymentResponse.message || 'فشلت عملية الدفع');
        }

    } catch (error) {
        console.error('Payment error:', error);
        alert(`خطأ في الدفع: ${error.message}`);
        
        payBtn.disabled = false;
        payBtn.innerHTML = `
            <svg class="btn-icon" viewBox="0 0 24 24" fill="none" stroke="currentColor">
                <rect x="2" y="5" width="20" height="14" rx="2"/>
                <path d="M2 10H22"/>
            </svg>
            الدفع الآن
        `;
    }
}

/**
 * تأكيد الحجز
 */
async function confirmBooking(bookingId) {
    try {
        await apiCall(`${BOOKING_API}/${bookingId}/confirm`, 'POST');
        return true;
    } catch (error) {
        console.error('Error confirming booking:', error);
        return false;
    }
}

/**
 * عرض iframe الدفع (Paymob)
 */
function displayPaymentIframe(iframeToken) {
    // يمكن إضافة modal لعرض iframe الدفع
    const iframeUrl = `https://accept.paymob.com/api/acceptance/iframes/YOUR_IFRAME_ID?payment_token=${iframeToken}`;
    
    // فتح في نافذة جديدة أو modal
    window.open(iframeUrl, '_blank', 'width=600,height=700');
}

// ========== Utility Functions ==========

/**
 * استدعاء API
 */
async function apiCall(url, method = 'GET', body = null) {
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

    const response = await fetch(url, options);

    let data;
    try {
        data = await response.json();
    } catch {
        data = { message: response.statusText || 'حدث خطأ غير معروف' };
    }

    if (!response.ok) {
        throw new Error(data.message || `HTTP Error ${response.status}`);
    }

    return data;
}

/**
 * تنسيق الوقت
 */
function formatTime(timeStr) {
    if (!timeStr) return '—';
    
    try {
        // إذا كان TimeSpan (HH:MM:SS)
        if (typeof timeStr === 'string' && timeStr.includes(':')) {
            const [hours, minutes] = timeStr.split(':').map(Number);
            const period = hours >= 12 ? 'مساءً' : 'صباحاً';
            const formattedHours = hours % 12 === 0 ? 12 : hours % 12;
            return `${formattedHours}:${minutes.toString().padStart(2, '0')} ${period}`;
        }
        return timeStr;
    } catch (e) {
        return timeStr;
    }
}

/**
 * حساب المدة بين وقتين
 */
function calculateDuration(depTimeStr, arrTimeStr) {
    if (!depTimeStr || !arrTimeStr) return 'غير محددة';
    
    try {
        const today = new Date().toDateString();
        
        const [depH, depM] = depTimeStr.split(':').map(Number);
        const depDate = new Date(`${today} ${depH}:${depM}:00`);

        const [arrH, arrM] = arrTimeStr.split(':').map(Number);
        let arrDate = new Date(`${today} ${arrH}:${arrM}:00`);

        if (arrDate < depDate) {
            arrDate.setDate(arrDate.getDate() + 1);
        }

        const diffMs = arrDate - depDate;
        const totalMinutes = Math.floor(diffMs / (1000 * 60));
        const hours = Math.floor(totalMinutes / 60);
        const minutes = totalMinutes % 60;
        
        let durationStr = '';
        if (hours > 0) {
            durationStr += `${hours} ساعة`;
            if (minutes > 0) {
                durationStr += ` و `;
            }
        }
        if (minutes > 0) {
            durationStr += `${minutes} دقيقة`;
        }
        
        return durationStr || 'أقل من دقيقة';
        
    } catch (e) {
        console.error("Error calculating duration:", e);
        return 'غير محددة';
    }
}

/**
 * تحويل حالة الحجز للعربية
 */
function getStatusText(status) {
    const statusMap = {
        'Pending': 'قيد الانتظار',
        'Confirmed': 'مؤكد',
        'Cancelled': 'ملغي',
        'Completed': 'مكتمل'
    };
    return statusMap[status] || status;
}

/**
 * إظهار التحميل
 */
function showLoading() {
    loadingIndicator.style.display = 'block';
    errorMessage.style.display = 'none';
    summaryContent.style.display = 'none';
}

/**
 * إخفاء التحميل
 */
function hideLoading() {
    loadingIndicator.style.display = 'none';
}

/**
 * إظهار الخطأ
 */
function showError(message) {
    hideLoading();
    errorText.textContent = message;
    errorMessage.style.display = 'block';
    summaryContent.style.display = 'none';
}

// ========== Event Listeners ==========
payBtn.addEventListener('click', initiatePayment);
