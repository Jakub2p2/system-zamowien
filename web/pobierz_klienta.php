<?php
require 'connect.php';

if (isset($_GET['id'])) {
    $id = $_GET['id'];
    
    $query = "SELECT id, nazwa, nip, region AS regon, pesel, email, telefon, adres FROM klienci WHERE id = $1";
    $stmt = pg_prepare($connection, "get_client", $query);
    $result = pg_execute($connection, "get_client", [$id]);
    
    if ($result) {
        $client = pg_fetch_assoc($result);
        echo json_encode($client);
    } else {
        echo json_encode(['error' => 'Nie znaleziono klienta']);
    }
}
?>
