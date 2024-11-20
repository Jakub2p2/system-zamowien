<?php
header('Content-Type: application/json');
error_reporting(E_ALL);
ini_set('display_errors', 1);

require 'czy_zalogowany.php';
require 'connect.php';

try {
    if (!isset($_POST['product_id'])) {
        throw new Exception('Nie przekazano ID produktu');
    }

    $product_id = $_POST['product_id'];

    pg_query($connection, "BEGIN");

    try {

        $delete_from_packages = "DELETE FROM paczki_produkty WHERE produkt_id = $1";
        $result_packages = pg_query_params($connection, $delete_from_packages, [$product_id]);
        
        if (!$result_packages) {
            throw new Exception("Błąd podczas usuwania produktu z paczek: " . pg_last_error($connection));
        }

        $delete_product = "DELETE FROM produkty WHERE id = $1";
        $result_product = pg_query_params($connection, $delete_product, [$product_id]);
        
        if (!$result_product) {
            throw new Exception("Błąd podczas usuwania produktu: " . pg_last_error($connection));
        }

        // Zatwierdzamy transakcję
        pg_query($connection, "COMMIT");

        header('Location: produkty.php?deleted=true');
        exit;

    } catch (Exception $e) {
        pg_query($connection, "ROLLBACK");
        throw $e;
    }

} catch (Exception $e) {
    pg_query($connection, "ROLLBACK");
    header('Location: produkty.php?error=' . urlencode($e->getMessage()));
    exit;
}
?> 