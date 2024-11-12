<?php
require_once 'connect.php';
require_once 'czy_zalogowany.php';

header('Content-Type: application/json');

try {
    $query = "SELECT * FROM dostawy";
    $result = pg_query($connection, $query);
    
    if (!$result) {
        throw new Exception(pg_last_error($connection));
    }

    $dostawy = [];
    while ($row = pg_fetch_assoc($result)) {
        $dostawy[] = $row;
    }

    echo json_encode($dostawy);

} catch (Exception $e) {
    http_response_code(500);
    echo json_encode(['error' => true, 'message' => $e->getMessage()]);
} 