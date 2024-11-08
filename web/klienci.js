function toggleDropdown() {
    document.getElementById("myDropdown").classList.toggle("show");
}

window.onclick = function(event) {
    if (!event.target.closest(".user-info")) {
        var dropdowns = document.getElementsByClassName("dropdown-content");
        for (var i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains("show")) {
                openDropdown.classList.remove("show");
            }
        }
    }
}

function openModal() {
    document.getElementById('clientModal').style.display = 'block';
    if (!document.getElementById('modal-id').value) {
        document.getElementById('addClientForm').reset();
        document.getElementById('modalTitle').textContent = 'Dodaj nowego klienta';
    }
}

function closeModal() {
    document.getElementById('clientModal').style.display = 'none';
}

window.onclick = function(event) {
    var modal = document.getElementById('clientModal');
    if (event.target == modal) {
        modal.style.display = 'none';
    }
}

function editClient(id) {
    fetch('pobierz_klienta.php?id=' + id)
        .then(response => response.json())
        .then(data => {
            document.getElementById('modal-id').value = data.id;
            document.getElementById('modal-nazwa').value = data.nazwa;
            document.getElementById('modal-nip').value = data.nip;
            document.getElementById('modal-regon').value = data.regon;
            document.getElementById('modal-pesel').value = data.pesel;
            document.getElementById('modal-email').value = data.email;
            document.getElementById('modal-telefon').value = data.telefon;
            document.getElementById('modal-adres').value = data.adres;
            openModal();
        })
        .catch(error => {
            console.error('Error:', error);
            alert('Wystąpił błąd podczas pobierania danych klienta');
        });
}

document.getElementById('addClientForm').addEventListener('submit', function(e) {
    e.preventDefault();
    
    const formData = new FormData(this);
    const clientId = document.getElementById('modal-id').value;
    const url = clientId ? 'aktualizuj_klienta.php' : 'dodaj_klienta.php';
    
    fetch(url, {
        method: 'POST',
        body: formData
    })
    .then(response => response.json())
    .then(data => {
        if (data.success) {
            alert(clientId ? 'Klient został zaktualizowany!' : 'Klient został dodany pomyślnie!');
            location.reload();
        } else {
            alert('Wystąpił błąd: ' + data.message);
        }
        closeModal();
    })
    .catch(error => {
        alert('Wystąpił błąd podczas ' + (clientId ? 'aktualizacji' : 'dodawania') + ' klienta');
        console.error('Error:', error);
    });
});

function deleteClient(id) {
    if (confirm('Czy na pewno chcesz usunąć tego klienta?')) {
        fetch('usun_klienta.php', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
            },
            body: 'id=' + id
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                alert('Klient został usunięty!');
                location.reload();
            } else {
                alert('Wystąpił błąd: ' + data.message);
            }
        })
        .catch(error => {
            console.error('Error:', error);
            alert('Wystąpił błąd podczas usuwania klienta');
        });
    }
}

let currentSort = {
    column: 'nazwa',
    direction: 'ASC'
};

function sortTable(column, event) {
    if (currentSort.column === column) {
        currentSort.direction = currentSort.direction === 'ASC' ? 'DESC' : 'ASC';
    } else {
        currentSort.column = column;
        currentSort.direction = 'ASC';
    }

    document.querySelectorAll('.arrow').forEach(arrow => arrow.classList.remove('active'));
    if (event && event.currentTarget) {
        const th = event.currentTarget;
        const arrow = currentSort.direction === 'ASC' ? 
            th.querySelector('.arrow.up') : 
            th.querySelector('.arrow.down');
        if (arrow) {
            arrow.classList.add('active');
        }
    }

    fetch(`pobierz_posortowanych_klientow.php?column=${encodeURIComponent(column)}&direction=${encodeURIComponent(currentSort.direction)}`)
        .then(response => {
            if (!response.ok) {
                return response.text().then(text => {
                    throw new Error('Błąd serwera: ' + text);
                });
            }
            return response.json();
        })
        .then(data => {
            console.log('Otrzymane dane:', data);
            
            const tbody = document.querySelector('.clients-table tbody');
            tbody.innerHTML = '';

            data.forEach(client => {
                const row = `
                    <tr>
                        <td>${escapeHtml(client.nazwa || '')}</td>
                        <td>${escapeHtml(client.nip || '')}</td>
                        <td>${escapeHtml(client.regon || '')}</td>
                        <td>${escapeHtml(client.pesel || '')}</td>
                        <td>${escapeHtml(client.email || '')}</td>
                        <td>${escapeHtml(client.telefon || '')}</td>
                        <td>${escapeHtml(client.adres || '')}</td>
                        <td>
                            <button onclick='editClient(${client.id})'>Edytuj</button>
                            <button onclick='deleteClient(${client.id})'>Usuń</button>
                        </td>
                    </tr>
                `;
                tbody.innerHTML += row;
            });
        })
        .catch(error => {
            console.error('Error:', error);
            alert(error.message);
        });
}

function escapeHtml(text) {
    if (text === null || text === undefined) return '';
    const div = document.createElement('div');
    div.textContent = text;
    return div.innerHTML;
}

function goToPage(page) {
    window.location.href = `?page=${page}&per_page=<?php echo $records_per_page; ?>`;
}

function changeRecordsPerPage(perPage) {
    window.location.href = `?page=1&per_page=${perPage}`;
}

function resetForm() {
    if (document.getElementById('regon')) document.getElementById('regon').value = '';
    if (document.getElementById('pesel')) document.getElementById('pesel').value = '';
    if (document.getElementById('email')) document.getElementById('email').value = '';
    if (document.getElementById('telefon')) document.getElementById('telefon').value = '';
    if (document.getElementById('adres')) document.getElementById('adres').value = '';
    if (document.getElementById('nazwa')) document.getElementById('nazwa').value = '';
    if (document.getElementById('nip')) document.getElementById('nip').value = '';
    window.location.href = 'klienci.php';
}

function toggleMenu() {
    console.log('Menu clicked');
    const sideMenu = document.getElementById('sideMenu');
    if (sideMenu) {
        sideMenu.classList.toggle('active');
        console.log('Menu toggled', sideMenu.classList.contains('active'));
    } else {
        console.log('Menu element not found');
    }
}

document.addEventListener('click', function(event) {
    const sideMenu = document.getElementById('sideMenu');
    const hamburger = document.querySelector('.hamburger-menu');
    
    if (sideMenu && hamburger) {
        if (!sideMenu.contains(event.target) && 
            !hamburger.contains(event.target) && 
            sideMenu.classList.contains('active')) {
            sideMenu.classList.remove('active');
        }
    }
});

document.addEventListener('DOMContentLoaded', function() {
    const menuBtn = document.getElementById('mobile-menu-btn');
    const sideMenu = document.getElementById('sideMenu');

    menuBtn.addEventListener('click', function() {
        sideMenu.classList.toggle('active');
    });

    document.addEventListener('click', function(event) {
        if (!sideMenu.contains(event.target) && 
            !menuBtn.contains(event.target) && 
            sideMenu.classList.contains('active')) {
            sideMenu.classList.remove('active');
        }
    });
});