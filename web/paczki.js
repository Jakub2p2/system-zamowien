document.addEventListener('DOMContentLoaded', function() {
    const form = document.getElementById('addPackageForm');
    if (form) {
        form.addEventListener('submit', function(e) {
            e.preventDefault();
            
            const submitButton = this.querySelector('button[type="submit"]');
            if (submitButton.disabled) return;
            
            submitButton.disabled = true;
            
            const formData = new FormData(this);
            addPackage(formData)
                .finally(() => {
                    submitButton.disabled = false;
                });
        });
    }
});

async function addPackage(formData) {
    try {
        const isEdit = formData.get('id') ? true : false;
        const url = isEdit ? 'update_package.php' : 'add_package.php';
        
        const response = await fetch(url, {
            method: 'POST',
            body: formData
        });
        
        if (!response.ok) {
            return response.text().then(text => {
                throw new Error('Błąd serwera: ' + text);
            });
        }
        
        const data = await response.json();
        
        if (data.success) {
            alert(data.message);
            window.location.reload();
        } else {
            throw new Error(data.message || 'Wystąpił nieznany błąd');
        }
    } catch (error) {
        console.error('Error:', error);
        alert('Wystąpił błąd podczas zapisywania paczki: ' + error.message);
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
    fetch('get_package.php?id=' + encodeURIComponent(id))
        .then(response => {
            if (!response.ok) {
                return response.text().then(text => {
                    throw new Error('Błąd serwera: ' + text);
                });
            }
            return response.json();
        })
        .then(data => {
            if (data.error) {
                throw new Error(data.message);
            }

            document.getElementById('modal-id').value = data.id;
            document.getElementById('modal-nr_listu').value = data.nr_listu || '';
            document.getElementById('modal-status').value = data.status || '';
            document.getElementById('modal-data_utworzenia').value = data.data_utworzenia || '';
            document.getElementById('modal-data_odbioru').value = data.data_odbioru || '';
            document.getElementById('modal-data_dostarczenia').value = data.data_dostarczenia || '';
            document.getElementById('modal-wartosc').value = data.wartosc || '';
            document.getElementById('modal-ubezpieczenie').value = data.ubezpieczenie || '';
            document.getElementById('modal-koszt_transportu').value = data.koszt_transportu || '';
            
            if (data.klient_id) {
                document.getElementById('modal-klient_id').value = data.klient_id;
            }
            
            document.getElementById('modalTitle').textContent = 'Edytuj paczkę';
            openModal();
        })
        .catch(error => {
            console.error('Error:', error);
            alert('Wystąpił błąd podczas pobierania danych paczki: ' + error.message);
        });
}

function deletePackage(packageId) {
    console.log('Otrzymane ID:', packageId);
    
    if (!confirm('Czy na pewno chcesz usunąć paczkę #' + packageId + '?')) {
        return;
    }
    
    fetch('delete_package.php', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded',
        },
        body: 'package_id=' + packageId
    })
    .then(response => response.json())
    .then(data => {
        console.log('Odpowiedź:', data);
        alert(data.message);
        if (data.success) {
            window.location.reload();
        }
    })
    .catch(error => {
        console.error('Błąd:', error);
        alert('Wystąpił błąd podczas usuwania paczki');
    });
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