

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
