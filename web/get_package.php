<?php
error_reporting(E_ALL);
ini_set('display_errors', 0);

require 'connect.php';
require 'czy_zalogowany.php';

header('Content-Type: application/json');

if (!isset($_GET['id'])) {
    echo json_encode(['error' => true, 'message' => 'Nie podano ID paczki']);
    exit;
}

try {
    $query = "SELECT * FROM paczki WHERE id = $1";
    $result = pg_query_params($connection, $query, array($_GET['id']));
    
    if (!$result) {
        throw new Exception(pg_last_error($connection));
    }
    
    $paczka = pg_fetch_assoc($result);
    
    if (!$paczka) {
        throw new Exception('Nie znaleziono paczki o podanym ID');
    }
    
    if (!empty($paczka['data_utworzenia'])) {
        $paczka['data_utworzenia'] = date('Y-m-d', strtotime($paczka['data_utworzenia']));
    }
    if (!empty($paczka['data_odbioru'])) {
        $paczka['data_odbioru'] = date('Y-m-d', strtotime($paczka['data_odbioru']));
    }
    if (!empty($paczka['data_dostarczenia'])) {
        $paczka['data_dostarczenia'] = date('Y-m-d', strtotime($paczka['data_dostarczenia']));
    }
    
    echo json_encode($paczka);
    
} catch (Exception $e) {
    http_response_code(500);
    echo json_encode(['error' => true, 'message' => $e->getMessage()]);
}
?> 