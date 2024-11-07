<?php
require 'connect.php';
require 'czy_zalogowany.php';

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $response = ['success' => false, 'message' => ''];
    
    try {
        $check_query = "SELECT id FROM paczki WHERE nr_listu = $1";
        $check_result = pg_query_params($connection, $check_query, array($_POST['nr_listu']));
        
        if (pg_num_rows($check_result) > 0) {
            $response['message'] = 'Paczka o takim numerze listu już istnieje.';
            echo json_encode($response);
            exit;
        }
        
        $query = "INSERT INTO paczki (nr_listu, status, data_utworzenia, data_odbioru, 
                                    data_dostarczenia, wartosc, ubezpieczenie, 
                                    koszt_transportu, klient_id) 
                 VALUES ($1, $2, $3, $4, $5, $6, $7, $8, $9)
                 RETURNING id";
        
        $params = array(
            $_POST['nr_listu'],
            $_POST['status'],
            $_POST['data_utworzenia'],
            !empty($_POST['data_odbioru']) ? $_POST['data_odbioru'] : null,
            !empty($_POST['data_dostarczenia']) ? $_POST['data_dostarczenia'] : null,
            !empty($_POST['wartosc']) ? $_POST['wartosc'] : null,
            !empty($_POST['ubezpieczenie']) ? $_POST['ubezpieczenie'] : null,
            !empty($_POST['koszt_transportu']) ? $_POST['koszt_transportu'] : null,
            $_POST['klient_id']
        );
        
        $result = pg_query_params($connection, $query, $params);
        
        if ($result) {
            $response['success'] = true;
            $response['message'] = 'Paczka została dodana pomyślnie.';
        } else {
            $response['message'] = 'Błąd podczas dodawania paczki: ' . pg_last_error($connection);
        }
    } catch (Exception $e) {
        $response['message'] = 'Wystąpił błąd: ' . $e->getMessage();
    }
    
    echo json_encode($response);
    exit;
}
?> 