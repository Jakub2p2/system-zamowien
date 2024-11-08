<?php
require 'connect.php';

header('Content-Type: application/json');

try {
    $nazwa = $_POST['nazwa'] ?? '';
    $nip = $_POST['nip'] ?? null;
    $regon = $_POST['regon'] ?? null;
    $pesel = $_POST['pesel'] ?? null;
    $email = $_POST['email'] ?? '';
    $telefon = $_POST['telefon'] ?? null;
    $adres = $_POST['adres'] ?? null;

    $query = "INSERT INTO klienci (nazwa, nip, region, pesel, email, telefon, adres) 
              VALUES ($1, $2, $3, $4, $5, $6, $7)";
    
    $stmt = pg_prepare($connection, "insert_client", $query);
    $result = pg_execute($connection, "insert_client", [
        $nazwa,
        $nip,
        $regon,
        $pesel,
        $email,
        $telefon,
        $adres
    ]);

    if ($result) {
        echo json_encode(['success' => true]);
    } else {
        throw new Exception(pg_last_error($connection));
    }
} catch (Exception $e) {
    echo json_encode([
        'success' => false,
        'message' => $e->getMessage()
    ]);
}
?>
