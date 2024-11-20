function openModal() {
    document.getElementById('productModal').style.display = 'block';
    document.getElementById('modalTitle').textContent = 'Dodaj nowy produkt';
    document.getElementById('addProductForm').reset();
    document.getElementById('modal-id').value = '';
}

function closeModal() {
    document.getElementById('productModal').style.display = 'none';
}

function editProduct(id) {
    fetch('get_product.php?id=' + id)
        .then(response => response.json())
        .then(data => {
            document.getElementById('modal-id').value = data.id;
            document.getElementById('modal-nazwa').value = data.nazwa;
            document.getElementById('modal-cechy').value = data.cechy;
            document.getElementById('modal-cena').value = data.cena;
            document.getElementById('modal-waga').value = data.waga;
            document.getElementById('modal-ilosc').value = data.ilosc;
            
            document.getElementById('modalTitle').textContent = 'Edytuj produkt';
            document.getElementById('productModal').style.display = 'block';
        })
        .catch(error => console.error('Error:', error));
}

function deleteProduct(productId) {
    if (confirm('Czy na pewno chcesz usunąć ten produkt? Zostanie on również usunięty ze wszystkich paczek!')) {
        const formData = new FormData();
        formData.append('product_id', productId);

        fetch('delete_product.php', {
            method: 'POST',
            body: formData
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                location.reload();
            } else {
                alert('Wystąpił błąd: ' + data.message);
            }
        })
        .catch(error => {
            console.error('Error:', error);
            alert('Wystąpił błąd podczas usuwania produktu');
        });
    }
}

document.getElementById('addProductForm').addEventListener('submit', function(e) {
    e.preventDefault();
    
    const formData = new FormData(this);
    const id = document.getElementById('modal-id').value;
    
    fetch(id ? 'update_product.php' : 'add_product.php', {
        method: 'POST',
        body: formData
    })
    .then(response => response.text())
    .then(data => {
        alert(data);
        location.reload();
    })
    .catch(error => console.error('Error:', error));
});

function resetForm() {
    document.getElementById('searchForm').reset();
    window.location.href = 'produkty.php';
}

let currentSort = {
    column: '',
    direction: 'asc'
};

function sortTable(column) {
    const table = document.querySelector('.clients-table');
    const tbody = table.querySelector('tbody');
    const rows = Array.from(tbody.querySelectorAll('tr'));

    if (currentSort.column === column) {
        currentSort.direction = currentSort.direction === 'asc' ? 'desc' : 'asc';
    } else {
        currentSort.column = column;
        currentSort.direction = 'asc';
    }

    document.querySelectorAll('.sort-arrows .arrow').forEach(arrow => {
        arrow.classList.remove('active');
    });

    const th = document.querySelector(`th[onclick="sortTable('${column}')"]`);
    const arrow = th.querySelector(`.arrow.${currentSort.direction === 'asc' ? 'up' : 'down'}`);
    if (arrow) {
        arrow.classList.add('active');
    }

    const compare = (a, b) => {
        let valueA, valueB;

        switch(column) {
            case 'nazwa':
                valueA = a.cells[0].textContent.trim().toLowerCase();
                valueB = b.cells[0].textContent.trim().toLowerCase();
                break;
            case 'cena':
                valueA = parseFloat(a.cells[2].textContent.replace(' zł', ''));
                valueB = parseFloat(b.cells[2].textContent.replace(' zł', ''));
                break;
            case 'waga':
                valueA = parseFloat(a.cells[3].textContent.replace(' kg', ''));
                valueB = parseFloat(b.cells[3].textContent.replace(' kg', ''));
                break;
            default:
                valueA = a.cells[0].textContent.trim().toLowerCase();
                valueB = b.cells[0].textContent.trim().toLowerCase();
        }

        if (valueA < valueB) return currentSort.direction === 'asc' ? -1 : 1;
        if (valueA > valueB) return currentSort.direction === 'asc' ? 1 : -1;
        return 0;
    };

    rows.sort(compare).forEach(row => tbody.appendChild(row));
}

function goToPage(page) {
    const urlParams = new URLSearchParams(window.location.search);
    urlParams.set('page', page);
    window.location.search = urlParams.toString();
}

function changeRecordsPerPage(perPage) {
    const urlParams = new URLSearchParams(window.location.search);
    urlParams.set('per_page', perPage);
    urlParams.set('page', 1);
    window.location.search = urlParams.toString();
}

function toggleDropdown() {
    document.getElementById("myDropdown").classList.toggle("show");
}

window.onclick = function(event) {
    if (!event.target.matches('.dropbtn')) {
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
