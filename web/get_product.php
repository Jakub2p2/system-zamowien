<?php
require 'connect.php';

if (isset($_GET['id'])) {
    $id = $_GET['id'];
    
    $query = "SELECT * FROM produkty WHERE id = $1";
    $stmt = pg_prepare($connection, "get_product", $query);
    $result = pg_execute($connection, "get_product", array($id));
    
    if ($result) {
        $product = pg_fetch_assoc($result);
        echo json_encode($product);
    } else {
        echo json_encode(array('error' => 'Nie znaleziono produktu'));
    }
}
?> 