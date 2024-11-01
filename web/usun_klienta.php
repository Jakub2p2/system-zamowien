<?php
require 'connect.php';

$response = ['success' => false, 'message' => ''];

if ($_SERVER['REQUEST_METHOD'] === 'POST' && isset($_POST['id'])) {
    $id = $_POST['id'];
    
    $query = "DELETE FROM klienci WHERE id = $1";
    $stmt = pg_prepare($connection, "delete_client", $query);
    $result = pg_execute($connection, "delete_client", [$id]);
    
    if ($result) {
        $response['success'] = true;
        $response['message'] = 'Klient został usunięty pomyślnie';
    } else {
        $response['message'] = 'Błąd podczas usuwania klienta';
    }
} else {
    $response['message'] = 'Nieprawidłowe żądanie';
}

echo json_encode($response);
?>
