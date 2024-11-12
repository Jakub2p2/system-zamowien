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
    if (empty($_POST['imie']) || empty($_POST['nazwisko']) || empty($_POST['login']) || 
        empty($_POST['email']) || empty($_POST['haslo']) || empty($_POST['ranga'])) {
        throw new Exception('Wszystkie pola są wymagane');
    }

    $check_query = "SELECT id FROM uzytkownicy WHERE login = $1";
    $check_result = pg_query_params($connection, $check_query, array($_POST['login']));
    if (pg_num_rows($check_result) > 0) {
        throw new Exception('Ten login jest już zajęty');
    }

    $check_query = "SELECT id FROM uzytkownicy WHERE email = $1";
    $check_result = pg_query_params($connection, $check_query, array($_POST['email']));
    if (pg_num_rows($check_result) > 0) {
        throw new Exception('Ten email jest już zajęty');
    }

    $hashed_password = password_hash($_POST['haslo'], PASSWORD_DEFAULT);

    $query = "INSERT INTO uzytkownicy (imie, nazwisko, login, email, haslo, ranga) 
              VALUES ($1, $2, $3, $4, $5, $6)";
    
    $result = pg_query_params($connection, $query, array(
        $_POST['imie'],
        $_POST['nazwisko'],
        $_POST['login'],
        $_POST['email'],
        $hashed_password,
        $_POST['ranga']
    ));

    if (!$result) {
        throw new Exception(pg_last_error($connection));
    }

    echo json_encode(['success' => true, 'message' => 'Użytkownik został dodany']);

} catch (Exception $e) {
    http_response_code(500);
    echo json_encode(['success' => false, 'message' => $e->getMessage()]);
} 