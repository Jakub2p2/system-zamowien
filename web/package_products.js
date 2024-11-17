document.addEventListener('DOMContentLoaded', function() {
    const addProductForm = document.getElementById('addProductForm');
    
    if (addProductForm) {
        addProductForm.addEventListener('submit', function(e) {
            e.preventDefault();
            
            const iloscInput = document.getElementById('ilosc');
            const selectedOption = document.getElementById('produkt_id').options[document.getElementById('produkt_id').selectedIndex];
            const dostepnaIlosc = parseInt(selectedOption.text.match(/Dostępne: (\d+)/)[1]);
            
            if (parseInt(iloscInput.value) > dostepnaIlosc) {
                alert('Nie można dodać więcej produktów niż jest dostępne w magazynie');
                return;
            }
            
            const formData = new FormData(this);
            
            fetch('add_package_product.php', {
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
            });
        });
    }
});

function openProductModal() {
    document.getElementById('productModal').style.display = 'block';
}

function closeProductModal() {
    document.getElementById('productModal').style.display = 'none';
}

function deleteProduct(pozycjaId) {
    if (confirm('Czy na pewno chcesz usunąć ten produkt z paczki?')) {
        fetch('delete_package_product.php', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                pozycja_id: pozycjaId
            })
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                location.reload();
            } else {
                alert('Wystąpił błąd: ' + data.message);
            }
        });
    }
}

document.getElementById('produkt_id').addEventListener('change', function() {
    const selectedOption = this.options[this.selectedIndex];
    const dostepnaIlosc = parseInt(selectedOption.text.match(/Dostępne: (\d+)/)[1]);
    
    const iloscInput = document.getElementById('ilosc');
    iloscInput.max = dostepnaIlosc;
    iloscInput.value = Math.min(iloscInput.value, dostepnaIlosc);
});