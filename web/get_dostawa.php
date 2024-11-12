<?php
require_once 'connect.php';
require_once 'czy_zalogowany.php';

header('Content-Type: application/json');

if (!isset($_GET['id'])) {
    http_response_code(400);
    echo json_encode(['error' => true, 'message' => 'Nie podano ID dostawcy']);
    exit;
}

try {
    $query = "SELECT id, nazwa, cena_za_kg, cena_ubezpieczenia, link_do_śledzenia FROM dostawy WHERE id = $1";
    $result = pg_query_params($connection, $query, array($_GET['id']));
    
    if (!$result) {
        throw new Exception(pg_last_error($connection));
    }

    $dostawa = pg_fetch_assoc($result);
    if (!$dostawa) {
        throw new Exception('Nie znaleziono dostawcy o podanym ID');
    }

    echo json_encode($dostawa);

} catch (Exception $e) {
    http_response_code(500);
    echo json_encode([
        'error' => true,
        'message' => 'Wystąpił błąd: ' . $e->getMessage()
    ]);
} 