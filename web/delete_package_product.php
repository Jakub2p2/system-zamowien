<?php
require 'czy_zalogowany.php';
require 'connect.php';

$data = json_decode(file_get_contents('php://input'), true);

if (isset($data['pozycja_id'])) {
    $pozycja_id = $data['pozycja_id'];

    pg_query($connection, "BEGIN");
    
    try {
        $query = "SELECT produkt_id, ilosc FROM paczki_produkty WHERE id = $1";
        $stmt = pg_prepare($connection, "get_position", $query);
        $result = pg_execute($connection, "get_position", [$pozycja_id]);
        $pozycja = pg_fetch_assoc($result);
        
        if ($pozycja) {
            $update_query = "UPDATE produkty SET ilosc = ilosc + $1 WHERE id = $2";
            $stmt = pg_prepare($connection, "update_quantity", $update_query);
            $result = pg_execute($connection, "update_quantity", [$pozycja['ilosc'], $pozycja['produkt_id']]);
            
            $delete_query = "DELETE FROM paczki_produkty WHERE id = $1";
            $stmt = pg_prepare($connection, "delete_product", $delete_query);
            $result = pg_execute($connection, "delete_product", [$pozycja_id]);

            pg_query($connection, "COMMIT");
            echo json_encode(['success' => true]);
        } else {
            throw new Exception('Nie znaleziono pozycji');
        }
        
    } catch (Exception $e) {
        pg_query($connection, "ROLLBACK");
        echo json_encode(['success' => false, 'message' => $e->getMessage()]);
    }
}
?>