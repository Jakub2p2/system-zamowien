<?php
header('Content-Type: application/json');
error_reporting(E_ALL);
ini_set('display_errors', 1);

try {
    require 'czy_zalogowany.php';
    require 'connect.php';

    if (!$connection) {
        throw new Exception("Brak połączenia z bazą danych");
    }

    $query = "SELECT id, nazwa FROM dostawy ORDER BY nazwa";
    $result = pg_query($connection, $query);
    
    if (!$result) {
        throw new Exception("Błąd zapytania: " . pg_last_error($connection));
    }
    
    $carriers = [];
    while ($row = pg_fetch_assoc($result)) {
        $carriers[] = [
            'id' => $row['id'],
            'nazwa' => $row['nazwa']
        ];
    }
    
    if (empty($carriers)) {
        error_log("Pobrano pustą listę dostawców");
    }
    
    echo json_encode($carriers);

} catch (Exception $e) {
    error_log("Błąd w get_carriers.php: " . $e->getMessage());
    http_response_code(500);
    echo json_encode([
        'error' => true,
        'message' => $e->getMessage()
    ]);
}
?> 