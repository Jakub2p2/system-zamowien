<?php
require 'connect.php';

header('Content-Type: application/json');

try {
    $imie = $_POST['imie'] ?? '';
    $nazwisko = $_POST['nazwisko'] ?? '';
    $nip = $_POST['nip'] ?? null;
    $regon = $_POST['regon'] ?? null;
    $pesel = $_POST['pesel'] ?? null;
    $email = $_POST['email'] ?? '';
    $telefon = $_POST['telefon'] ?? null;
    $adres = $_POST['adres'] ?? null;

    $query = "INSERT INTO klienci (imie, nazwisko, nip, regon, pesel, email, telefon, adres) 
              VALUES ($1, $2, $3, $4, $5, $6, $7, $8)";
    
    $result = pg_query_params($connection, $query, [
        $imie,
        $nazwisko,
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
