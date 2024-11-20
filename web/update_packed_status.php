<?php
header('Content-Type: application/json');
error_reporting(0);

require 'czy_zalogowany.php';
require 'connect.php';

try {
    $input = file_get_contents('php://input');
    $data = json_decode($input, true);

    if (!isset($data['product_id']) || !isset($data['packed'])) {
        throw new Exception('Brak wymaganych danych');
    }

    $product_id = $data['product_id'];
    $packed = $data['packed'] ? 'true' : 'false';
    
    $query = "UPDATE paczki_produkty SET spakowany = $1 WHERE id = $2";
    $stmt = pg_prepare($connection, "update_packed", $query);
    
    if (!$stmt) {
        throw new Exception(pg_last_error($connection));
    }
    
    $result = pg_execute($connection, "update_packed", [$packed, $product_id]);
    
    if (!$result) {
        throw new Exception(pg_last_error($connection));
    }
    
    echo json_encode(['success' => true]);

} catch (Exception $e) {
    echo json_encode([
        'success' => false,
        'message' => $e->getMessage()
    ]);
}
?> 