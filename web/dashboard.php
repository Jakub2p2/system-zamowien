<?php
require 'czy_zalogowany.php';
require 'connect.php';

if (isset($_SESSION['user_id'])) {
    $user_id = $_SESSION['user_id'];
    $query = "SELECT imie, login, ranga FROM uzytkownicy WHERE id = $1";
    
    $stmt = pg_prepare($connection, "get_user", $query);
    $result = pg_execute($connection, "get_user", [$user_id]);

    if ($result) {
        $user = pg_fetch_assoc($result);
        if ($user) {
            $welcomeMessage = htmlspecialchars($user['imie']);
            $userLogin = htmlspecialchars($user['login']);
            $userRanga = htmlspecialchars($user['ranga']);
            ?>
            <div class="button-bar">
                <div class="welcome-message">
                    Witaj, <?php echo $welcomeMessage; ?>!
                </div>
                <div class="user-info" onclick="toggleDropdown()">
                    <img src="img/kolo.png" class="user-icon">
                    <div class="dropdown">
                        <span class="dropbtn"><?php echo $userLogin . ' (' . $userRanga . ')'; ?></span>
                        <div class="dropdown-content" id="myDropdown">
                            <a href="zmien_haslo.php">Zmień hasło</a>
                            <a href="wyloguj.php">Wyloguj</a>
                        </div>
                    </div>
                </div>
            </div>

            <div class="main-container">

                <div class="side-menu">
                    <ul>
                        <li><a href="dashboard.php">Strona główna</a></li>
                        <li><a href="paczki.php">Zarządzanie paczkami</a></li>
                        <li><a href="produkty.php">Zarządzanie produktami</a></li>
                        <li><a href="klienci.php">Zarządzanie klientami</a></li>
                        <li><a href="uzytkownicy.php">Lista użytkowników</a></li>
                        <li><a href="dostawy.php">Zarządzanie dostawami</a></li>
                    </ul>
                </div>
                

                <div class="content-area">

                    <div class="main-content">

                    </div>
                </div>
            </div>
            <?php
        } else {
            echo "Użytkownik nie znaleziony.";
        }
    } else {
        echo "Błąd w zapytaniu.";
    }
} else {
    echo "Witaj!";
}
?>

<link rel="stylesheet" type="text/css" href="main.css">
<script>
function toggleDropdown() {
    document.getElementById("myDropdown").classList.toggle("show");
}

window.onclick = function(event) {
    if (!event.target.closest(".user-info")) {
        var dropdowns = document.getElementsByClassName("dropdown-content");
        for (var i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains("show")) {
                openDropdown.classList.remove("show");
            }
        }
    }
}
</script>
