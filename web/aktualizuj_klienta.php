<?php
require 'connect.php';

$response = ['success' => false, 'message' => ''];

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $id = $_POST['id'];
    $nazwa = $_POST['nazwa'];
    $nip = $_POST['nip'];
    $regon = $_POST['regon'];
    $pesel = $_POST['pesel'];
    $email = $_POST['email'];
    $telefon = $_POST['telefon'];
    $adres = $_POST['adres'];
    
    $query = "UPDATE klienci SET 
              nazwa = $1, 
              nip = $2, 
              region = $3, 
              pesel = $4, 
              email = $5, 
              telefon = $6, 
              adres = $7 
              WHERE id = $8";
              
    $stmt = pg_prepare($connection, "update_client", $query);
    $result = pg_execute($connection, "update_client", [
        $nazwa, $nip, $regon, $pesel, 
        $email, $telefon, $adres, $id
    ]);
    
    if ($result) {
        $response['success'] = true;
        $response['message'] = 'Klient został zaktualizowany pomyślnie';
    } else {
        $response['message'] = 'Błąd podczas aktualizacji klienta';
    }
}

echo json_encode($response);
?>
