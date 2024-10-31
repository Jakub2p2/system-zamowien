<?php
session_start();
if (isset($_SESSION['username'])) {
    echo "Witaj, " . htmlspecialchars($_SESSION['username']) . "!";
} else {
    echo "Witaj!";
}
?>
