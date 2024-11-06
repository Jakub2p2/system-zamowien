<?php
require 'connect.php';

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $id = $_POST['id'];
    $nazwa = $_POST['nazwa'];
    $cechy = $_POST['cechy'];
    $cena = $_POST['cena'];
    $waga = $_POST['waga'];
    $ilosc = $_POST['ilosc'];
    
    $query = "UPDATE produkty SET nazwa = $1, cechy = $2, cena = $3, waga = $4, ilosc = $5 WHERE id = $6";
    $stmt = pg_prepare($connection, "update_product", $query);
    $result = pg_execute($connection, "update_product", array($nazwa, $cechy, $cena, $waga, $ilosc, $id));
    
    if ($result) {
        echo "Produkt został zaktualizowany pomyślnie";
    } else {
        echo "Wystąpił błąd podczas aktualizacji produktu";
    }
}
?> 