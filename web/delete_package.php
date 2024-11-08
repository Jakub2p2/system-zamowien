<?php
error_reporting(E_ALL);
ini_set('display_errors', 0);

require 'connect.php';
require 'czy_zalogowany.php';

header('Content-Type: application/json');

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $response = ['success' => false, 'message' => ''];
    
    try {
        if (!isset($_POST['id'])) {
            throw new Exception('Nie podano ID paczki');
        }
        
        $query = "DELETE FROM paczki WHERE id = $1 RETURNING id";
        $result = pg_query_params($connection, $query, array($_POST['id']));
        
        if (!$result) {
            throw new Exception(pg_last_error($connection));
        }
        
        if (pg_affected_rows($result) > 0) {
            $response['success'] = true;
            $response['message'] = 'Paczka została usunięta pomyślnie.';
        } else {
            throw new Exception('Nie znaleziono paczki o podanym ID.');
        }
        
    } catch (Exception $e) {
        $response['message'] = 'Wystąpił błąd: ' . $e->getMessage();
    }
    
    echo json_encode($response);
    exit;
}

http_response_code(405);
echo json_encode(['success' => false, 'message' => 'Metoda nie dozwolona']); 