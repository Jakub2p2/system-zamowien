<?php
session_start();

if ($_SERVER['REQUEST_METHOD'] === 'POST') {

    $firstName = trim($_POST['firstName']);
    $lastName = trim($_POST['lastName']);
    $newUsername = trim($_POST['newUsername']);
    $newPassword = $_POST['newPassword'];
    $confirmPassword = $_POST['confirmPassword'];
    $email = trim($_POST['email']);
    $role = trim($_POST['role']);

    if ($newPassword !== $confirmPassword) {
        $_SESSION['error'] = 'Hasła nie zgadzają się.';
        header('Location: index.php');
        exit;
    }

    include 'connect.php';

    $result = pg_query_params($connection, "SELECT COUNT(*) FROM uzytkownicy WHERE login = $1", array($newUsername));
    $usernameCount = pg_fetch_result($result, 0, 0);

    if ($usernameCount > 0) {
        $_SESSION['error'] = 'Login jest już zajęty.';
        header('Location: index.php');
        exit;
    }

    $result = pg_query_params($connection, "SELECT COUNT(*) FROM uzytkownicy WHERE email = $1", array($email));
    $emailCount = pg_fetch_result($result, 0, 0);

    if ($emailCount > 0) {
        $_SESSION['error'] = 'Email jest już zajęty.';
        header('Location: index.php');
        exit;
    }

    $hashedPassword = md5($newPassword);

    $result = pg_query_params($connection, 
        "INSERT INTO uzytkownicy (imie, nazwisko, login, haslo, email, ranga) 
         VALUES ($1, $2, $3, $4, $5, $6);", 
        array($firstName, $lastName, $newUsername, $hashedPassword, $email, $role)
    );

    if (!$result) {
        $_SESSION['error'] = 'Wystąpił błąd podczas rejestracji: ' . pg_last_error($connection);
        pg_close($connection);
        header('Location: index.php');
        exit;
    }

    pg_close($connection);
    
    $_SESSION['success'] = 'Rejestracja przebiegła pomyślnie.';
    header('Location: index.php');
    exit;
}
?>
