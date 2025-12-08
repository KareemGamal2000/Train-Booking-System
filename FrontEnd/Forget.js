// ===========================================
// تعاريف المتغيرات وعناصر DOM
// ===========================================
const FORGOT_PASSWORD_URL = 'https://localhost:7192/api/Auth/ForgotPassword';
// نقطة النهاية الجديدة لتأكيد الكود:
const VERIFY_CODE_URL = 'https://localhost:7192/api/Auth/VerifyCode';
// نقطة النهاية لتغيير كلمة المرور:
const RESET_PASSWORD_URL = 'https://localhost:7192/api/Auth/ResetPassword'; 

const forgetForm = document.getElementById('forget-form');
const title = document.getElementById('title');
const infoText = document.getElementById('info');

const emailStep = document.getElementById('emailStep');
const codeStep = document.getElementById('codeStep');
const resetStep = document.getElementById('resetStep'); // استخدامه الآن كخطوة منفصلة

const emailInput = document.getElementById('email');
const emailError = document.getElementById('email-error');
const sendCodeBtn = document.getElementById('sendCodeBtn');

const codeInput = document.getElementById('code');
const codeError = document.getElementById('code-error');
const verifyCodeBtn = document.getElementById('verifyCodeBtn'); // الزر في خطوة الكود
const backToEmailBtn = document.getElementById('backToEmail');

const passwordNewInput = document.getElementById('passwordNew');
const passwordNewError = document.getElementById('passwordNew-error');
const passwordConfirmInput = document.getElementById('passwordConfirm');
const passwordConfirmError = document.getElementById('passwordConfirm-error');
const savePassBtn = document.getElementById('savePassBtn'); // الزر في خطوة حفظ كلمة المرور
const backToCodeBtn = document.getElementById('backToCode');

let submittedEmail = ''; // لتخزين الإيميل بعد نجاح الخطوة الأولى

// ===========================================
// دالة مساعدة لتبديل الخطوات
// ===========================================
function showStep(stepName) {
    emailStep.style.display = 'none';
    codeStep.style.display = 'none';
    resetStep.style.display = 'none';

    if (stepName === 'email') {
        emailStep.style.display = 'block';
        title.textContent = 'استعادة كلمة المرور';
        infoText.textContent = 'أدخل بريدك الإلكتروني لإرسال كود الاستعادة.';
    } else if (stepName === 'code') {
        codeStep.style.display = 'block';
        title.textContent = 'تأكيد كود الاستعادة';
        // نتأكد من أن submittedEmail ليس فارغاً قبل عرضه
        const emailDisplay = submittedEmail || 'البريد الإلكتروني';
        infoText.textContent = `تم إرسال الكود لـ ${emailDisplay}. الرجاء إدخاله.`;
    } else if (stepName === 'reset') {
        resetStep.style.display = 'block';
        title.textContent = 'تعيين كلمة المرور الجديدة';
        infoText.textContent = 'أدخل كلمة المرور الجديدة وأكدها.';
    }
}

