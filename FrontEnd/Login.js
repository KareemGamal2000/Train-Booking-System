const form = document.querySelector('form');
const email = document.getElementById('email');
const password = document.getElementById('password');

const emailError = document.getElementById('email-error');
const passwordError = document.getElementById('password-error');
const generalError = document.getElementById('general-error');

form.addEventListener('submit', async function (e) {
    e.preventDefault();

    let valid = true;
    emailError.textContent = "";
    passwordError.textContent = "";
    if (generalError) generalError.textContent = "";

    const emailPattern = /^[^ ]+@[^ ]+\.[a-z]{2,3}$/;
    if (email.value.trim() === "") {
        emailError.textContent = "يرجى إدخال البريد الإلكتروني";
        valid = false;
    } else if (!email.value.match(emailPattern)) {
        emailError.textContent = "صيغة البريد الإلكتروني غير صحيحة";
        valid = false;
    }

    const passwordPattern =
        /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%&*!?])[A-Za-z\d@#$%&*!?]{8,}$/;

    if (password.value.trim() === "") {
        passwordError.textContent = "يرجى إدخال كلمة المرور";
        valid = false;
    } else if (!passwordPattern.test(password.value)) {
        passwordError.textContent =
            "يجب أن تحتوي على 8 أحرف وحرف كبير وصغير ورقم ورمز";
        valid = false;
    }

    if (!valid) return;

    try {
    
        const res = await fetch('https://localhost:7192/api/Auth/Login', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                email: email.value,
                password: password.value
            })
        });

        const data = await res.json().catch(() => ({}));

        console.log('Login response:', {
            status: res.status,
            isAuthenticated: data.isAuthenticated,
            hasToken: !!data.token,
            email: data.email,
            username: data.userName || data.username
        });
        
        if (res.ok && (data.isAuthenticated === true || data.isAuthenticated === "True")) {
            // حفظ Token في localStorage
            if (data.token) {
                localStorage.setItem('authToken', data.token);
                console.log('✓ Token saved successfully');
            } else {
                console.error('⚠ No token in response!');
            }
            
            // حفظ معلومات المستخدم الأخرى إذا لزم الأمر
            if (data.email) {
                localStorage.setItem('userEmail', data.email);
            }
            if (data.userName || data.username) {
                localStorage.setItem('username', data.userName || data.username);
            }
            
            // التحقق من وجود حجز معلق (إذا جاء من صفحة المقاعد)
            const pendingSeat = localStorage.getItem('pendingSeatSelection');
            if (pendingSeat) {
                // حذف القيمة المعلقة والعودة لصفحة المقاعد
                localStorage.removeItem('pendingSeatSelection');
                window.location.href = "Seat.html";
            } else {
                // الذهاب للصفحة الرئيسية بشكل افتراضي
                window.location.href = "Home.html";
            }
        } else {
            if (generalError) {
                generalError.textContent = data.message || "فشل تسجيل الدخول";
            } else {
     
                console.error(data.message || "فشل تسجيل الدخول");
            }
        }
    } catch (err) {
        console.error('Login error:', err);
        if (generalError) {
            generalError.textContent = "لا يمكن الاتصال بالخادم الآن (تأكد من تشغيل الـ API)";
        } else {
   
            console.error("لا يمكن الاتصال بالخادم الآن");
        }
    }
});