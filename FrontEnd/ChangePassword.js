document.addEventListener('DOMContentLoaded', async function() {
    checkAuth();
    setupPasswordValidation();
    setupDropdownHandlers();
    await loadUserProfile();
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

function checkAuth() {
    const token = localStorage.getItem('authToken');
    
    if (!token || token.trim() === '') {
        alert('يجب تسجيل الدخول أولاً');
        window.location.href = 'Login.html';
        return;
    }
}

function setupPasswordValidation() {
    const newPassword = document.getElementById('newPassword');
    const confirmPassword = document.getElementById('confirmPassword');
    
    newPassword.addEventListener('input', function() {
        checkPasswordStrength(this.value);
        checkPasswordMatch();
    });
    
    confirmPassword.addEventListener('input', checkPasswordMatch);
}

function checkPasswordStrength(password) {
    const strengthIndicator = document.getElementById('passwordStrength');
    
    if (password.length === 0) {
        strengthIndicator.className = 'password-strength';
        return;
    }
    
    let strength = 0;
    
    // Check length
    if (password.length >= 6) strength++;
    if (password.length >= 8) strength++;
    
    // Check for uppercase
    if (/[A-Z]/.test(password)) strength++;
    
    // Check for lowercase
    if (/[a-z]/.test(password)) strength++;
    
    // Check for numbers
    if (/[0-9]/.test(password)) strength++;
    
    // Check for special characters
    if (/[@#$%^&+=!]/.test(password)) strength++;
    
    if (strength <= 2) {
        strengthIndicator.className = 'password-strength weak';
    } else if (strength <= 4) {
        strengthIndicator.className = 'password-strength medium';
    } else {
        strengthIndicator.className = 'password-strength strong';
    }
}

function checkPasswordMatch() {
    const newPassword = document.getElementById('newPassword');
    const confirmPassword = document.getElementById('confirmPassword');
    const matchIndicator = document.getElementById('passwordMatch');
    
    if (confirmPassword.value.length === 0) {
        matchIndicator.textContent = '';
        matchIndicator.className = 'password-match';
        return;
    }
    
    if (newPassword.value === confirmPassword.value) {
        matchIndicator.textContent = '✓ كلمات السر متطابقة';
        matchIndicator.className = 'password-match match';
    } else {
        matchIndicator.textContent = '✗ كلمات السر غير متطابقة';
        matchIndicator.className = 'password-match no-match';
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

function validatePassword(password) {
    const errors = [];
    
    if (password.length < 6) {
        errors.push('يجب أن تكون كلمة السر 6 أحرف على الأقل');
    }
    
    if (!/[A-Z]/.test(password)) {
        errors.push('يجب أن تحتوي على حرف كبير على الأقل');
    }
    
    if (!/[a-z]/.test(password)) {
        errors.push('يجب أن تحتوي على حرف صغير على الأقل');
    }
    
    if (!/[0-9]/.test(password)) {
        errors.push('يجب أن تحتوي على رقم على الأقل');
    }
    
    if (!/[@#$%^&+=!]/.test(password)) {
        errors.push('يجب أن تحتوي على رمز خاص على الأقل (@#$%^&+=!)');
    }
    
    return errors;
}

async function handleChangePassword(event) {
    event.preventDefault();
    
    const errorMessage = document.getElementById('errorMessage');
    const successMessage = document.getElementById('successMessage');
    errorMessage.style.display = 'none';
    successMessage.style.display = 'none';
    
    const oldPassword = document.getElementById('oldPassword').value;
    const newPassword = document.getElementById('newPassword').value;
    const confirmPassword = document.getElementById('confirmPassword').value;
    
    // Validate passwords match
    if (newPassword !== confirmPassword) {
        showError('كلمات السر الجديدة غير متطابقة');
        return;
    }
    
    // Validate password strength
    const validationErrors = validatePassword(newPassword);
    if (validationErrors.length > 0) {
        showError('كلمة السر الجديدة لا تستوفي المتطلبات:\n' + validationErrors.join('\n'));
        return;
    }
    
    // Check if new password is different from old password
    if (oldPassword === newPassword) {
        showError('كلمة السر الجديدة يجب أن تكون مختلفة عن القديمة');
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
        const response = await fetch('https://localhost:7192/api/Auth/ChangePassword', {
            method: 'POST',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                currentPassword: oldPassword,
                newPassword: newPassword,
                confirmPassword: confirmPassword
            })
        });
        
        if (response.ok) {
            showSuccess('✓ تم تغيير كلمة السر بنجاح! سيتم تحويلك لتسجيل الدخول...');
            
            // Clear form
            document.getElementById('changePasswordForm').reset();
            
            // Redirect to login after 2 seconds
            setTimeout(() => {
                localStorage.removeItem('authToken');
                localStorage.removeItem('userData');
                window.location.href = 'Login.html';
            }, 2000);
        } else {
            const errorData = await response.text();
            console.error('Change password error:', errorData);
            
            if (response.status === 400) {
                showError('كلمة السر الحالية غير صحيحة أو كلمة السر الجديدة لا تستوفي المتطلبات');
            } else if (response.status === 401) {
                showError('انتهت صلاحية الجلسة. يرجى تسجيل الدخول مرة أخرى');
                setTimeout(() => {
                    localStorage.removeItem('authToken');
                    window.location.href = 'Login.html';
                }, 2000);
            } else {
                showError('فشل تغيير كلمة السر. يرجى المحاولة مرة أخرى');
            }
        }
    } catch (error) {
        console.error('Error changing password:', error);
        showError('حدث خطأ أثناء تغيير كلمة السر. يرجى المحاولة لاحقاً');
    } finally {
        submitBtn.disabled = false;
        submitText.style.display = 'block';
        submitSpinner.style.display = 'none';
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
