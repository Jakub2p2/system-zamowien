<?php
session_start();

if (!isset($_SESSION['user_id'])) {
    $_SESSION['error'] = "Nie jesteś zalogowany!";
    header("Location: index.php");
    exit();
}
