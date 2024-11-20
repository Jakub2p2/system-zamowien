<?php
header('Content-Type: application/json');
require 'czy_zalogowany.php';
require 'connect.php';

try {
    if (!isset($_POST['id'])) {
        throw new Exception('Nie przekazano ID paczki do aktualizacji');
    }

    $package_id = $_POST['id'];
    
    $update_query = "UPDATE paczki SET 
        nr_listu = $1,
        status = $2,
        data_utworzenia = $3,
        data_odbioru = $4,
        data_dostarczenia = $5,
        wartosc = $6,
        ubezpieczenie = $7,
        koszt_transportu = $8,
        klient_id = $9
        WHERE id = $10";
        
    $result = pg_query_params($connection, $update_query, array(
        $_POST['nr_listu'],
        $_POST['status'],
        $_POST['data_utworzenia'],
        $_POST['data_odbioru'],
        $_POST['data_dostarczenia'],
        $_POST['wartosc'],
        $_POST['ubezpieczenie'],
        $_POST['koszt_transportu'],
        $_POST['klient_id'],
        $package_id
    ));

    if (!$result) {
        throw new Exception("Błąd podczas aktualizacji paczki: " . pg_last_error($connection));
    }

    echo json_encode([
        'success' => true,
        'message' => 'Paczka została zaktualizowana'
    ]);

} catch (Exception $e) {
    echo json_encode([
        'success' => false,
        'message' => $e->getMessage()
    ]);
}
?> 