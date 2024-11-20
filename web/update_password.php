<?php
require_once 'connect.php';
require_once 'czy_zalogowany.php';

header('Content-Type: application/json');

if ($_SERVER['REQUEST_METHOD'] !== 'POST') {
    echo json_encode(['success' => false, 'message' => 'Niedozwolona metoda']);
    exit;
}

try {
    if (empty($_POST['old_password']) || empty($_POST['new_password']) || empty($_POST['confirm_password'])) {
        throw new Exception('Wszystkie pola są wymagane');
    }

    if ($_POST['new_password'] !== $_POST['confirm_password']) {
        throw new Exception('Nowe hasła nie są identyczne');
    }

    $query = "SELECT haslo FROM uzytkownicy WHERE id = $1";
    $result = pg_query_params($connection, $query, array($_SESSION['user_id']));
    
    if (!$result) {
        throw new Exception('Błąd bazy danych');
    }

    $user = pg_fetch_assoc($result);
    
    $old_password_hash = md5($_POST['old_password']);
    if (!$user || $user['haslo'] !== $old_password_hash) {
        throw new Exception('Nieprawidłowe stare hasło');
    }

    $new_password_hash = md5($_POST['new_password']);
    
    $update_query = "UPDATE uzytkownicy SET haslo = $1 WHERE id = $2";
    $update_result = pg_query_params($connection, $update_query, array($new_password_hash, $_SESSION['user_id']));

    if (!$update_result) {
        throw new Exception('Błąd podczas aktualizacji hasła');
    }

    echo json_encode(['success' => true, 'message' => 'Hasło zostało zmienione']);

} catch (Exception $e) {
    http_response_code(400);
    echo json_encode(['success' => false, 'message' => $e->getMessage()]);
} 