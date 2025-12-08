const apiUrl = 'https://localhost:7192';
let allBookings = [];
let currentFilter = 'all';
let bookingToCancel = null;

document.addEventListener('DOMContentLoaded', async function() {
    setupFilterButtons();
    setupDropdownHandlers();
    await checkAuthAndLoadBookings();
});

function setupDropdownHandlers() {
    document.addEventListener('click', function(event) {
        const dropdown = document.getElementById('profileDropdown');
        const profileToggle = document.querySelector('.profile-toggle');
        
        if (dropdown && profileToggle && !profileToggle.contains(event.target) && !dropdown.contains(event.target)) {
            dropdown.classList.remove('show');
            profileToggle.classList.remove('active');
        }
    });
}

function toggleProfileDropdown() {
    const dropdown = document.getElementById('profileDropdown');
    const toggle = document.querySelector('.profile-toggle');
    if (dropdown && toggle) {
        dropdown.classList.toggle('show');
        toggle.classList.toggle('active');
    }
}

async function loadUserProfile() {
    const token = localStorage.getItem('authToken');
    try {
        const response = await fetch('https://localhost:7192/api/Auth/Profile', {
            headers: { 'Authorization': `Bearer ${token}` }
        });
        if (response.ok) {
            const userData = await response.json();
            const firstName = userData.firstName || userData.firstname || '';
            const lastName = userData.lastName || userData.lastname || '';
            const initials = getInitials(firstName, lastName);
            
            document.getElementById('userInitials').textContent = initials;
            document.getElementById('profileUserName').textContent = `${firstName} ${lastName}`;
            document.getElementById('dropdownUserName').textContent = `${firstName} ${lastName}`;
            document.getElementById('dropdownUserEmail').textContent = userData.email;
        }
    } catch (error) {
        console.error('Error loading profile:', error);
    }
}

function getInitials(firstName, lastName) {
    const first = firstName ? firstName.charAt(0).toUpperCase() : '';
    const last = lastName ? lastName.charAt(0).toUpperCase() : '';
    return first + last || 'U';
}

async function checkAuthAndLoadBookings() {
    const token = localStorage.getItem('authToken');
    
    if (!token || token.trim() === '') {
        alert('يجب تسجيل الدخول أولاً');
        window.location.href = 'Login.html';
        return;
    }

    // تحميل بيانات المستخدم والحجوزات معاً
    await Promise.all([
        loadUserProfile(),
        loadBookings()
    ]);
}

async function loadBookings() {
    const token = localStorage.getItem('authToken');
    const loadingIndicator = document.getElementById('loadingIndicator');
    const emptyState = document.getElementById('emptyState');
    const bookingsList = document.getElementById('bookingsList');

    loadingIndicator.style.display = 'block';
    emptyState.style.display = 'none';
    bookingsList.innerHTML = '';

    try {
        const response = await fetch('https://localhost:7192/api/Booking/user/bookings', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        });

        loadingIndicator.style.display = 'none';

        if (response.ok) {
            allBookings = await response.json();
            console.log('Bookings loaded:', allBookings);
            
            if (allBookings.length === 0) {
                emptyState.style.display = 'block';
            } else {
                displayBookings(allBookings);
            }
        } else if (response.status === 401) {
            alert('انتهت صلاحية الجلسة. يرجى تسجيل الدخول مرة أخرى');
            localStorage.removeItem('authToken');
            window.location.href = 'Login.html';
        } else {
            console.error('Failed to load bookings:', response.status);
            emptyState.style.display = 'block';
        }
    } catch (error) {
        console.error('Error loading bookings:', error);
        loadingIndicator.style.display = 'none';
        
        // التحقق من نوع الخطأ
        if (error.name === 'TypeError' && error.message.includes('Failed to fetch')) {
            emptyState.style.display = 'block';
            emptyState.querySelector('h2').textContent = 'خطأ في الاتصال';
            emptyState.querySelector('p').textContent = 'تعذر الاتصال بالخادم. يرجى التحقق من اتصال الإنترنت والمحاولة مرة أخرى.';
        } else {
            emptyState.style.display = 'block';
            emptyState.querySelector('h2').textContent = 'خطأ في تحميل الحجوزات';
            emptyState.querySelector('p').textContent = 'حدث خطأ أثناء تحميل البيانات. يرجى المحاولة لاحقاً';
        }
        
        // إضافة زر لإعادة المحاولة
        const retryBtn = document.createElement('button');
        retryBtn.textContent = 'إعادة المحاولة';
        retryBtn.className = 'btn-primary';
        retryBtn.style.marginTop = '1rem';
        retryBtn.onclick = () => location.reload();
        
        const emptyStateP = emptyState.querySelector('p');
        if (emptyStateP && !emptyState.querySelector('.btn-primary')) {
            emptyStateP.parentNode.insertBefore(retryBtn, emptyStateP.nextSibling);
        }
    }
}

