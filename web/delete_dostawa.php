<?php
require_once 'connect.php';
require_once 'czy_zalogowany.php';

header('Content-Type: application/json');

if ($_SERVER['REQUEST_METHOD'] !== 'POST') {
    http_response_code(405);
    echo json_encode(['success' => false, 'message' => 'Metoda nie dozwolona']);
    exit;
}

try {
    if (!isset($_POST['id'])) {
        throw new Exception('Nie podano ID dostawcy');
    }

    $query = "DELETE FROM dostawy WHERE id = $1";
    $result = pg_query_params($connection, $query, array($_POST['id']));

    if (!$result) {
        throw new Exception(pg_last_error($connection));
    }

    if (pg_affected_rows($result) === 0) {
        throw new Exception('Nie znaleziono dostawcy o podanym ID');
    }

    echo json_encode(['success' => true, 'message' => 'Dostawca został usunięty']);

} catch (Exception $e) {
    http_response_code(500);
    echo json_encode(['success' => false, 'message' => $e->getMessage()]);
} 