// ===========================================
// معالجة إرسال النموذج الرئيسي
// ===========================================
forgetForm.addEventListener('submit', async (e) => {
    e.preventDefault();

    // ----------------------------------------------------
    // 1. خطوة إرسال البريد الإلكتروني (Email Step)
    // ----------------------------------------------------
    if (emailStep.style.display !== 'none') {
        emailError.textContent = '';
        const email = emailInput.value.trim();

        if (!email) {
            emailError.textContent = 'الرجاء إدخال البريد الإلكتروني.';
            return;
        }

        sendCodeBtn.disabled = true;
        sendCodeBtn.textContent = 'جارٍ الإرسال...';
        
        try {
            const response = await fetch(FORGOT_PASSWORD_URL, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ email: email })
            });

            sendCodeBtn.disabled = false;
            sendCodeBtn.textContent = 'إرسال الكود';

            if (response.ok) {
                submittedEmail = email; 
                showStep('code'); // الانتقال لخطوة الكود
            } else {
                const errorData = await response.json();
                emailError.textContent = errorData.message || 'فشل إرسال طلب استعادة كلمة المرور.';
                console.error('Error Response:', errorData);
            }
        } catch (error) {
            sendCodeBtn.disabled = false;
            sendCodeBtn.textContent = 'إرسال الكود';
            emailError.textContent = 'فشل الاتصال بالخادم. تأكد من أن السيرفر يعمل.';
            console.error('Network Error:', error);
        }

    // ----------------------------------------------------
    // 2. خطوة تأكيد الكود (Code Step)
    // ----------------------------------------------------
    } else if (codeStep.style.display !== 'none') {
        codeError.textContent = '';
        const code = codeInput.value.trim();

        if (!code) {
            codeError.textContent = 'الرجاء إدخال كود الاستعادة.';
            return;
        }

        verifyCodeBtn.disabled = true;
        verifyCodeBtn.textContent = 'جارٍ التأكيد...';
        
        try {
            // استدعاء API تأكيد الكود
            const response = await fetch(VERIFY_CODE_URL, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ 
                    email: submittedEmail, 
                    code: code
                }) 
            });

            verifyCodeBtn.disabled = false;
            verifyCodeBtn.textContent = 'تأكيد الكود';

            if (response.ok) {
                // نجاح تأكيد الكود، الانتقال لخطوة تعيين كلمة المرور
                showStep('reset'); 
            } else {
                const errorData = await response.json();
                codeError.textContent = errorData.message || 'فشل تأكيد الكود. الرجاء المحاولة مرة أخرى.';
                console.error('Error Response:', errorData);
            }
        } catch (error) {
            verifyCodeBtn.disabled = false;
            verifyCodeBtn.textContent = 'تأكيد الكود';
            codeError.textContent = 'فشل الاتصال بالخادم.';
            console.error('Network Error:', error);
        }
    
    // ----------------------------------------------------
    // 3. خطوة تعيين كلمة المرور الجديدة (Reset Step)
    // ----------------------------------------------------
    } else if (resetStep.style.display !== 'none') {
        passwordNewError.textContent = '';
        passwordConfirmError.textContent = '';
        
        const code = codeInput.value.trim(); // ما زلنا بحاجة للكود المرسل سابقاً
        const newPassword = passwordNewInput.value;
        const confirmPassword = passwordConfirmInput.value;

        // التحقق من الحقول المطلوبة
        if (newPassword.length < 6) { 
            passwordNewError.textContent = 'يجب أن تكون كلمة المرور 6 أحرف على الأقل.';
            return;
        }
        if (newPassword !== confirmPassword) {
            passwordConfirmError.textContent = 'كلمتا المرور غير متطابقتين.';
            return;
        }

        savePassBtn.disabled = true;
        savePassBtn.textContent = 'جارٍ الحفظ...';

        try {
            // إرسال جميع البيانات المطلوبة لـ ResetPassword
            const response = await fetch(RESET_PASSWORD_URL, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ 
                    email: submittedEmail, 
                    code: code,
                    newPassword: newPassword,
                    confirmPassword: confirmPassword 
                }) 
            });

            savePassBtn.disabled = false;
            savePassBtn.textContent = 'حفظ كلمة المرور';

            if (response.ok) {
                alert('تم تغيير كلمة المرور بنجاح! سيتم توجيهك لصفحة تسجيل الدخول.');
                window.location.href = 'Login.html'; // التوجيه لصفحة تسجيل الدخول
            } else {
                const errorData = await response.json();
                
                // عرض رسالة الخطأ في مكان مناسب
                const errorMessage = errorData.message || 'فشل تعيين كلمة المرور الجديدة.';
                passwordNewError.textContent = errorMessage;
                console.error('Error Response:', errorData);
            }
        } catch (error) {
            savePassBtn.disabled = false;
            savePassBtn.textContent = 'حفظ كلمة المرور';
            passwordNewError.textContent = 'فشل الاتصال بالخادم.';
            console.error('Network Error:', error);
        }
    }
});

// ===========================================
// معالجة أزرار الرجوع
// ===========================================
// الرجوع من خطوة الكود إلى الإيميل
backToEmailBtn.addEventListener('click', () => {
    codeInput.value = ''; 
    codeError.textContent = ''; 
    showStep('email');
});

// الرجوع من خطوة تعيين كلمة المرور إلى الكود
backToCodeBtn.addEventListener('click', () => {
    passwordNewInput.value = ''; 
    passwordConfirmInput.value = '';
    passwordNewError.textContent = '';
    passwordConfirmError.textContent = '';
    showStep('code');
});

// ===========================================
// تشغيل الدالة لعرض الخطوة الأولى عند التحميل
// ===========================================
document.addEventListener('DOMContentLoaded', () => {
    showStep('email');
});