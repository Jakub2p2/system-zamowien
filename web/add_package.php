<?php
error_reporting(E_ALL);
ini_set('display_errors', 0);

require 'connect.php';
require 'czy_zalogowany.php';

header('Content-Type: application/json');

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $response = ['success' => false, 'message' => ''];
    
    try {
        $data_utworzenia = !empty($_POST['data_utworzenia']) ? date('Y-m-d', strtotime($_POST['data_utworzenia'])) : null;
        $data_odbioru = !empty($_POST['data_odbioru']) ? date('Y-m-d', strtotime($_POST['data_odbioru'])) : null;
        $data_dostarczenia = !empty($_POST['data_dostarczenia']) ? date('Y-m-d', strtotime($_POST['data_dostarczenia'])) : null;
        
        $query = "INSERT INTO paczki (nr_listu, status, data_utworzenia, data_odbioru, 
                                    data_dostarczenia, wartosc, ubezpieczenie, 
                                    koszt_transportu, klient_id, created_by) 
                 VALUES ($1, $2, $3, $4, $5, $6, $7, $8, $9, $10) RETURNING id";
        
        $params = array(
            $_POST['nr_listu'],
            $_POST['status'],
            $data_utworzenia,
            $data_odbioru,
            $data_dostarczenia,
            !empty($_POST['wartosc']) ? $_POST['wartosc'] : null,
            !empty($_POST['ubezpieczenie']) ? $_POST['ubezpieczenie'] : null,
            !empty($_POST['koszt_transportu']) ? $_POST['koszt_transportu'] : null,
            $_POST['klient_id'],
            $_SESSION['user_id']
        );
        
        error_log("Params: " . print_r($params, true));
        
        $stmt = pg_prepare($connection, "add_package", $query);
        if (!$stmt) {
            throw new Exception(pg_last_error($connection));
        }
        
        $result = pg_execute($connection, "add_package", $params);
        if (!$result) {
            throw new Exception(pg_last_error($connection));
        }
        
        $row = pg_fetch_assoc($result);
        if ($row) {
            $response['success'] = true;
            $response['message'] = 'Paczka została dodana pomyślnie.';
            $response['id'] = $row['id'];
        } else {
            throw new Exception('Nie udało się pobrać ID nowej paczki.');
        }
        
    } catch (Exception $e) {
        $response['message'] = 'Wystąpił błąd: ' . $e->getMessage();
    }
    
    echo json_encode($response);
    exit;
}

http_response_code(405);
echo json_encode(['success' => false, 'message' => 'Metoda nie dozwolona']);
?> 