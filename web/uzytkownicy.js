let currentSort = {
    column: 'nazwisko',
    direction: 'ASC'
};

function sortTable(column) {
    if (currentSort.column === column) {
        currentSort.direction = currentSort.direction === 'ASC' ? 'DESC' : 'ASC';
    } else {
        currentSort.column = column;
        currentSort.direction = 'ASC';
    }

    document.querySelectorAll('.arrow').forEach(arrow => arrow.classList.remove('active'));
    const th = event.currentTarget;
    const arrow = currentSort.direction === 'ASC' ? 
        th.querySelector('.arrow.up') : 
        th.querySelector('.arrow.down');
    if (arrow) {
        arrow.classList.add('active');
    }

    loadUsers();
}

function loadUsers() {
    const urlParams = new URLSearchParams(window.location.search);
    const page = urlParams.get('page') || 1;
    const per_page = urlParams.get('per_page') || 10;

    const searchParams = new URLSearchParams(window.location.search);
    searchParams.set('column', currentSort.column);
    searchParams.set('direction', currentSort.direction);

    fetch('get_users.php?' + searchParams.toString())
        .then(response => response.json())
        .then(data => {
            const tbody = document.querySelector('.clients-table tbody');
            tbody.innerHTML = '';
            data.users.forEach(user => {
                const row = `
                    <tr>
                        <td>${escapeHtml(user.imie)}</td>
                        <td>${escapeHtml(user.nazwisko)}</td>
                        <td>${escapeHtml(user.login)}</td>
                        <td>${escapeHtml(user.email)}</td>
                        <td>${escapeHtml(user.ranga)}</td>
                        <td>
                            <button onclick='editUser(${user.id})'>Edytuj</button>
                            <button onclick='deleteUser(${user.id})'>Usuń</button>
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

function resetForm() {
    document.getElementById('searchForm').reset();
    const urlParams = new URLSearchParams(window.location.search);
    urlParams.delete('imie');
    urlParams.delete('nazwisko');
    urlParams.delete('login');
    urlParams.delete('email');
    urlParams.delete('ranga');
    window.location.search = urlParams.toString();
}

function editUser(id) {
    fetch('get_user.php?id=' + encodeURIComponent(id))
        .then(response => response.json())
        .then(data => {
            document.getElementById('modal-id').value = data.id;
            document.getElementById('modal-imie').value = data.imie;
            document.getElementById('modal-nazwisko').value = data.nazwisko;
            document.getElementById('modal-login').value = data.login;
            document.getElementById('modal-email').value = data.email;
            document.getElementById('modal-ranga').value = data.ranga;
            
            const changePasswordGroup = document.getElementById('changePasswordGroup');
            const passwordFields = document.querySelectorAll('.password-group');
            
            if (changePasswordGroup) changePasswordGroup.style.display = 'block';
            passwordFields.forEach(field => field.style.display = 'none');
            
            document.getElementById('modal-haslo').value = '';
            document.getElementById('modal-haslo2').value = '';
            document.getElementById('modal-haslo').required = false;
            document.getElementById('modal-haslo2').required = false;
            
            document.getElementById('modalTitle').textContent = 'Edytuj użytkownika';
            openModal();
        })
        .catch(error => {
            console.error('Error:', error);
            alert('Wystąpił błąd podczas pobierania danych użytkownika');
        });
}

function deleteUser(id) {
    if (confirm('Czy na pewno chcesz usunąć tego użytkownika?')) {
        const formData = new FormData();
        formData.append('id', id);

        fetch('delete_user.php', {
            method: 'POST',
            body: formData
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                loadUsers();
                alert('Użytkownik został usunięty');
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
        
        const changePasswordGroup = document.getElementById('changePasswordGroup');
        const passwordFields = document.querySelectorAll('.password-group');
        const isEdit = document.getElementById('modal-id').value !== '';
        
        if (changePasswordGroup) {
            changePasswordGroup.style.display = isEdit ? 'block' : 'none';
        }
        
        passwordFields.forEach(field => {
            field.style.display = isEdit ? 'none' : 'block';
            const input = field.querySelector('input');
            if (input) input.required = !isEdit;
        });
    } else {
        console.error('Modal element not found');
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
    if (modalTitle) modalTitle.textContent = 'Dodaj nowego użytkownika';
}

function goToPage(page) {
    const urlParams = new URLSearchParams(window.location.search);
    urlParams.set('page', page);
    window.location.search = urlParams.toString();
}

function changeRecordsPerPage(perPage) {
    const urlParams = new URLSearchParams(window.location.search);
    urlParams.set('per_page', perPage);
    urlParams.set('page', '1');
    window.location.search = urlParams.toString();
}

function escapeHtml(text) {
    if (text === null || text === undefined) return '';
    const div = document.createElement('div');
    div.textContent = text;
    return div.innerHTML;
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

document.addEventListener('DOMContentLoaded', function() {
    const addUserForm = document.getElementById('addUserForm');
    if (addUserForm) {
        addUserForm.addEventListener('submit', function(e) {
            e.preventDefault();
            
            const formData = new FormData(this);
            const isEdit = formData.get('id') !== '';
            
            if (!isEdit && formData.get('haslo') !== formData.get('haslo2')) {
                alert('Hasła nie są identyczne!');
                return;
            }

            fetch(isEdit ? 'update_user.php' : 'add_user.php', {
                method: 'POST',
                body: formData
            })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    loadUsers();
                    closeModal();
                    alert(isEdit ? 'Użytkownik został zaktualizowany' : 'Użytkownik został dodany');
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

    loadUsers();

    const changePasswordCheckbox = document.getElementById('changePassword');
    if (changePasswordCheckbox) {
        changePasswordCheckbox.addEventListener('change', function() {
            const passwordFields = document.querySelectorAll('.password-group');
            passwordFields.forEach(field => {
                field.style.display = this.checked ? 'block' : 'none';
                const input = field.querySelector('input');
                if (input) input.required = this.checked;
            });
        });
    }
}); 