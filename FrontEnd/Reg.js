

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

form.addEventListener('submit', function(e){
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
if(valid){
            window.location.href = "Home.html" ;
}
     
    }
});
