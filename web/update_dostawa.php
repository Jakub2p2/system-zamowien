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
    if (empty($_POST['id']) || empty($_POST['nazwa']) || !isset($_POST['cena_za_kg']) || 
        !isset($_POST['cena_ubezpieczenia']) || empty($_POST['link_do_śledzenia'])) {
        throw new Exception('Wszystkie pola są wymagane');
    }
    $check_query = "SELECT id FROM dostawy WHERE nazwa = $1 AND id != $2";
    $check_result = pg_query_params($connection, $check_query, array($_POST['nazwa'], $_POST['id']));
    if (pg_num_rows($check_result) > 0) {
        throw new Exception('Dostawca o takiej nazwie już istnieje');
    }

    $query = "UPDATE dostawy SET nazwa = $1, cena_za_kg = $2, cena_ubezpieczenia = $3, 
              link_do_śledzenia = $4 WHERE id = $5";
    
    $result = pg_query_params($connection, $query, array(
        $_POST['nazwa'],
        $_POST['cena_za_kg'],
        $_POST['cena_ubezpieczenia'],
        $_POST['link_do_śledzenia'],
        $_POST['id']
    ));

    if (!$result) {
        throw new Exception(pg_last_error($connection));
    }

    if (pg_affected_rows($result) === 0) {
        throw new Exception('Nie znaleziono dostawcy o podanym ID');
    }

    echo json_encode(['success' => true, 'message' => 'Dostawca został zaktualizowany']);

} catch (Exception $e) {
    http_response_code(500);
    echo json_encode(['success' => false, 'message' => $e->getMessage()]);
} 