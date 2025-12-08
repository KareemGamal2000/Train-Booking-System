let pendingEmailChange = null;

document.addEventListener('DOMContentLoaded', async function() {
    await checkAuthAndLoadProfile();
    setupDropdownHandlers();
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

async function checkAuthAndLoadProfile() {
    const token = localStorage.getItem('authToken');
    
    if (!token || token.trim() === '') {
        alert('يجب تسجيل الدخول أولاً');
        window.location.href = 'Login.html';
        return;
    }

    await loadCurrentEmail();
}

async function loadCurrentEmail() {
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
            const firstName = userData.firstName || userData.firstname || '';
            const lastName = userData.lastName || userData.lastname || '';
            const initials = getInitials(firstName, lastName);
            
            document.getElementById('currentEmail').textContent = userData.email;
            document.getElementById('userInitials').textContent = initials;
            document.getElementById('profileUserName').textContent = `${firstName} ${lastName}`;
            document.getElementById('dropdownUserName').textContent = `${firstName} ${lastName}`;
            document.getElementById('dropdownUserEmail').textContent = userData.email;
        } else {
            // Try to load from localStorage
            const cachedData = localStorage.getItem('userData');
            if (cachedData) {
                const userData = JSON.parse(cachedData);
                document.getElementById('currentEmail').textContent = userData.email;
            } else {
                document.getElementById('currentEmail').textContent = 'غير متوفر';
            }
        }
    } catch (error) {
        console.error('Error loading email:', error);
        document.getElementById('currentEmail').textContent = 'خطأ في التحميل';
    }
}

function togglePassword(inputId) {
    const input = document.getElementById(inputId);
    const button = input.parentElement.querySelector('.toggle-password');
    
    if (input.type === 'password') {
        input.type = 'text';
        button.querySelector('.eye-icon').textContent = '🙈';
    } else {
        input.type = 'password';
        button.querySelector('.eye-icon').textContent = '👁️';
    }
}

async function handleChangeEmail(event) {
    event.preventDefault();
    
    const errorMessage = document.getElementById('errorMessage');
    const successMessage = document.getElementById('successMessage');
    errorMessage.style.display = 'none';
    successMessage.style.display = 'none';
    
    const newEmail = document.getElementById('newEmail').value.trim();
    const password = document.getElementById('password').value;
    const currentEmail = document.getElementById('currentEmail').textContent;
    
    // Validate email format
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(newEmail)) {
        showError('البريد الإلكتروني غير صالح');
        return;
    }
    
    // Check if new email is different
    if (newEmail.toLowerCase() === currentEmail.toLowerCase()) {
        showError('البريد الإلكتروني الجديد يجب أن يكون مختلفاً عن الحالي');
        return;
    }
    
    const token = localStorage.getItem('authToken');
    const submitBtn = document.getElementById('submitBtn');
    const submitText = document.getElementById('submitText');
    const submitSpinner = document.getElementById('submitSpinner');
    
    submitBtn.disabled = true;
    submitText.style.display = 'none';
    submitSpinner.style.display = 'block';
    
    try {
        const response = await fetch('https://localhost:7192/api/Auth/ChangeEmail', {
            method: 'POST',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                newEmail: newEmail,
                password: password
            })
        });
        
        if (response.ok) {
            // Store pending email change
            pendingEmailChange = newEmail;
            
            showSuccess('✓ تم إرسال رمز التحقق إلى بريدك الجديد. يرجى التحقق من صندوق الوارد.');
            
            // Show verification section
            setTimeout(() => {
                document.getElementById('verificationSection').style.display = 'block';
                document.getElementById('verificationSection').scrollIntoView({ behavior: 'smooth' });
            }, 1500);
        } else {
            const errorData = await response.text();
            console.error('Change email error:', errorData);
            
            if (response.status === 400) {
                showError('كلمة السر غير صحيحة أو البريد الإلكتروني مستخدم بالفعل');
            } else if (response.status === 401) {
                showError('انتهت صلاحية الجلسة. يرجى تسجيل الدخول مرة أخرى');
                setTimeout(() => {
                    localStorage.removeItem('authToken');
                    window.location.href = 'Login.html';
                }, 2000);
            } else {
                showError('فشل تغيير البريد الإلكتروني. يرجى المحاولة مرة أخرى');
            }
        }
    } catch (error) {
        console.error('Error changing email:', error);
        showError('حدث خطأ أثناء تغيير البريد الإلكتروني. يرجى المحاولة لاحقاً');
    } finally {
        submitBtn.disabled = false;
        submitText.style.display = 'block';
        submitSpinner.style.display = 'none';
    }
}

