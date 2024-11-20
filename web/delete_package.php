<?php
header('Content-Type: application/json');
require 'czy_zalogowany.php';
require 'connect.php';
error_log("POST: " . print_r($_POST, true));
error_log("Raw input: " . file_get_contents('php://input'));

try {
    $input = file_get_contents('php://input');
    parse_str($input, $postData);
    
    if (empty($postData['package_id'])) {
        throw new Exception('Nie przekazano ID paczki');
    }

    $package_id = $postData['package_id'];

    pg_query($connection, "BEGIN");

    $delete_products = "DELETE FROM paczki_produkty WHERE paczka_id = $1";
    $result_products = pg_query_params($connection, $delete_products, [$package_id]);
    
    if (!$result_products) {
        throw new Exception("Błąd podczas usuwania produktów z paczki");
    }

    $delete_package = "DELETE FROM paczki WHERE id = $1";
    $result_package = pg_query_params($connection, $delete_package, [$package_id]);
    
    if (!$result_package) {
        throw new Exception("Błąd podczas usuwania paczki");
    }

    pg_query($connection, "COMMIT");
    
    echo json_encode([
        'success' => true,
        'message' => 'Paczka została pomyślnie usunięta'
    ]);

} catch (Exception $e) {
    pg_query($connection, "ROLLBACK");
    echo json_encode([
        'success' => false,
        'message' => $e->getMessage()
    ]);
}
?> 