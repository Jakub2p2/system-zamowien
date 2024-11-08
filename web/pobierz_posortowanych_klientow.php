<?php
error_reporting(E_ALL);
ini_set('display_errors', 1);

require_once 'connect.php';
session_start();

header('Content-Type: application/json');

if (!isset($connection)) {
    http_response_code(500);
    echo json_encode(['error' => true, 'message' => 'Brak połączenia z bazą danych']);
    exit();
}

if (!isset($_SESSION['user_id'])) {
    http_response_code(401);
    echo json_encode(['error' => true, 'message' => 'Unauthorized']);
    exit();
}

// Mapowanie nazw kolumn
$column_mapping = [
    'regon' => 'region' 
];

$allowed_columns = ['nazwa', 'nip', 'region', 'pesel', 'email', 'telefon', 'adres'];

$sort_column = $_GET['column'];
if ($sort_column === 'regon') {
    $sort_column = 'region';
}

$sort_column = in_array($sort_column, $allowed_columns) ? $sort_column : 'nazwa';
$sort_direction = strtoupper($_GET['direction']) === 'DESC' ? 'DESC' : 'ASC';

try {
    $sql = 'SELECT *, region as regon FROM klienci ORDER BY "' . pg_escape_string($sort_column) . '" ' . $sort_direction;
    
    $result = pg_query($connection, $sql);
    
    if (!$result) {
        throw new Exception(pg_last_error($connection));
    }
    
    $clients = [];
    while ($row = pg_fetch_assoc($result)) {
        $clients[] = $row;
    }
    
    pg_free_result($result);
    
    echo json_encode($clients, JSON_UNESCAPED_UNICODE);
    
} catch (Exception $e) {
    http_response_code(500);
    echo json_encode([
        'error' => true,
        'message' => 'Wystąpił błąd podczas pobierania danych: ' . $e->getMessage()
    ]);
}
?>
