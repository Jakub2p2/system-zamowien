function loadDostawy() {
    fetch('get_dostawy.php')
        .then(response => response.json())
        .then(data => {
            const tbody = document.querySelector('.clients-table tbody');
            tbody.innerHTML = '';
            
            data.forEach(dostawa => {
                const row = `
                    <tr>
                        <td>${escapeHtml(dostawa.nazwa)}</td>
                        <td>${escapeHtml(dostawa.cena_za_kg)}</td>
                        <td>${escapeHtml(dostawa.cena_ubezpieczenia)}</td>
                        <td>${escapeHtml(dostawa.link_do_śledzenia)}</td>
                        <td>
                            <button onclick='editDostawa(${dostawa.id})'>Edytuj</button>
                            <button onclick='deleteDostawa(${dostawa.id})'>Usuń</button>
                        </td>
                    </tr>
                `;
                tbody.innerHTML += row;
            });
        })
        .catch(error => {
            console.error('Error:', error);
            alert('Wystąpił błąd podczas ładowania danych');
        });
}

function editDostawa(id) {
    fetch('get_dostawa.php?id=' + encodeURIComponent(id))
        .then(response => response.json())
        .then(data => {
            document.getElementById('modal-id').value = data.id;
            document.getElementById('modal-nazwa').value = data.nazwa;
            document.getElementById('modal-cena_za_kg').value = data.cena_za_kg;
            document.getElementById('modal-cena_ubezpieczenia').value = data.cena_ubezpieczenia;
            document.getElementById('modal-link_do_śledzenia').value = data.link_do_śledzenia;
            
            document.getElementById('modalTitle').textContent = 'Edytuj dostawcę';
            openModal();
        })
        .catch(error => {
            console.error('Error:', error);
            alert('Wystąpił błąd podczas pobierania danych dostawcy');
        });
}

function deleteDostawa(id) {
    if (confirm('Czy na pewno chcesz usunąć tego dostawcę?')) {
        const formData = new FormData();
        formData.append('id', id);

        fetch('delete_dostawa.php', {
            method: 'POST',
            body: formData
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                loadDostawy();
                alert('Dostawca został usunięty');
            } else {
                throw new Error(data.message || 'Wystąpił błąd podczas usuwania');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            alert('Wystąpił błąd: ' + error.message);
        });
    }
}

function openModal() {
    const modal = document.getElementById('userModal');
    if (modal) {
        modal.style.display = 'block';
    }
}

function closeModal() {
    const modal = document.getElementById('userModal');
    const form = document.getElementById('addUserForm');
    const modalId = document.getElementById('modal-id');
    const modalTitle = document.getElementById('modalTitle');
    
    if (modal) modal.style.display = 'none';
    if (form) form.reset();
    if (modalId) modalId.value = '';
    if (modalTitle) modalTitle.textContent = 'Dodaj nowego dostawcę';
}

function escapeHtml(text) {
    if (text === null || text === undefined) return '';
    const div = document.createElement('div');
    div.textContent = text;
    return div.innerHTML;
}

document.addEventListener('DOMContentLoaded', function() {
    const addUserForm = document.getElementById('addUserForm');
    if (addUserForm) {
        addUserForm.addEventListener('submit', function(e) {
            e.preventDefault();
            
            const formData = new FormData(this);
            const isEdit = formData.get('id') !== '';

            fetch(isEdit ? 'update_dostawa.php' : 'add_dostawa.php', {
                method: 'POST',
                body: formData
            })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    loadDostawy();
                    closeModal();
                    alert(isEdit ? 'Dostawca został zaktualizowany' : 'Dostawca został dodany');
                } else {
                    throw new Error(data.message || 'Wystąpił błąd podczas zapisywania');
                }
            })
            .catch(error => {
                console.error('Error:', error);
                alert('Wystąpił błąd: ' + error.message);
            });
        });
    }

    const closeModalBtn = document.querySelector('.close');
    if (closeModalBtn) {
        closeModalBtn.onclick = closeModal;
    }

    window.onclick = function(event) {
        const modal = document.getElementById('userModal');
        if (event.target === modal) {
            closeModal();
        }
    };

    loadDostawy();
});

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