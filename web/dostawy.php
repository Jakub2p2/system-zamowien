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
                        <h2>Dostawy</h2>
                        
                        <div class="button-container">
                            <button type="button" class="add-client-btn" onclick="openModal()">Dodaj dostawcę</button>
                        </div>

                        <div class="table-container">
                            <table class="clients-table">
                                <thead>
                                    <tr>
                                        <th>Nazwa</th>
                                        <th>Cena za kg</th>
                                        <th>Cena ubezpieczenia</th>
                                        <th>Link do śledzenia</th>
                                        <th>Akcje</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                        </div>

                        <div id="userModal" class="modal">
                            <div class="modal-content">
                                <span class="close">&times;</span>
                                <h3 id="modalTitle">Dodaj nowego dostawcę</h3>
                                <form id="addUserForm">
                                    <input type="hidden" id="modal-id" name="id">
                                    
                                    <div class="form-group">
                                        <label for="modal-nazwa">Nazwa:</label>
                                        <input type="text" id="modal-nazwa" name="nazwa" class="form-control" required>
                                    </div>
                                    
                                    <div class="form-group">
                                        <label for="modal-cena_za_kg">Cena za kg:</label>
                                        <input type="number" step="0.01" id="modal-cena_za_kg" name="cena_za_kg" class="form-control" required>
                                    </div>

                                    <div class="form-group">
                                        <label for="modal-cena_ubezpieczenia">Cena ubezpieczenia:</label>
                                        <input type="number" step="0.01" id="modal-cena_ubezpieczenia" name="cena_ubezpieczenia" class="form-control" required>
                                    </div>

                                    <div class="form-group">
                                        <label for="modal-link_do_śledzenia">Link do śledzenia:</label>
                                        <input type="text" id="modal-link_do_śledzenia" name="link_do_śledzenia" class="form-control" required>
                                    </div>

                                    <div class="form-group">
                                        <button type="submit" class="submit-btn" id="saveButton">Zapisz</button>
                                    </div>
                                </form>
                            </div>
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
<script src="dostawy.js"></script>
