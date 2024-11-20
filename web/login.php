<?php
session_start();
require 'connect.php';

try {
    if (!isset($_POST['username']) || !isset($_POST['password'])) {
        $_SESSION['error'] = 'Wprowadź login i hasło';
        header('Location: index.php');
        exit();
    }

    $login = $_POST['username'];
    $password = md5($_POST['password']);

    $query = "SELECT id, ranga FROM uzytkownicy WHERE login = $1 AND haslo = $2";
    $result = pg_query_params($connection, $query, array($login, $password));

    if ($result && pg_num_rows($result) > 0) {
        $user = pg_fetch_assoc($result);
        
        $_SESSION['user_id'] = $user['id'];
        $_SESSION['user_ranga'] = $user['ranga'];
        
        header('Location: dashboard.php');
        exit();
    } else {
        $_SESSION['error'] = 'Nieprawidłowy login lub hasło';
        header('Location: index.php');
        exit();
    }

} catch (Exception $e) {
    $_SESSION['error'] = 'Wystąpił błąd podczas logowania';
    header('Location: index.php');
    exit();
}
?>
