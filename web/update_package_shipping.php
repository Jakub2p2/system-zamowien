<?php
header('Content-Type: application/json');
error_reporting(E_ALL);
ini_set('display_errors', 1);

require 'czy_zalogowany.php';
require 'connect.php';

try {
    $data = json_decode(file_get_contents('php://input'), true);
    
    if (!isset($data['package_id'])) {
        throw new Exception('Brak ID paczki');
    }
    
    pg_query($connection, "BEGIN");
    
    $query = "UPDATE paczki SET 
                status = $1,
                dostawa_id = $2,
                nr_listu = $3,
                data_odbioru = $4,
                data_dostarczenia = $5,
                ubezpieczenie = $6
              WHERE id = $7";
              
    $params = [
        'oczekiwanie na kuriera',
        $data['carrier_id'],
        $data['tracking_number'],
        $data['pickup_date'],
        $data['delivery_date'],
        $data['insurance'],
        $data['package_id']
    ];
    
    $result = pg_query_params($connection, $query, $params);
    
    if (!$result) {
        throw new Exception(pg_last_error($connection));
    }

    pg_query($connection, "COMMIT");
    
    echo json_encode(['success' => true]);

} catch (Exception $e) {
    pg_query($connection, "ROLLBACK");
    error_log("Błąd w update_package_shipping.php: " . $e->getMessage());
    http_response_code(500);
    echo json_encode([
        'success' => false,
        'message' => $e->getMessage()
    ]);
}
?>  