function toggleMenu() {
    const sideMenu = document.getElementById('sideMenu');
    const content = document.querySelector('.content');
    
    if (window.innerWidth <= 768) {
        // Mobilna wersja
        if (sideMenu.style.left === '0px') {
            sideMenu.style.left = '-250px';
        } else {
            sideMenu.style.left = '0px';
        }
    } else {
        // Desktopowa wersja
        if (sideMenu.style.left === '-250px') {
            sideMenu.style.left = '0px';
            content.style.marginLeft = '250px';
        } else {
            sideMenu.style.left = '-250px';
            content.style.marginLeft = '0px';
        }
    }
}

// Obsługa dropdownu w nawigacji
function toggleDropdown() {
    document.getElementById("myDropdown").classList.toggle("show");
}

// Zamykanie dropdownu gdy kliknie się poza nim
window.onclick = function(event) {
    if (!event.target.matches('.dropbtn') && !event.target.matches('.user-icon')) {
        var dropdowns = document.getElementsByClassName("dropdown-content");
        for (var i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
}

// Obsługa responsywności
window.addEventListener('resize', function() {
    const sideMenu = document.getElementById('sideMenu');
    const content = document.querySelector('.content');
    const menuToggle = document.querySelector('.menu-toggle');

    if (window.innerWidth > 768) {
        // Wersja desktopowa
        menuToggle.style.display = 'none';
        sideMenu.style.left = '0px';
        content.style.marginLeft = '250px';
    } else {
        // Wersja mobilna
        menuToggle.style.display = 'block';
        sideMenu.style.left = '-250px';
        content.style.marginLeft = '0px';
    }
});

// Inicjalizacja przy załadowaniu strony
document.addEventListener('DOMContentLoaded', function() {
    if (window.innerWidth > 768) {
        const menuToggle = document.querySelector('.menu-toggle');
        if (menuToggle) {
            menuToggle.style.display = 'none';
        }
    }
}); 