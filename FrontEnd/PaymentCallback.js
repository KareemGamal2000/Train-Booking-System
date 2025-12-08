// PaymentCallback.js - معالجة callback من Paymob
const API_BASE_URL = 'https://localhost:7192/api';

// عناصر الواجهة
const loadingState = document.getElementById('loadingState');
const successState = document.getElementById('successState');
const failureState = document.getElementById('failureState');
const errorState = document.getElementById('errorState');

// الأزرار
const viewBookingBtn = document.getElementById('viewBookingBtn');
const goHomeBtn = document.getElementById('goHomeBtn');
const retryPaymentBtn = document.getElementById('retryPaymentBtn');
const goHomeFailBtn = document.getElementById('goHomeFailBtn');
const contactSupportBtn = document.getElementById('contactSupportBtn');
const goHomeErrorBtn = document.getElementById('goHomeErrorBtn');

// بيانات الدفع من الـ URL
let callbackData = {};

// عند تحميل الصفحة
document.addEventListener('DOMContentLoaded', () => {
    console.log('PaymentCallback page loaded');
    console.log('Full URL:', window.location.href);
    
    // استخراج بيانات callback من الـ URL
    parseCallbackData();
    
    // معالجة الـ callback
    processPaymentCallback();
    
    // إعداد الأزرار
    setupEventListeners();
});

/**
 * استخراج بيانات callback من URL parameters
 */
function parseCallbackData() {
    const urlParams = new URLSearchParams(window.location.search);
    
    callbackData = {
        id: urlParams.get('id'),
        pending: urlParams.get('pending'),
        amount_cents: urlParams.get('amount_cents') ? parseInt(urlParams.get('amount_cents')) : null,
        success: urlParams.get('success') === 'true',
        is_auth: urlParams.get('is_auth'),
        is_capture: urlParams.get('is_capture'),
        is_standalone_payment: urlParams.get('is_standalone_payment'),
        is_voided: urlParams.get('is_voided'),
        is_refunded: urlParams.get('is_refunded'),
        is_3d_secure: urlParams.get('is_3d_secure'),
        integration_id: urlParams.get('integration_id'),
        profile_id: urlParams.get('profile_id'),
        has_parent_transaction: urlParams.get('has_parent_transaction'),
        order: urlParams.get('order'),
        created_at: urlParams.get('created_at'),
        currency: urlParams.get('currency'),
        merchant_commission: urlParams.get('merchant_commission') ? parseInt(urlParams.get('merchant_commission')) : null,
        accept_fees: urlParams.get('accept_fees') ? parseInt(urlParams.get('accept_fees')) : null,
        discount_details: urlParams.get('discount_details'),
        is_void: urlParams.get('is_void'),
        is_refund: urlParams.get('is_refund'),
        error_occured: urlParams.get('error_occured'),
        refunded_amount_cents: urlParams.get('refunded_amount_cents') ? parseInt(urlParams.get('refunded_amount_cents')) : null,
        captured_amount: urlParams.get('captured_amount') ? parseInt(urlParams.get('captured_amount')) : null,
        updated_at: urlParams.get('updated_at'),
        is_settled: urlParams.get('is_settled'),
        bill_balanced: urlParams.get('bill_balanced'),
        is_bill: urlParams.get('is_bill'),
        owner: urlParams.get('owner'),
        source_data_type: urlParams.get('source_data.type'),
        source_data_pan: urlParams.get('source_data.pan'),
        source_data_sub_type: urlParams.get('source_data.sub_type'),
        acq_response_code: urlParams.get('acq_response_code'),
        txn_response_code: urlParams.get('txn_response_code'),
        hmac: urlParams.get('hmac')
    };
    
    console.log('Parsed callback data:', callbackData);
}

/**
 * معالجة callback وإرساله للـ backend
 */
