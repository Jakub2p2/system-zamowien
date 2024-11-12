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
    if (empty($_POST['nazwa']) || !isset($_POST['cena_za_kg']) || !isset($_POST['cena_ubezpieczenia']) || empty($_POST['link_do_śledzenia'])) {
        throw new Exception('Wszystkie pola są wymagane');
    }

    $check_query = "SELECT id FROM dostawy WHERE nazwa = $1";
    $check_result = pg_query_params($connection, $check_query, array($_POST['nazwa']));
    if (pg_num_rows($check_result) > 0) {
        throw new Exception('Dostawca o takiej nazwie już istnieje');
    }

    $query = "INSERT INTO dostawy (nazwa, cena_za_kg, cena_ubezpieczenia, \"link_do_śledzenia\") 
              VALUES ($1, $2, $3, $4)";
    
    $result = pg_query_params($connection, $query, array(
        $_POST['nazwa'],
        $_POST['cena_za_kg'],
        $_POST['cena_ubezpieczenia'],
        $_POST['link_do_śledzenia']
    ));

    if (!$result) {
        throw new Exception(pg_last_error($connection));
    }

    echo json_encode(['success' => true, 'message' => 'Dostawca został dodany']);

} catch (Exception $e) {
    http_response_code(500);
    echo json_encode(['success' => false, 'message' => $e->getMessage()]);
} 