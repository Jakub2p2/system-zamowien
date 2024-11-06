<?php
require 'connect.php';

if ($_SERVER['REQUEST_METHOD'] === 'POST' && isset($_POST['id'])) {
    $id = $_POST['id'];
    
    $query = "DELETE FROM produkty WHERE id = $1";
    $stmt = pg_prepare($connection, "delete_product", $query);
    $result = pg_execute($connection, "delete_product", array($id));
    
    if ($result) {
        echo "Produkt został usunięty pomyślnie";
    } else {
        echo "Wystąpił błąd podczas usuwania produktu";
    }
}
?> 