async function processPaymentCallback() {
    try {
        // التحقق من وجود البيانات الأساسية
        if (!callbackData.id || !callbackData.order) {
            showError('بيانات الدفع غير مكتملة');
            return;
        }

        // تحويل البيانات للصيغة المطلوبة من الـ backend
        const backendPayload = {
            order: callbackData.order,
            success: callbackData.success,
            amount_cents: callbackData.amount_cents,
            transaction_id: callbackData.id,
            id: callbackData.id,
            hmac: callbackData.hmac,
            error_occured: callbackData.error_occured || (callbackData.success ? 'false' : 'true'),
            
            // البيانات الإضافية للـ HMAC verification
            created_at: callbackData.created_at,
            currency: callbackData.currency,
            has_parent_transaction: callbackData.has_parent_transaction,
            integration_id: callbackData.integration_id,
            is_3d_secure: callbackData.is_3d_secure,
            is_auth: callbackData.is_auth,
            is_capture: callbackData.is_capture,
            is_refunded: callbackData.is_refunded,
            is_standalone_payment: callbackData.is_standalone_payment,
            is_voided: callbackData.is_voided,
            owner: callbackData.owner,
            pending: callbackData.pending,
            source_data_pan: callbackData.source_data_pan,
            source_data_sub_type: callbackData.source_data_sub_type,
            source_data_type: callbackData.source_data_type,
            
            // بيانات إضافية
            profile_id: callbackData.profile_id,
            merchant_commission: callbackData.merchant_commission,
            accept_fees: callbackData.accept_fees,
            discount_details: callbackData.discount_details,
            is_void: callbackData.is_void,
            is_refund: callbackData.is_refund,
            refunded_amount_cents: callbackData.refunded_amount_cents,
            captured_amount: callbackData.captured_amount,
            updated_at: callbackData.updated_at,
            is_settled: callbackData.is_settled,
            bill_balanced: callbackData.bill_balanced,
            is_bill: callbackData.is_bill,
            acq_response_code: callbackData.acq_response_code,
            txn_response_code: callbackData.txn_response_code
        };

        console.log('Sending to backend:', backendPayload);

        // إرسال للـ backend
        const response = await fetch(`${API_BASE_URL}/Payment/callback`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(backendPayload)
        });

        console.log('Response status:', response.status);
        const result = await response.json();
        console.log('Backend response:', result);

        // عرض النتيجة للمستخدم
        if (response.ok && callbackData.success) {
            showSuccess();
        } else if (callbackData.success === false) {
            showFailure(result.message);
        } else {
            showError(result.message || 'حدث خطأ أثناء معالجة الدفع');
        }

    } catch (error) {
        console.error('Error processing payment callback:', error);
        showError('حدث خطأ في الاتصال بالخادم. برجاء المحاولة مرة أخرى.');
    }
}

/**
 * عرض شاشة النجاح
 */
function showSuccess() {
    loadingState.style.display = 'none';
    failureState.style.display = 'none';
    errorState.style.display = 'none';
    successState.style.display = 'block';

    // عرض تفاصيل الدفع
    const amount = callbackData.amount_cents ? (callbackData.amount_cents / 100).toFixed(2) : '0.00';
    const paymentMethod = callbackData.source_data_sub_type || 'بطاقة';
    const lastDigits = callbackData.source_data_pan || '****';
    const transactionId = callbackData.id;

    document.getElementById('paymentDetails').innerHTML = `
        <div class="detail-row">
            <span class="detail-label">المبلغ المدفوع:</span>
            <span class="detail-value">${amount} جنيه</span>
        </div>
        <div class="detail-row">
            <span class="detail-label">طريقة الدفع:</span>
            <span class="detail-value">${paymentMethod} (${lastDigits})</span>
        </div>
        <div class="detail-row">
            <span class="detail-label">رقم العملية:</span>
            <span class="detail-value">${transactionId}</span>
        </div>
        <div class="detail-row">
            <span class="detail-label">رقم الطلب:</span>
            <span class="detail-value">${callbackData.order}</span>
        </div>
        ${callbackData.txn_response_code ? `
        <div class="detail-row">
            <span class="detail-label">كود التأكيد:</span>
            <span class="detail-value">${callbackData.txn_response_code}</span>
        </div>
        ` : ''}
    `;
}

