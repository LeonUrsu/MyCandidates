// wwwroot/js/page-transition.js

document.addEventListener("DOMContentLoaded", function () {
    const overlay = document.getElementById('page-transition-overlay');
    if (!overlay) return;

    const isInternalNav = sessionStorage.getItem('isInternalNav');

    if (isInternalNav) {
        overlay.style.display = 'none';
        sessionStorage.removeItem('isInternalNav');
    } else {
        overlay.style.display = 'flex';
        overlay.classList.remove('hidden');

        // Dopo 1 secondo fa partire lo sbiadimento
        setTimeout(() => {
            overlay.classList.add('hidden');

            // Dopo altri 600ms lo nasconde del tutto
            setTimeout(() => {
                overlay.style.display = 'none';
            }, 600);
        }, 1000);
    }
});

// Intercettiamo i click sui link interni del sito
document.addEventListener('click', function (e) {
    const link = e.target.closest('a');
    
    // Se l'utente clicca su un link interno (stesso dominio, niente target="_blank" o ancore "#")
    if (link && link.href && link.hostname === window.location.hostname && !link.getAttribute('target') && !link.getAttribute('href').startsWith('#')) {
        // Memorizziamo che stiamo cambiando pagina tramite un link del sito
        sessionStorage.setItem('isInternalNav', 'true');
    }
});