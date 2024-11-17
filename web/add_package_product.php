<?php
require 'czy_zalogowany.php';
require 'connect.php';

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $paczka_id = $_POST['paczka_id'];
    $produkt_id = $_POST['produkt_id'];
    $ilosc = (int)$_POST['ilosc'];

    pg_query($connection, "BEGIN");
    
    try {
        $check_query = "SELECT ilosc FROM produkty WHERE id = $1";
        $stmt = pg_prepare($connection, "check_quantity", $check_query);
        $result = pg_execute($connection, "check_quantity", [$produkt_id]);
        $produkt = pg_fetch_assoc($result);
        
        if (!$produkt || $produkt['ilosc'] < $ilosc) {
            throw new Exception('Niewystarczająca ilość produktu w magazynie');
        }
        
        $update_query = "UPDATE produkty SET ilosc = ilosc - $1 WHERE id = $2";
        $stmt = pg_prepare($connection, "update_quantity", $update_query);
        $result = pg_execute($connection, "update_quantity", [$ilosc, $produkt_id]);
        
        $insert_query = "INSERT INTO paczki_produkty (paczka_id, produkt_id, ilosc) VALUES ($1, $2, $3)";
        $stmt = pg_prepare($connection, "add_product", $insert_query);
        $result = pg_execute($connection, "add_product", [$paczka_id, $produkt_id, $ilosc]);

        pg_query($connection, "COMMIT");
        echo json_encode(['success' => true]);
        
    } catch (Exception $e) {
        pg_query($connection, "ROLLBACK");
        echo json_encode(['success' => false, 'message' => $e->getMessage()]);
    }
}
?>