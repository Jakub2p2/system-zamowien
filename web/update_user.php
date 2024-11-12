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
    if (empty($_POST['id']) || empty($_POST['imie']) || empty($_POST['nazwisko']) || 
        empty($_POST['login']) || empty($_POST['email']) || empty($_POST['ranga'])) {
        throw new Exception('Wszystkie pola są wymagane');
    }

    $check_query = "SELECT id FROM uzytkownicy WHERE login = $1 AND id != $2";
    $check_result = pg_query_params($connection, $check_query, array($_POST['login'], $_POST['id']));
    if (pg_num_rows($check_result) > 0) {
        throw new Exception('Ten login jest już zajęty');
    }

    $check_query = "SELECT id FROM uzytkownicy WHERE email = $1 AND id != $2";
    $check_result = pg_query_params($connection, $check_query, array($_POST['email'], $_POST['id']));
    if (pg_num_rows($check_result) > 0) {
        throw new Exception('Ten email jest już zajęty');
    }

    if (!empty($_POST['haslo'])) {
        $hashed_password = password_hash($_POST['haslo'], PASSWORD_DEFAULT);
        $query = "UPDATE uzytkownicy SET imie = $1, nazwisko = $2, login = $3, 
                 email = $4, haslo = $5, ranga = $6 WHERE id = $7";
        $params = array(
            $_POST['imie'],
            $_POST['nazwisko'],
            $_POST['login'],
            $_POST['email'],
            $hashed_password,
            $_POST['ranga'],
            $_POST['id']
        );
    } else {
        $query = "UPDATE uzytkownicy SET imie = $1, nazwisko = $2, login = $3, 
                 email = $4, ranga = $5 WHERE id = $6";
        $params = array(
            $_POST['imie'],
            $_POST['nazwisko'],
            $_POST['login'],
            $_POST['email'],
            $_POST['ranga'],
            $_POST['id']
        );
    }

    $result = pg_query_params($connection, $query, $params);
    if (!$result) {
        throw new Exception(pg_last_error($connection));
    }

    echo json_encode(['success' => true, 'message' => 'Użytkownik został zaktualizowany']);

} catch (Exception $e) {
    http_response_code(500);
    echo json_encode(['success' => false, 'message' => $e->getMessage()]);
} 