async function handleVerification(event) {
    event.preventDefault();
    
    const verificationError = document.getElementById('verificationError');
    verificationError.style.display = 'none';
    
    const verificationCode = document.getElementById('verificationCode').value.trim();
    
    if (verificationCode.length !== 6 || !/^\d{6}$/.test(verificationCode)) {
        showVerificationError('رمز التحقق يجب أن يكون مكون من 6 أرقام');
        return;
    }
    
    const token = localStorage.getItem('authToken');
    const verifyBtn = document.getElementById('verifyBtn');
    const verifyText = document.getElementById('verifyText');
    const verifySpinner = document.getElementById('verifySpinner');
    
    verifyBtn.disabled = true;
    verifyText.style.display = 'none';
    verifySpinner.style.display = 'block';
    
    try {
        const response = await fetch('https://localhost:7192/api/Auth/ConfirmEmailChange', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                newEmail: pendingEmailChange,
                token: verificationCode
            })
        });
        
        if (response.ok) {
            alert('✓ تم تغيير البريد الإلكتروني بنجاح! سيتم تحويلك لتسجيل الدخول...');
            
            // Clear data and redirect
            localStorage.removeItem('authToken');
            localStorage.removeItem('userData');
            
            setTimeout(() => {
                window.location.href = 'Login.html';
            }, 2000);
        } else {
            const errorData = await response.text();
            console.error('Verification error:', errorData);
            
            if (response.status === 400) {
                showVerificationError('رمز التحقق غير صحيح أو منتهي الصلاحية');
            } else if (response.status === 401) {
                showVerificationError('انتهت صلاحية الجلسة. يرجى تسجيل الدخول مرة أخرى');
                setTimeout(() => {
                    localStorage.removeItem('authToken');
                    window.location.href = 'Login.html';
                }, 2000);
            } else {
                showVerificationError('فشل التحقق. يرجى المحاولة مرة أخرى');
            }
        }
    } catch (error) {
        console.error('Error verifying code:', error);
        showVerificationError('حدث خطأ أثناء التحقق. يرجى المحاولة لاحقاً');
    } finally {
        verifyBtn.disabled = false;
        verifyText.style.display = 'block';
        verifySpinner.style.display = 'none';
    }
}

async function resendCode(event) {
    event.preventDefault();
    
    if (!pendingEmailChange) {
        alert('يرجى إدخال البريد الإلكتروني الجديد أولاً');
        return;
    }
    
    const token = localStorage.getItem('authToken');
    const password = document.getElementById('password').value;
    
    try {
        const response = await fetch('https://localhost:7192/api/Auth/ChangeEmail', {
            method: 'POST',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                newEmail: pendingEmailChange,
                password: password
            })
        });
        
        if (response.ok) {
            alert('✓ تم إعادة إرسال رمز التحقق');
        } else {
            alert('فشل إعادة الإرسال. يرجى المحاولة مرة أخرى');
        }
    } catch (error) {
        console.error('Error resending code:', error);
        alert('حدث خطأ أثناء إعادة الإرسال');
    }
}

function showError(message) {
    const errorMessage = document.getElementById('errorMessage');
    errorMessage.textContent = message;
    errorMessage.style.display = 'block';
    errorMessage.scrollIntoView({ behavior: 'smooth', block: 'nearest' });
}

function showSuccess(message) {
    const successMessage = document.getElementById('successMessage');
    successMessage.textContent = message;
    successMessage.style.display = 'block';
    successMessage.scrollIntoView({ behavior: 'smooth', block: 'nearest' });
}

function showVerificationError(message) {
    const verificationError = document.getElementById('verificationError');
    verificationError.textContent = message;
    verificationError.style.display = 'block';
    verificationError.scrollIntoView({ behavior: 'smooth', block: 'nearest' });
}

function getInitials(firstName, lastName) {
    const first = firstName ? firstName.charAt(0).toUpperCase() : '';
    const last = lastName ? lastName.charAt(0).toUpperCase() : '';
    return first + last || 'U';
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
