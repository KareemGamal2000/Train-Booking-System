document.addEventListener('DOMContentLoaded', async function() {
    await checkAuthAndLoadProfile();
    setupDropdownHandlers();
});

function setupDropdownHandlers() {
    // إغلاق القائمة عند النقر خارجها
    document.addEventListener('click', function(event) {
        const dropdown = document.getElementById('profileDropdown');
        const profileToggle = document.querySelector('.profile-toggle');
        
        if (dropdown && !profileToggle.contains(event.target) && !dropdown.contains(event.target)) {
            dropdown.classList.remove('show');
            profileToggle.classList.remove('active');
        }
    });
}

function toggleProfileDropdown() {
    const dropdown = document.getElementById('profileDropdown');
    const toggle = document.querySelector('.profile-toggle');
    dropdown.classList.toggle('show');
    toggle.classList.toggle('active');
}

async function checkAuthAndLoadProfile() {
    const token = localStorage.getItem('authToken');
    
    if (!token || token.trim() === '') {
        alert('يجب تسجيل الدخول أولاً');
        window.location.href = 'Login.html';
        return;
    }

    await loadUserProfile();
}

async function loadUserProfile() {
    const token = localStorage.getItem('authToken');
    
    try {
        const response = await fetch('https://localhost:7192/api/Auth/Profile', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        });

        if (response.ok) {
            const userData = await response.json();
            console.log('Profile data loaded:', userData);
            
            // حفظ البيانات في localStorage
            localStorage.setItem('userData', JSON.stringify(userData));
            
            // تحديث واجهة المستخدم
            updateProfileUI(userData);
        } else if (response.status === 401) {
            // Token expired or invalid
            alert('انتهت صلاحية الجلسة. يرجى تسجيل الدخول مرة أخرى');
            localStorage.removeItem('authToken');
            localStorage.removeItem('userData');
            window.location.href = 'Login.html';
        } else {
            console.error('Failed to load profile:', response.status);
            // محاولة تحميل من localStorage
            loadFromLocalStorage();
        }
    } catch (error) {
        console.error('Error loading profile:', error);
        // محاولة تحميل من localStorage
        loadFromLocalStorage();
    }
}

function loadFromLocalStorage() {
    const cachedData = localStorage.getItem('userData');
    if (cachedData) {
        try {
            const userData = JSON.parse(cachedData);
            updateProfileUI(userData);
            showErrorMessage('تم تحميل البيانات من الذاكرة المؤقتة. قد لا تكون محدثة.');
        } catch (e) {
            showErrorMessage('فشل تحميل بيانات الملف الشخصي');
        }
    } else {
        showErrorMessage('فشل تحميل بيانات الملف الشخصي');
    }
}

function updateProfileUI(userData) {
    // تحديث الاسم والصورة الرمزية
    const firstName = userData.firstName || userData.firstname || '';
    const lastName = userData.lastName || userData.lastname || '';
    const email = userData.email || '';
    const phoneNumber = userData.phoneNumber || userData.phonenumber || 'غير متوفر';
    const role = userData.role || 'مستخدم';

    // تحديث العنوان الرئيسي
    document.getElementById('profileTitle').textContent = `${firstName} ${lastName}`;
    
    // تحديث الأحرف الأولى
    const initials = getInitials(firstName, lastName);
    document.getElementById('userInitialsLarge').textContent = initials;
    
    // تحديث بيانات القائمة المنسدلة
    document.getElementById('userInitials').textContent = initials;
    document.getElementById('profileUserName').textContent = `${firstName} ${lastName}`;
    document.getElementById('dropdownUserName').textContent = `${firstName} ${lastName}`;
    document.getElementById('dropdownUserEmail').textContent = email;

    // تحديث المعلومات الشخصية
    document.getElementById('firstName').textContent = firstName;
    document.getElementById('lastName').textContent = lastName;
    document.getElementById('email').textContent = email;
    document.getElementById('phoneNumber').textContent = phoneNumber;
    
    // تحديث الدور
    const roleElement = document.getElementById('role');
    roleElement.textContent = translateRole(role);
    
    // إضافة الفئة المناسبة للدور
    if (role.toLowerCase() === 'admin') {
        roleElement.classList.add('badge-danger');
        roleElement.style.background = '#f8d7da';
        roleElement.style.color = '#721c24';
    } else {
        roleElement.style.background = '#d1ecf1';
        roleElement.style.color = '#0c5460';
    }

    // تحديث الحالة
    const statusElement = document.getElementById('status');
    statusElement.textContent = 'نشط';
}

function getInitials(firstName, lastName) {
    const first = firstName ? firstName.charAt(0).toUpperCase() : '';
    const last = lastName ? lastName.charAt(0).toUpperCase() : '';
    return first + last || 'U';
}

function translateRole(role) {
    const roleMap = {
        'user': 'مستخدم',
        'admin': 'مدير',
        'manager': 'مشرف',
        'customer': 'عميل'
    };
    return roleMap[role.toLowerCase()] || role;
}

function showErrorMessage(message) {
    const profileContent = document.querySelector('.profile-content');
    const errorDiv = document.createElement('div');
    errorDiv.className = 'error-message';
    errorDiv.textContent = message;
    profileContent.insertBefore(errorDiv, profileContent.firstChild);
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
