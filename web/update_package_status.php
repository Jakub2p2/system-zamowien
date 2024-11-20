<?php
require 'czy_zalogowany.php';
require 'connect.php';

$data = json_decode(file_get_contents('php://input'), true);

if (isset($data['package_id']) && isset($data['status'])) {
    $package_id = $data['package_id'];
    $status = $data['status'];
    
    $query = "UPDATE paczki SET status = $1 WHERE id = $2";
    $stmt = pg_prepare($connection, "update_status", $query);
    $result = pg_execute($connection, "update_status", [$status, $package_id]);
    
    if ($result) {
        echo json_encode(['success' => true]);
    } else {
        echo json_encode(['success' => false, 'message' => 'Błąd podczas aktualizacji statusu']);
    }
}
?> 