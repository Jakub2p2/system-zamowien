<?php
require 'czy_zalogowany.php';
require 'connect.php';
require 'menu_permissions.php';

$current_page = basename($_SERVER['PHP_SELF']);
if (!hasPageAccess($_SESSION['user_ranga'], $current_page)) {
    header("Location: dashboard.php");
    exit();
}

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
                <button id="mobile-menu-btn" class="mobile-menu-btn">
                    ☰
                </button>
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
                <div class="side-menu" id="sideMenu">
                    <ul>
                        <?php foreach(getUserMenuItems($_SESSION['user_ranga']) as $item): ?>
                            <li><a href="<?php echo $item['url']; ?>"><?php echo $item['name']; ?></a></li>
                        <?php endforeach; ?>
                    </ul>
                </div>

                <div class="content-area">
                    <div class="main-content">
                        <h2>Zmiana hasła</h2>
                        
                        <div class="password-form-container">
                            <form id="changePasswordForm">
                                <div class="form-group">
                                    <label for="old_password">Stare hasło:</label>
                                    <input type="password" id="old_password" name="old_password" class="form-control" required>
                                </div>
                                
                                <div class="form-group">
                                    <label for="new_password">Nowe hasło:</label>
                                    <input type="password" id="new_password" name="new_password" class="form-control" required>
                                </div>
                                
                                <div class="form-group">
                                    <label for="confirm_password">Powtórz nowe hasło:</label>
                                    <input type="password" id="confirm_password" name="confirm_password" class="form-control" required>
                                </div>

                                <div class="form-group">
                                    <button type="submit" class="submit-btn">Zmień hasło</button>
                                </div>
                            </form>
                        </div>
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
<script src="menu.js"></script>
<script>
document.getElementById('changePasswordForm').addEventListener('submit', function(e) {
    e.preventDefault();
    
    const newPassword = document.getElementById('new_password').value;
    const confirmPassword = document.getElementById('confirm_password').value;
    
    if (newPassword !== confirmPassword) {
        alert('Nowe hasła nie są identyczne!');
        return;
    }
    
    const formData = new FormData(this);
    
    fetch('update_password.php', {
        method: 'POST',
        body: formData
    })
    .then(response => response.json())
    .then(data => {
        if (data.success) {
            alert('Hasło zostało zmienione pomyślnie');
            window.location.href = 'dashboard.php';
        } else {
            alert(data.message || 'Wystąpił błąd podczas zmiany hasła');
        }
    })
    .catch(error => {
        alert('Wystąpił błąd podczas zmiany hasła');
    });
});
</script> 