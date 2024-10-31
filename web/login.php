<?php
     session_start();
require 'connect.php';

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $username = $_POST['username'];
    $password = $_POST['password'];

    if (!$connection) {
        die("Błąd połączenia z bazą danych.");
    }

    $query = "SELECT * FROM uzytkownicy WHERE username = $1 LIMIT 1";
    $result = pg_query_params($connection, $query, array($username));

    if ($result && pg_num_rows($result) > 0) {
        $user = pg_fetch_assoc($result);

        if ($password === $user['password']) {
            session_start();
            $_SESSION['user_id'] = $user['id'];
            $_SESSION['username'] = $user['username'];
            header("Location: dashboard.php");
            exit();
        } else {
       
            $_SESSION['error'] = "Nieprawidłowy login lub hasło.";
            header("Location: " . $_SERVER['HTTP_REFERER']); 
            exit();
        }
    } else {
        $_SESSION['error'] = "Nieprawidłowy login lub hasło.";
        header("Location: " . $_SERVER['HTTP_REFERER']);
        exit();
    }
}
?>