function displayBookings(bookings) {
    const bookingsList = document.getElementById('bookingsList');
    bookingsList.innerHTML = '';

    bookings.forEach(booking => {
        const card = createBookingCard(booking);
        bookingsList.appendChild(card);
    });
}

function createBookingCard(booking) {
    const card = document.createElement('div');
    card.className = 'booking-card';
    
    // استخدام الأسماء الصحيحة من الـ DTO
    const status = booking.bookingStatus || 'Pending';
    const statusClass = `status-${status.toLowerCase()}`;
    const statusText = translateStatus(status);
    
    const bookingDate = formatDate(booking.bookingDate);
    
    // استخراج معلومات المقاعد والدرجة من التذاكر
    const tickets = booking.tickets || [];
    const seatNumbers = tickets.map(t => t.seatNumber).join(', ');
    const className = tickets.length > 0 ? tickets[0].className : 'غير متوفر';
    
    // استخراج أسماء المحطات
    const departureStation = booking.departureStationNameAR || `محطة رقم ${booking.departureStopID}`;
    const arrivalStation = booking.arrivalStationNameAR || `محطة رقم ${booking.arrivalStopID}`;
    
    card.innerHTML = `
        <div class="booking-header">
            <div class="booking-reference">
                <span class="reference-label">رقم الحجز</span>
                <span class="reference-number">#${booking.bookingId}</span>
            </div>
            <span class="booking-status ${statusClass}">${statusText}</span>
        </div>
        
        <div class="booking-details">
            <div class="detail-item">
                <span class="detail-label">🚂 رقم الرحلة</span>
                <span class="detail-value">${booking.tripID || 'غير متوفر'}</span>
            </div>
            <div class="detail-item">
                <span class="detail-label">📍 محطة المغادرة</span>
                <span class="detail-value">${departureStation}</span>
            </div>
            <div class="detail-item">
                <span class="detail-label">📍 محطة الوصول</span>
                <span class="detail-value">${arrivalStation}</span>
            </div>
            <div class="detail-item">
                <span class="detail-label">📅 تاريخ الحجز</span>
                <span class="detail-value">${bookingDate}</span>
            </div>
            <div class="detail-item">
                <span class="detail-label">🎫 رقم المقعد</span>
                <span class="detail-value">${seatNumbers || 'غير متوفر'}</span>
            </div>
            <div class="detail-item">
                <span class="detail-label">🏷️ الدرجة</span>
                <span class="detail-value">${className}</span>
            </div>
            <div class="detail-item">
                <span class="detail-label">📊 عدد التذاكر</span>
                <span class="detail-value">${tickets.length} تذكرة</span>
            </div>
            <div class="detail-item">
                <span class="detail-label">💰 السعر الإجمالي</span>
                <span class="detail-value">${booking.totalPrice || '0'} جنيه</span>
            </div>
        </div>
        
        <div class="booking-actions">
            <button class="btn btn-secondary" onclick='showBookingDetails(${JSON.stringify(booking).replace(/'/g, "\\'")})'> 
                عرض التفاصيل
            </button>
            ${status.toLowerCase() === 'confirmed' || status.toLowerCase() === 'pending' ? 
                `<button class="btn btn-danger" onclick="showCancelModal('${booking.bookingId}')">
                    إلغاء الحجز
                </button>` : 
                ''}
        </div>
    `;
    
    return card;
}

function setupFilterButtons() {
    const filterButtons = document.querySelectorAll('.filter-btn');
    
    filterButtons.forEach(btn => {
        btn.addEventListener('click', function() {
            filterButtons.forEach(b => b.classList.remove('active'));
            this.classList.add('active');
            
            currentFilter = this.dataset.status;
            filterBookings(currentFilter);
        });
    });
}

function filterBookings(status) {
    if (status === 'all') {
        displayBookings(allBookings);
    } else {
        const filtered = allBookings.filter(b => 
            (b.bookingStatus || 'Pending').toLowerCase() === status.toLowerCase()
        );
        displayBookings(filtered);
        
        if (filtered.length === 0) {
            document.getElementById('emptyState').style.display = 'block';
            document.getElementById('bookingsList').innerHTML = '';
        } else {
            document.getElementById('emptyState').style.display = 'none';
        }
    }
}

