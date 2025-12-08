

const form = document.getElementById('register-form');
const firstName = document.getElementById('firstName');
const lastName = document.getElementById('lastName');
const phone = document.getElementById('phone');
const email = document.getElementById('email');
const password = document.getElementById('password');

const firstNameError = document.getElementById('firstName-error');
const lastNameError = document.getElementById('lastName-error');
const phoneError = document.getElementById('phone-error');
const emailError = document.getElementById('email-error');
const passwordError = document.getElementById('password-error');

form.addEventListener('submit', async function(e){
    e.preventDefault();
    let valid = true;

    
    firstNameError.textContent = "";
    lastNameError.textContent = "";
    phoneError.textContent = "";
    emailError.textContent = "";
    passwordError.textContent = "";

    
    if(firstName.value.trim() === ""){
        firstNameError.textContent = "يرجى إدخال الاسم الأول";
        valid = false;
    }


    if(lastName.value.trim() === ""){
        lastNameError.textContent = "يرجى إدخال الاسم الأخير";
        valid = false;
    }

    
    const phonePattern = /^[0-9]{10,15}$/;
    if(phone.value.trim() === ""){
        phoneError.textContent = "يرجى إدخال رقم الموبايل";
        valid = false;
    } else if(!phone.value.match(phonePattern)){
        phoneError.textContent = "رقم الموبايل غير صالح";
        valid = false;
    }

    
    const emailPattern = /^[^ ]+@[^ ]+\.[a-z]{2,3}$/;
    if(email.value.trim() === ""){
        emailError.textContent = "يرجى إدخال البريد الإلكتروني";
        valid = false;
    } else if(!email.value.match(emailPattern)){
        emailError.textContent = "صيغة البريد الإلكتروني غير صحيحة";
        valid = false;
    }

    
    const passwordPattern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%&*!?])[A-Za-z\d@#$%&*!?]{8,}$/;
    if(password.value.trim() === ""){
        passwordError.textContent = "يرجى إدخال كلمة المرور";
        valid = false;
    } else if(!passwordPattern.test(password.value)){
        passwordError.textContent = "يجب أن تحتوي على 8 أحرف وحرف كبير وصغير ورقم ورمز";
        valid = false;
    }

if(valid){
    const submitBtn = document.querySelector('button[type="submit"]');
    submitBtn.textContent = 'جاري الإنشاء...';
    submitBtn.disabled = true;
    
    try {
        console.log('📤 Sending registration data...');
        const requestBody = {
            firstName: firstName.value.trim(),
            lastName: lastName.value.trim(),
            phoneNumber: phone.value.replace(/\s/g, ''),
            email: email.value.trim(),
            password: password.value
        };
        console.log('Request:', requestBody);
        
        const res = await fetch('https://localhost:7192/api/Auth/Register', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(requestBody)
        });
        
        console.log('Response status:', res.status);
        
        if (!res.ok) {
            const errorData = await res.json().catch(() => ({ message: 'خطأ في الاتصال بالخادم' }));
            alert(errorData.message || "فشل في التسجيل");
            return;
        }
        
        const data = await res.json();
        console.log('Response data:', data);
        
        if (data.isAuthenticated) {
            alert("تم التسجيل بنجاح! يرجى تسجيل الدخول");
            window.location.href = "Login.html";
        } else {
            alert(data.message || "فشل في التسجيل");
        }
    } catch (err) {
        console.error('Registration error:', err);
        alert("لا يمكن الاتصال بالخادم. تأكد من تشغيل الـ API");
    } finally {
        submitBtn.textContent = 'تسجيل';
        submitBtn.disabled = false;
    }
}

});
