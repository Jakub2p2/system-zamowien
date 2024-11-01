<?php
require 'connect.php';

$response = ['success' => false, 'message' => ''];

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $id = $_POST['id'];
    $imie = $_POST['imie'];
    $nazwisko = $_POST['nazwisko'];
    $nip = $_POST['nip'];
    $regon = $_POST['regon'];
    $pesel = $_POST['pesel'];
    $email = $_POST['email'];
    $telefon = $_POST['telefon'];
    $adres = $_POST['adres'];
    
    $query = "UPDATE klienci SET 
              imie = $1, 
              nazwisko = $2, 
              nip = $3, 
              regon = $4, 
              pesel = $5, 
              email = $6, 
              telefon = $7, 
              adres = $8 
              WHERE id = $9";
              
    $stmt = pg_prepare($connection, "update_client", $query);
    $result = pg_execute($connection, "update_client", [
        $imie, $nazwisko, $nip, $regon, $pesel, 
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
