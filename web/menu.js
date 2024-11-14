function toggleMenu() {
    const sideMenu = document.getElementById('sideMenu');
    const content = document.querySelector('.content');
    
    if (window.innerWidth <= 768) {
        if (sideMenu.style.left === '0px') {
            sideMenu.style.left = '-250px';
        } else {
            sideMenu.style.left = '0px';
        }
    } else {
        if (sideMenu.style.left === '-250px') {
            sideMenu.style.left = '0px';
            content.style.marginLeft = '250px';
        } else {
            sideMenu.style.left = '-250px';
            content.style.marginLeft = '0px';
        }
    }
}

function toggleDropdown() {
    document.getElementById("myDropdown").classList.toggle("show");
}

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

window.addEventListener('resize', function() {
    const sideMenu = document.getElementById('sideMenu');
    const content = document.querySelector('.content');
    const menuToggle = document.querySelector('.menu-toggle');

    if (window.innerWidth > 768) {
        menuToggle.style.display = 'none';
        sideMenu.style.left = '0px';
        content.style.marginLeft = '250px';
    } else {
        menuToggle.style.display = 'block';
        sideMenu.style.left = '-250px';
        content.style.marginLeft = '0px';
    }
});

document.addEventListener('DOMContentLoaded', function() {
    if (window.innerWidth > 768) {
        const menuToggle = document.querySelector('.menu-toggle');
        if (menuToggle) {
            menuToggle.style.display = 'none';
        }
    }
}); 