/**
 * عرض شاشة الفشل
 */
function showFailure(backendMessage = null) {
    loadingState.style.display = 'none';
    successState.style.display = 'none';
    errorState.style.display = 'none';
    failureState.style.display = 'block';

    // عرض سبب الفشل إن وجد
    const errorMessage = backendMessage || callbackData.error_occured || 'فشلت عملية الدفع';
    document.getElementById('failureMessage').textContent = errorMessage;

    // عرض التفاصيل
    const amount = callbackData.amount_cents ? (callbackData.amount_cents / 100).toFixed(2) : '0.00';
    document.getElementById('failureDetails').innerHTML = `
        <div class="detail-row">
            <span class="detail-label">المبلغ:</span>
            <span class="detail-value">${amount} جنيه</span>
        </div>
        <div class="detail-row">
            <span class="detail-label">رقم الطلب:</span>
            <span class="detail-value">${callbackData.order || 'غير متوفر'}</span>
        </div>
        <div class="detail-row">
            <span class="detail-label">رقم العملية:</span>
            <span class="detail-value">${callbackData.id || 'غير متوفر'}</span>
        </div>
        ${callbackData.txn_response_code ? `
        <div class="detail-row">
            <span class="detail-label">كود الرد:</span>
            <span class="detail-value">${callbackData.txn_response_code}</span>
        </div>
        ` : ''}
    `;

    // حفظ order للمحاولة مرة أخرى
    if (callbackData.order) {
        sessionStorage.setItem('failedPaymentOrder', callbackData.order);
    }
}

/**
 * عرض شاشة الخطأ
 */
function showError(message) {
    loadingState.style.display = 'none';
    successState.style.display = 'none';
    failureState.style.display = 'none';
    errorState.style.display = 'block';

    document.getElementById('errorMessage').textContent = message;
}

/**
 * إعداد مستمعي الأحداث للأزرار
 */
function setupEventListeners() {
    // أزرار النجاح
    if (viewBookingBtn) {
        viewBookingBtn.addEventListener('click', () => {
            window.location.href = 'MyBookings.html';
        });
    }

    if (goHomeBtn) {
        goHomeBtn.addEventListener('click', () => {
            window.location.href = 'Home.html';
        });
    }

    // أزرار الفشل
    if (retryPaymentBtn) {
        retryPaymentBtn.addEventListener('click', () => {
            // استرجاع بيانات الحجز ومحاولة الدفع مرة أخرى
            const bookingId = extractBookingIdFromOrder(callbackData.order);
            if (bookingId) {
                window.location.href = `BookingSummary.html?bookingId=${bookingId}`;
            } else {
                window.location.href = 'MyBookings.html';
            }
        });
    }

    if (goHomeFailBtn) {
        goHomeFailBtn.addEventListener('click', () => {
            window.location.href = 'Home.html';
        });
    }

    // أزرار الخطأ
    if (contactSupportBtn) {
        contactSupportBtn.addEventListener('click', () => {
            window.location.href = 'Contact.html';
        });
    }

    if (goHomeErrorBtn) {
        goHomeErrorBtn.addEventListener('click', () => {
            window.location.href = 'Home.html';
        });
    }
}

/**
 * استخراج Booking ID من Order
 * Order format: {BookingID}_{Timestamp}
 */
function extractBookingIdFromOrder(order) {
    if (!order) return null;
    
    const parts = order.split('_');
    if (parts.length > 0) {
        return parts[0];
    }
    
    return null;
}

/**
 * الحصول على التوكن من localStorage
 */
function getAuthToken() {
    return localStorage.getItem('authToken');
}
