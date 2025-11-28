

const form = document.querySelector('form');
const email = document.getElementById('email');
const password = document.getElementById('password');

const emailError = document.getElementById('email-error');
const passwordError = document.getElementById('password-error');

form.addEventListener('submit', function(e){
    e.preventDefault();

    let valid = true;

    
    emailError.textContent = "";
    passwordError.textContent = "";


    const emailPattern = /^[^ ]+@[^ ]+\.[a-z]{2,3}$/;
    if(email.value.trim() === ""){
        emailError.textContent = "يرجى إدخال البريد الإلكتروني";
        valid = false;
    } 
    else if(!email.value.match(emailPattern)){
        emailError.textContent = "صيغة البريد الإلكتروني غير صحيحة";
        valid = false;
    }

    
    const passwordPattern =
        /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%&*!?])[A-Za-z\d@#$%&*!?]{8,}$/;

    if(password.value.trim() === ""){
        passwordError.textContent = "يرجى إدخال كلمة المرور";
        valid = false;
    }
    else if(!passwordPattern.test(password.value)){
        passwordError.textContent =
            "يجب أن تحتوي على 8 أحرف وحرف كبير وصغير ورقم ورمز";
        valid = false;
    }

    if(valid){
        window.location.href = "Home.html"; 
    }
});
