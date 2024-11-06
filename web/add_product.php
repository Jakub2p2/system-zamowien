<?php
require 'connect.php';

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $nazwa = $_POST['nazwa'];
    $cechy = $_POST['cechy'];
    $cena = $_POST['cena'];
    $waga = $_POST['waga'];
    $ilosc = $_POST['ilosc'];
    
    $query = "INSERT INTO produkty (nazwa, cechy, cena, waga, ilosc) VALUES ($1, $2, $3, $4, $5)";
    $stmt = pg_prepare($connection, "add_product", $query);
    $result = pg_execute($connection, "add_product", array($nazwa, $cechy, $cena, $waga, $ilosc));
    
    if ($result) {
        echo "Produkt został dodany pomyślnie";
    } else {
        echo "Wystąpił błąd podczas dodawania produktu";
    }
}
?>
