window.addEventListener('scroll', function() {
    let scrollPosition = window.scrollY;
    let hero = document.querySelector('.hero-content');
    hero.style.transform = `translateY(${scrollPosition * 0.9}px)`;
});

document.querySelector(".btn-gold").addEventListener("mousedown", function() {
    this.style.background = "#b38b2a";
});

document.querySelector(".btn-gold").addEventListener("mouseup", function() {
    this.style.background = "#d4af37";
});

document.addEventListener('DOMContentLoaded', async function () {
    console.log('Home page loaded - connecting to backend...');
    
    // تحديث واجهة المستخدم بناءً على حالة تسجيل الدخول
    updateUIBasedOnAuth();

    try {
        const res = await fetch('http://localhost:5274/api/Train'); 
        console.log('Status:', res.status);
        const text = await res.text();          
        console.log('Raw response:', text);      

        let data;
        try {
            data = JSON.parse(text);             
        } catch (e) {
            console.error('Not valid JSON:', e);
            return;
        }

        console.log('Parsed JSON:', data);
        displayStats(data.length ?? 0, 0);       
    } catch (error) {
        console.error('Failed to fetch backend data:', error);
    }
});

function displayStats(trainCount, stationCount) {
    const heroContent = document.querySelector('.hero-content');
    const statsHtml = `
        <div style="margin-top: 20px; font-size: 16px; color: #444;">
            <p>عدد القطارات المتاحة: ${trainCount}</p>
            <p>عدد المحطات: ${stationCount}</p>
        </div>`;
    heroContent.insertAdjacentHTML('beforeend', statsHtml);
}

/**
 * تحديث الواجهة بناءً على حالة تسجيل الدخول
 */
async function updateUIBasedOnAuth() {
    const token = localStorage.getItem('authToken');
    const loginLink = document.getElementById('loginLink');
    const registerLink = document.getElementById('registerLink');
    const userProfileMenu = document.getElementById('userProfileMenu');
    
    if (token && token.trim() !== '') {
        // المستخدم مسجل دخول
        console.log('User is logged in');
        
        // إخفاء روابط تسجيل الدخول والتسجيل
        if (loginLink) loginLink.style.display = 'none';
        if (registerLink) registerLink.style.display = 'none';
        
        // إظهار قائمة الملف الشخصي
        if (userProfileMenu) {
            userProfileMenu.style.display = 'block';
            
            // جلب بيانات المستخدم من API
            await loadUserProfile();
        }
    } else {
        // المستخدم غير مسجل دخول
        console.log('User is not logged in');
        
        // إظهار روابط تسجيل الدخول والتسجيل
        if (loginLink) loginLink.style.display = 'block';
        if (registerLink) registerLink.style.display = 'block';
        
        // إخفاء قائمة الملف الشخصي
        if (userProfileMenu) userProfileMenu.style.display = 'none';
    }
}

/**
 * جلب بيانات المستخدم من API
 */
async function loadUserProfile() {
    try {
        const token = localStorage.getItem('authToken');
        const response = await fetch('https://localhost:7192/api/Auth/Profile', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        });
        
        if (response.ok) {
            const userData = await response.json();
            console.log('User profile loaded:', userData);
            
            // تحديث الواجهة ببيانات المستخدم
            updateProfileUI(userData);
        } else {
            console.error('Failed to load profile:', response.status);
            // إذا فشل جلب البيانات، استخدم البيانات المحفوظة محلياً
            const userName = localStorage.getItem('username') || localStorage.getItem('userEmail') || 'مستخدم';
            const userEmail = localStorage.getItem('userEmail') || '';
            updateProfileUI({ userName, email: userEmail });
        }
    } catch (error) {
        console.error('Error loading profile:', error);
        // استخدام البيانات المحفوظة محلياً في حالة الخطأ
        const userName = localStorage.getItem('username') || localStorage.getItem('userEmail') || 'مستخدم';
        const userEmail = localStorage.getItem('userEmail') || '';
        updateProfileUI({ userName, email: userEmail });
    }
}

/**
 * تحديث واجهة الملف الشخصي ببيانات المستخدم
 */
function updateProfileUI(userData) {
    // تحديث الاسم في الشريط العلوي
    const profileUserName = document.getElementById('profileUserName');
    const dropdownUserName = document.getElementById('dropdownUserName');
    const dropdownUserEmail = document.getElementById('dropdownUserEmail');
    const userInitials = document.getElementById('userInitials');
    
    const displayName = userData.userName || userData.firstName || userData.email || 'مستخدم';
    const email = userData.email || '';
    
    if (profileUserName) profileUserName.textContent = displayName;
    if (dropdownUserName) dropdownUserName.textContent = displayName;
    if (dropdownUserEmail) dropdownUserEmail.textContent = email;
    
    // تحديث الأحرف الأولى
    if (userInitials) {
        const initials = displayName.substring(0, 2).toUpperCase();
        userInitials.textContent = initials;
    }
}

/**
 * تبديل عرض القائمة المنسدلة
 */
function toggleProfileDropdown() {
    const dropdown = document.getElementById('profileDropdown');
    const toggle = document.querySelector('.profile-toggle');
    
    if (dropdown.classList.contains('show')) {
        dropdown.classList.remove('show');
        toggle.classList.remove('active');
    } else {
        dropdown.classList.add('show');
        toggle.classList.add('active');
    }
}

/**
 * إغلاق القائمة المنسدلة عند النقر خارجها
 */
document.addEventListener('click', function(event) {
    const userProfile = document.querySelector('.user-profile');
    const dropdown = document.getElementById('profileDropdown');
    
    if (userProfile && !userProfile.contains(event.target)) {
        if (dropdown) {
            dropdown.classList.remove('show');
            const toggle = document.querySelector('.profile-toggle');
            if (toggle) toggle.classList.remove('active');
        }
    }
});

/**
 * تسجيل الخروج
 */
function logout() {
    if (confirm('هل أنت متأكد من تسجيل الخروج؟')) {
        // مسح جميع البيانات المحفوظة
        localStorage.removeItem('authToken');
        localStorage.removeItem('userEmail');
        localStorage.removeItem('username');
        localStorage.removeItem('userId');
        
        // إعادة التوجيه لصفحة تسجيل الدخول
        window.location.href = 'Login.html';
    }
}

// جعل الدوال متاحة عالمياً
window.toggleProfileDropdown = toggleProfileDropdown;
window.logout = logout;
