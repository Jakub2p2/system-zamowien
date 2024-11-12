<?php
session_start();

if (!isset($_SESSION['user_id'])) {
    $_SESSION['error'] = "Nie jesteś zalogowany!";
    header("Location: index.php");
    exit();
}

if (!isset($_SESSION['user_ranga'])) {
    require_once 'connect.php';
    $query = "SELECT ranga FROM uzytkownicy WHERE id = $1";
    $result = pg_query_params($connection, $query, array($_SESSION['user_id']));
    if ($result && $row = pg_fetch_assoc($result)) {
        $_SESSION['user_ranga'] = $row['ranga'];
    }
}
