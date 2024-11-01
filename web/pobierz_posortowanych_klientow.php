<?php
require 'connect.php';

$allowed_columns = ['imie', 'nazwisko'];
$column = isset($_GET['column']) && in_array($_GET['column'], $allowed_columns) ? $_GET['column'] : 'nazwisko';
$direction = isset($_GET['direction']) && strtoupper($_GET['direction']) === 'DESC' ? 'DESC' : 'ASC';

$query = "SELECT * FROM klienci ORDER BY $column $direction";
$result = pg_query($connection, $query);

$clients = [];
while ($row = pg_fetch_assoc($result)) {
    $clients[] = $row;
}

header('Content-Type: application/json');
echo json_encode($clients);
?>
