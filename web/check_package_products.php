<?php
require 'czy_zalogowany.php';
require 'connect.php';

if (isset($_GET['id'])) {
    $package_id = $_GET['id'];
    
    $query = "SELECT COUNT(*) as count FROM paczki_produkty WHERE paczka_id = $1";
    $stmt = pg_prepare($connection, "check_products", $query);
    $result = pg_execute($connection, "check_products", [$package_id]);
    $count = pg_fetch_assoc($result);
    
    echo json_encode(['has_products' => $count['count'] > 0]);
}
?> 