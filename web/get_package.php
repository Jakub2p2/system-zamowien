<?php
require 'connect.php';
require 'czy_zalogowany.php';

if (isset($_GET['id'])) {
    $id = $_GET['id'];
    
    $query = "SELECT p.*, k.imie, k.nazwisko, k.nip 
              FROM paczki p 
              LEFT JOIN klienci k ON p.klient_id = k.id 
              WHERE p.id = $1";
              
    $result = pg_query_params($connection, $query, array($id));
    
    if ($result && $row = pg_fetch_assoc($result)) {
        $row['data_utworzenia'] = $row['data_utworzenia'] ? date('Y-m-d', strtotime($row['data_utworzenia'])) : '';
        $row['data_odbioru'] = $row['data_odbioru'] ? date('Y-m-d', strtotime($row['data_odbioru'])) : '';
        $row['data_dostarczenia'] = $row['data_dostarczenia'] ? date('Y-m-d', strtotime($row['data_dostarczenia'])) : '';
        
        echo json_encode($row);
    } else {
        echo json_encode(['error' => 'Nie znaleziono paczki']);
    }
}
?> 