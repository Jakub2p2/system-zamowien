document.addEventListener('DOMContentLoaded', function() {
    const form = document.getElementById('addPackageForm');
    if (form) {
        form.addEventListener('submit', function(e) {
            e.preventDefault();
            
            const submitButton = this.querySelector('button[type="submit"]');
            if (submitButton.disabled) return;
            
            submitButton.disabled = true;
            
            const formData = new FormData(this);
            savePackage(formData)
                .finally(() => {
                    submitButton.disabled = false;
                });
        });
    }
});

async function savePackage(formData) {
    const url = formData.get('id') ? 'update_package.php' : 'add_package.php';
    
    try {
        const response = await fetch(url, {
            method: 'POST',
            body: formData
        });
        const data = await response.json();
        
        if (data.success) {
            closeModal();
            location.reload();
        } else {
            alert(data.message || 'Wystąpił błąd podczas zapisywania paczki.');
        }
    } catch (error) {
        console.error('Error:', error);
        alert('Wystąpił błąd podczas zapisywania paczki.');
    }
}

function openModal() {
    const modal = document.getElementById('packageModal');
    modal.style.display = 'block';
}

function closeModal() {
    const modal = document.getElementById('packageModal');
    modal.style.display = 'none';
    document.getElementById('addPackageForm').reset();
    document.getElementById('modal-id').value = '';
    document.getElementById('modalTitle').textContent = 'Dodaj nową paczkę';
}

function editPackage(id) {
    fetch('get_package.php?id=' + id)
        .then(response => response.json())
        .then(data => {
            document.getElementById('modalTitle').textContent = 'Edytuj paczkę';
            document.getElementById('modal-id').value = data.id;
            document.getElementById('modal-nr_listu').value = data.nr_listu || '';
            document.getElementById('modal-status').value = data.status || '';
            document.getElementById('modal-data_utworzenia').value = data.data_utworzenia || '';
            document.getElementById('modal-data_odbioru').value = data.data_odbioru || '';
            document.getElementById('modal-data_dostarczenia').value = data.data_dostarczenia || '';
            document.getElementById('modal-wartosc').value = data.wartosc || '';
            document.getElementById('modal-ubezpieczenie').value = data.ubezpieczenie || '';
            document.getElementById('modal-koszt_transportu').value = data.koszt_transportu || '';
            
            const klientSelect = document.getElementById('modal-klient_id');
            if (data.klient_id) {
                klientSelect.value = data.klient_id;
            } else {
                klientSelect.selectedIndex = 0;
            }
            
            openModal();
        })
        .catch(error => {
            console.error('Error:', error);
            alert('Wystąpił błąd podczas pobierania danych paczki.');
        });
}

function deletePackage(id) {
    if (confirm('Czy na pewno chcesz usunąć tę paczkę?')) {
        const formData = new FormData();
        formData.append('action', 'delete');
        formData.append('id', id);

        fetch('paczki_operations.php', {
            method: 'POST',
            body: formData
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                window.location.reload();
            } else {
                alert(data.message || 'Nie udało się usunąć paczki.');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            alert('Wystąpił błąd podczas usuwania paczki.');
        });
    }
}

function resetForm() {
    document.getElementById('searchForm').reset();
    window.location.href = 'paczki.php';
}

function sortTable(column) {
    const urlParams = new URLSearchParams(window.location.search);
    const currentSort = urlParams.get('sort') || '';
    const currentOrder = urlParams.get('order') || 'asc';
    
    let newOrder = 'asc';
    if (currentSort === column && currentOrder === 'asc') {
        newOrder = 'desc';
    }
    
    urlParams.set('sort', column);
    urlParams.set('order', newOrder);
    
    window.location.href = `paczki.php?${urlParams.toString()}`;
}

function goToPage(page) {
    const urlParams = new URLSearchParams(window.location.search);
    urlParams.set('page', page);
    window.location.href = `paczki.php?${urlParams.toString()}`;
}

function changeRecordsPerPage(perPage) {
    const urlParams = new URLSearchParams(window.location.search);
    urlParams.set('per_page', perPage);
    urlParams.set('page', 1);
    window.location.href = `paczki.php?${urlParams.toString()}`;
}
document.getElementById('mobile-menu-btn').addEventListener('click', function() {
    const sideMenu = document.getElementById('sideMenu');
    sideMenu.classList.toggle('active');
});
window.onclick = function(event) {
    const modal = document.getElementById('packageModal');
    if (event.target == modal) {
        closeModal();
    }
}
function toggleDropdown() {
    document.getElementById("myDropdown").classList.toggle("show");
}
window.onclick = function(event) {
    if (!event.target.matches('.dropbtn')) {
        const dropdowns = document.getElementsByClassName("dropdown-content");
        for (let i = 0; i < dropdowns.length; i++) {
            const openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
}

document.querySelector('.add-client-btn').addEventListener('click', function() {
    document.getElementById('addPackageForm').reset();
    document.getElementById('modal-id').value = '';
    document.getElementById('modalTitle').textContent = 'Dodaj nową paczkę';
    openModal();
});