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

function sendToWarehouse(packageId) {
    if (confirm('Czy na pewno chcesz przesłać paczkę do magazynu?')) {
        fetch('update_package_status.php', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                package_id: packageId,
                status: 'towar zamowiony'
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

function updateSendButton() {
    const button = document.getElementById('sendToWarehouse');
    if (button) {
        fetch('check_package_products.php?id=' + packageId)
            .then(response => response.json())
            .then(data => {
                if (data.has_products) {
                    button.classList.remove('disabled');
                    button.disabled = false;
                } else {
                    button.classList.add('disabled');
                    button.disabled = true;
                }
            });
    }
}

function startCompletion(packageId) {
    if (confirm('Czy chcesz rozpocząć kompletację paczki?')) {
        fetch('update_package_status.php', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                package_id: packageId,
                status: 'komplementacja paczki'
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

function updatePackedStatus(productId, isPacked, checkbox) {
    const action = isPacked ? 'spakować' : 'odpakować';
    const originalState = checkbox.checked;
    
    if (confirm(`Czy na pewno chcesz ${action} ten produkt?`)) {
        fetch('update_packed_status.php', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                product_id: productId,
                packed: isPacked
            })
        })
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            if (data.success) {
                location.reload();
            } else {
                alert('Wystąpił błąd: ' + (data.message || 'Nieznany błąd'));
                checkbox.checked = originalState;
            }
        })
        .catch(error => {
            console.error('Błąd:', error);
            alert('Wystąpił błąd połączenia: ' + error.message);
            checkbox.checked = originalState;
        });
    } else {
        checkbox.checked = originalState;
    }
}

function markReadyToShip(packageId) {
    if (confirm('Czy na pewno chcesz oznaczyć paczkę jako przygotowaną do wysyłki?')) {
        fetch('update_package_status.php', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                package_id: packageId,
                status: 'towar przygotowany do wysylki'
            })
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                location.reload();
            } else {
                alert('Wystąpił błąd: ' + (data.message || 'Nieznany błąd'));
            }
        })
        .catch(error => {
            console.error('Błąd:', error);
            alert('Wystąpił błąd połączenia: ' + error.message);
        });
    }
}

function waitForCourier(packageId) {
    let modal = document.createElement('div');
    modal.className = 'courier-modal';
    modal.innerHTML = `
        <div class="courier-modal-content">
            <div class="courier-modal-header">
                <h4>Szczegóły wysyłki</h4>
                <span class="courier-close" onclick="closeShippingModal(this)">&times;</span>
            </div>
            <form id="shipmentForm">
                <div class="courier-form-group">
                    <label>Dostawca</label>
                    <select name="carrier" required>
                        <option value="">Wybierz dostawcę</option>
                    </select>
                </div>
                <div class="courier-form-group">
                    <label>
                        <input type="checkbox" name="insurance">
                        Ubezpieczenie przesyłki
                    </label>
                </div>
                <div class="courier-form-group">
                    <label>Numer listu przewozowego</label>
                    <input type="text" name="tracking_number" required>
                </div>
                <div class="courier-form-group">
                    <label>Data odbioru</label>
                    <input type="date" name="pickup_date" required>
                </div>
                <div class="courier-form-group">
                    <label>Przewidywana data doręczenia</label>
                    <input type="date" name="delivery_date" required>
                </div>
                <div class="courier-modal-footer">
                    <button type="button" class="courier-btn courier-btn-secondary" onclick="closeShippingModal(this)">Anuluj</button>
                    <button type="submit" class="courier-btn courier-btn-primary">Zapisz</button>
                </div>
            </form>
        </div>
    `;

    document.body.appendChild(modal);

    fetch('get_carriers.php')
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(carriers => {
            const select = modal.querySelector('select[name="carrier"]');
            carriers.forEach(carrier => {
                const option = document.createElement('option');
                option.value = carrier.id;
                option.textContent = carrier.nazwa;
                select.appendChild(option);
            });
        })
        .catch(error => {
            console.error('Error:', error);
            alert('Błąd podczas pobierania listy dostawców');
        });

    modal.querySelector('#shipmentForm').onsubmit = function(e) {
        e.preventDefault();
        
        const formData = new FormData(this);
        const data = {
            package_id: packageId,
            carrier_id: formData.get('carrier'),
            insurance: formData.get('insurance') === 'on',
            tracking_number: formData.get('tracking_number'),
            pickup_date: formData.get('pickup_date'),
            delivery_date: formData.get('delivery_date'),
            status: 'oczekiwanie na kuriera'
        };

        fetch('update_package_shipping.php', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data)
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
            alert('Wystąpił błąd podczas zapisywania danych');
        });
    };
}

function closeShippingModal(element) {
    const modal = element.closest('.courier-modal');
    if (modal) {
        modal.remove();
    }
}

function markPickedUpByCourier(packageId) {
    console.log("Funkcja zostanie zaimplementowana później");
}