function showBookingDetails(booking) {
    const modal = document.getElementById('bookingDetailsModal');
    const content = document.getElementById('bookingDetailsContent');
    
    const status = booking.bookingStatus || 'Pending';
    const statusClass = `status-${status.toLowerCase()}`;
    const statusText = translateStatus(status);
    
    const tickets = booking.tickets || [];
    const seatNumbers = tickets.map(t => t.seatNumber).join(', ');
    const className = tickets.length > 0 ? tickets[0].className : 'غير متوفر';
    
    // استخراج أسماف المحطات
    const departureStation = booking.departureStationNameAR || `محطة رقم ${booking.departureStopID}`;
    const arrivalStation = booking.arrivalStationNameAR || `محطة رقم ${booking.arrivalStopID}`;
    
    content.innerHTML = `
        <div class="booking-details">
            <h3 style="margin-bottom: 1rem; color: #d4af37;">معلومات الحجز</h3>
            <div style="display: grid; gap: 1rem;">
                <div class="detail-item">
                    <span class="detail-label">رقم الحجز</span>
                    <span class="detail-value">#${booking.bookingId}</span>
                </div>
                <div class="detail-item">
                    <span class="detail-label">الحالة</span>
                    <span class="booking-status ${statusClass}">${statusText}</span>
                </div>
                <div class="detail-item">
                    <span class="detail-label">رقم الرحلة</span>
                    <span class="detail-value">${booking.tripID || 'غير متوفر'}</span>
                </div>
                <div class="detail-item">
                    <span class="detail-label">محطة المغادرة</span>
                    <span class="detail-value">${departureStation}</span>
                </div>
                <div class="detail-item">
                    <span class="detail-label">محطة الوصول</span>
                    <span class="detail-value">${arrivalStation}</span>
                </div>
                <div class="detail-item">
                    <span class="detail-label">تاريخ الحجز</span>
                    <span class="detail-value">${formatDate(booking.bookingDate)}</span>
                </div>
                <div class="detail-item">
                    <span class="detail-label">رقم المقعد</span>
                    <span class="detail-value">${seatNumbers || 'غير متوفر'}</span>
                </div>
                <div class="detail-item">
                    <span class="detail-label">الدرجة</span>
                    <span class="detail-value">${className}</span>
                </div>
                <div class="detail-item">
                    <span class="detail-label">عدد التذاكر</span>
                    <span class="detail-value">${tickets.length} تذكرة</span>
                </div>
                <div class="detail-item">
                    <span class="detail-label">السعر الإجمالي</span>
                    <span class="detail-value" style="color: #28a745; font-size: 1.5rem;">${booking.totalPrice || '0'} جنيه</span>
                </div>
            </div>
        </div>
    `;
    
    modal.classList.add('show');
}

function closeDetailsModal() {
    const modal = document.getElementById('bookingDetailsModal');
    modal.classList.remove('show');
}

function showCancelModal(bookingId) {
    bookingToCancel = bookingId;
    const modal = document.getElementById('cancelModal');
    modal.classList.add('show');
}

function closeCancelModal() {
    bookingToCancel = null;
    const modal = document.getElementById('cancelModal');
    modal.classList.remove('show');
}

async function confirmCancelBooking() {
    if (!bookingToCancel) return;
    
    const token = localStorage.getItem('authToken');
    const confirmBtn = document.getElementById('confirmCancelBtn');
    
    confirmBtn.disabled = true;
    confirmBtn.textContent = 'جاري الإلغاء...';
    
    try {
        const response = await fetch(`${apiUrl}/api/Booking/${bookingId}`, {
            method: 'DELETE',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                bookingId: bookingId,
                reason: 'إلغاء من قبل المستخدم'
            })
        });
        
        if (response.ok) {
            alert('تم إلغاء الحجز بنجاح');
            closeCancelModal();
            await loadBookings(); // Reload bookings
        } else {
            const error = await response.text();
            alert(`فشل إلغاء الحجز: ${error}`);
        }
    } catch (error) {
        console.error('Error cancelling booking:', error);
        alert('حدث خطأ أثناء إلغاء الحجز');
    } finally {
        confirmBtn.disabled = false;
        confirmBtn.textContent = 'تأكيد الإلغاء';
    }
}

function formatDate(dateString) {
    if (!dateString) return 'غير متوفر';
    
    try {
        const date = new Date(dateString);
        return date.toLocaleDateString('ar-EG', {
            year: 'numeric',
            month: 'long',
            day: 'numeric'
        });
    } catch (e) {
        return dateString;
    }
}

function translateStatus(status) {
    const statusMap = {
        'confirmed': 'مؤكد',
        'pending': 'معلق',
        'cancelled': 'ملغي',
        'completed': 'مكتمل'
    };
    return statusMap[status.toLowerCase()] || status;
}

function logout() {
    if (confirm('هل أنت متأكد من تسجيل الخروج؟')) {
        localStorage.removeItem('authToken');
        localStorage.removeItem('userData');
        localStorage.removeItem('availableSeatsData');
        localStorage.removeItem('selectedSeats');
        localStorage.removeItem('bookingDetails');
        window.location.href = 'Login.html';
    }
}
