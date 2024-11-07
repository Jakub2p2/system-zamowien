<?php
require 'connect.php';
require 'czy_zalogowany.php';

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $response = ['success' => false, 'message' => ''];
    
    try {
        $query = "UPDATE paczki 
                 SET nr_listu = $1, status = $2, data_utworzenia = $3, 
                     data_odbioru = $4, data_dostarczenia = $5, wartosc = $6, 
                     ubezpieczenie = $7, koszt_transportu = $8, klient_id = $9 
                 WHERE id = $10";
        
        $params = array(
            $_POST['nr_listu'],
            $_POST['status'],
            $_POST['data_utworzenia'],
            $_POST['data_odbioru'] ?: null,
            $_POST['data_dostarczenia'] ?: null,
            $_POST['wartosc'] ?: null,
            $_POST['ubezpieczenie'] ?: null,
            $_POST['koszt_transportu'] ?: null,
            $_POST['klient_id'],
            $_POST['id']
        );
        
        $stmt = pg_prepare($connection, "update_package", $query);
        $result = pg_execute($connection, "update_package", $params);
        
        if ($result) {
            $response['success'] = true;
            $response['message'] = 'Paczka została zaktualizowana pomyślnie.';
        } else {
            $response['message'] = 'Błąd podczas aktualizacji paczki.';
        }
    } catch (Exception $e) {
        $response['message'] = 'Wystąpił błąd: ' . $e->getMessage();
    }
    
    echo json_encode($response);
}
?> 