<?php
require_once 'connect.php';
require_once 'czy_zalogowany.php';

header('Content-Type: application/json');

if (!isset($_GET['id'])) {
    http_response_code(400);
    echo json_encode(['error' => true, 'message' => 'Nie podano ID użytkownika']);
    exit;
}

try {
    $query = "SELECT id, imie, nazwisko, login, email, ranga FROM uzytkownicy WHERE id = $1";
    $result = pg_query_params($connection, $query, array($_GET['id']));
    
    if (!$result) {
        throw new Exception(pg_last_error($connection));
    }

    $user = pg_fetch_assoc($result);
    if (!$user) {
        throw new Exception('Nie znaleziono użytkownika o podanym ID');
    }

    echo json_encode($user);

} catch (Exception $e) {
    http_response_code(500);
    echo json_encode([
        'error' => true,
        'message' => 'Wystąpił błąd: ' . $e->getMessage()
    ]);
} 