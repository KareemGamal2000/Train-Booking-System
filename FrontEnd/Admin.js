


const pieCtx = document.getElementById("bookingChart").getContext("2d");

new Chart(pieCtx, {
    type: "pie",
    data: {
        labels: ["قاموا بالحجز", "مسجلين فقط"],
        datasets: [{
            data: [700, 500],
            backgroundColor: ["#d4af37", "#b38b2a"]
        }]
    },
    options: {
        responsive: true,
    }
});


const genderCtx = document.getElementById("genderChart").getContext("2d");

new Chart(genderCtx, {
    type: "bar",
    data: {
        labels: ["ذكور", "إناث"],
        datasets: [{
            label: "عدد المستخدمين",
            data: [800, 400], 
            backgroundColor: ["#d4af37", "#b38b2a"]
        }]
    },
    options: {
        responsive: true,
        scales: {
            y: { beginAtZero: true }
        }
    }
});
