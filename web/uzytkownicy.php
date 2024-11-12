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
                        <?php
                        $menu_items = getUserMenuItems($_SESSION['user_ranga']);
                        foreach($menu_items as $item): ?>
                            <li><a href="<?php echo $item['url']; ?>"><?php echo $item['name']; ?></a></li>
                        <?php endforeach; ?>
                    </ul>
                </div>

                <div class="content-area">
                    <div class="main-content">
                        <h2>Użytkownicy</h2>
                        <form class="client-form" id="searchForm" method="GET">
                            <div class="form-group">
                                <label for="imie">Imię:</label>
                                <input type="text" id="imie" name="imie" class="form-control" value="<?php echo isset($_GET['imie']) ? htmlspecialchars($_GET['imie']) : ''; ?>">
                            </div>
                            
                            <div class="form-group">
                                <label for="nazwisko">Nazwisko:</label>
                                <input type="text" id="nazwisko" name="nazwisko" class="form-control" value="<?php echo isset($_GET['nazwisko']) ? htmlspecialchars($_GET['nazwisko']) : ''; ?>">
                            </div>

                            <div class="form-group">
                                <label for="login">Login:</label>
                                <input type="text" id="login" name="login" class="form-control" value="<?php echo isset($_GET['login']) ? htmlspecialchars($_GET['login']) : ''; ?>">
                            </div>

                            <div class="form-group">
                                <label for="email">Email:</label>
                                <input type="email" id="email" name="email" class="form-control" value="<?php echo isset($_GET['email']) ? htmlspecialchars($_GET['email']) : ''; ?>">
                            </div>

                            <div class="form-group">
                                <label for="ranga">Ranga:</label>
                                <select id="ranga" name="ranga" class="form-control">
                                    <option value="">Wszystkie</option>
                                    <option value="Administrator" <?php echo (isset($_GET['ranga']) && $_GET['ranga'] === 'Administrator') ? 'selected' : ''; ?>>Administrator</option>
                                    <option value="Sprzedawca" <?php echo (isset($_GET['ranga']) && $_GET['ranga'] === 'Sprzedawca') ? 'selected' : ''; ?>>Sprzedawca</option>
                                    <option value="Magazynier" <?php echo (isset($_GET['ranga']) && $_GET['ranga'] === 'Magazynier') ? 'selected' : ''; ?>>Magazynier</option>
                                </select>
                            </div>

                            <div class="form-group button-group"><br>
                                <button type="submit" class="search-btn">Szukaj</button>
                                <button type="button" class="reset-btn" onclick="resetForm()">Reset</button>
                            </div>
                        </form>
                        
                        <div class="button-container">
                            <button type="button" class="add-client-btn" onclick="openModal()">Dodaj użytkownika</button>
                        </div>

                        <div class="table-container">
                            <table class="clients-table">
                                <thead>
                                    <tr>
                                        <th class="sortable" onclick="sortTable('imie')">
                                            Imię
                                            <div class="sort-arrows">
                                                <span class="arrow up">▲</span>
                                                <span class="arrow down">▼</span>
                                            </div>
                                        </th>
                                        <th class="sortable" onclick="sortTable('nazwisko')">
                                            Nazwisko
                                            <div class="sort-arrows">
                                                <span class="arrow up">▲</span>
                                                <span class="arrow down">▼</span>
                                            </div>
                                        </th>
                                        <th class="sortable" onclick="sortTable('login')">
                                            Login
                                            <div class="sort-arrows">
                                                <span class="arrow up">▲</span>
                                                <span class="arrow down">▼</span>
                                            </div>
                                        </th>
                                        <th class="sortable" onclick="sortTable('email')">
                                            Email
                                            <div class="sort-arrows">
                                                <span class="arrow up">▲</span>
                                                <span class="arrow down">▼</span>
                                            </div>
                                        </th>
                                        <th>Ranga</th>
                                        <th>Akcje</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <?php
                                    $records_per_page = isset($_GET['per_page']) ? (int)$_GET['per_page'] : 10;
                                    $current_page = isset($_GET['page']) ? (int)$_GET['page'] : 1;
                                    $offset = ($current_page - 1) * $records_per_page;

                                    $where_conditions = [];
                                    $params = [];
                                    $param_counter = 1;

                                    if (!empty($_GET['imie'])) {
                                        $where_conditions[] = "LOWER(imie) LIKE LOWER($" . $param_counter . ")";
                                        $params[] = "%" . $_GET['imie'] . "%";
                                        $param_counter++;
                                    }

                                    if (!empty($_GET['nazwisko'])) {
                                        $where_conditions[] = "LOWER(nazwisko) LIKE LOWER($" . $param_counter . ")";
                                        $params[] = "%" . $_GET['nazwisko'] . "%";
                                        $param_counter++;
                                    }

                                    if (!empty($_GET['login'])) {
                                        $where_conditions[] = "LOWER(login) LIKE LOWER($" . $param_counter . ")";
                                        $params[] = "%" . $_GET['login'] . "%";
                                        $param_counter++;
                                    }

                                    if (!empty($_GET['email'])) {
                                        $where_conditions[] = "LOWER(email) LIKE LOWER($" . $param_counter . ")";
                                        $params[] = "%" . $_GET['email'] . "%";
                                        $param_counter++;
                                    }

                                    if (!empty($_GET['ranga'])) {
                                        $where_conditions[] = "ranga = $" . $param_counter;
                                        $params[] = $_GET['ranga'];
                                        $param_counter++;
                                    }

                                    $count_query = "SELECT COUNT(*) as total FROM uzytkownicy";
                                    if (!empty($where_conditions)) {
                                        $count_query .= " WHERE " . implode(" AND ", $where_conditions);
                                    }
                                    $stmt = pg_prepare($connection, "count_filtered", $count_query);
                                    $count_result = pg_execute($connection, "count_filtered", $params);
                                    $total_records = pg_fetch_assoc($count_result)['total'];
                                    $total_pages = ceil($total_records / $records_per_page);

                                    $query = "SELECT * FROM uzytkownicy";
                                    if (!empty($where_conditions)) {
                                        $query .= " WHERE " . implode(" AND ", $where_conditions);
                                    }
                                    $query .= " ORDER BY nazwisko LIMIT $records_per_page OFFSET $offset";

                                    $stmt = pg_prepare($connection, "select_filtered", $query);
                                    $result = pg_execute($connection, "select_filtered", $params);
                                    
                                    if ($result) {
                                        while ($row = pg_fetch_assoc($result)) {
                                            echo "<tr>";
                                            echo "<td>" . htmlspecialchars($row['imie']) . "</td>";
                                            echo "<td>" . htmlspecialchars($row['nazwisko']) . "</td>";
                                            echo "<td>" . htmlspecialchars($row['login']) . "</td>";
                                            echo "<td>" . htmlspecialchars($row['email']) . "</td>";
                                            echo "<td>" . htmlspecialchars($row['ranga']) . "</td>";
                                            echo "<td>
                                                    <button onclick='editUser(" . $row['id'] . ")'>Edytuj</button>
                                                    <button onclick='deleteUser(" . $row['id'] . ")'>Usuń</button>
                                                  </td>";
                                            echo "</tr>";
                                        }
                                    } else {
                                        echo "Wystąpił błąd podczas wykonywania zapytania.";
                                    }
                                    ?>
                                </tbody>
                            </table>
                        </div>

                        <div class="pagination-container">
                            <div class="pagination-info">
                                Idź do: 
                                <select onchange="goToPage(this.value)">
                                    <?php for($i = 1; $i <= $total_pages; $i++): ?>
                                        <option value="<?php echo $i; ?>" <?php echo $i == $current_page ? 'selected' : ''; ?>><?php echo $i; ?></option>
                                    <?php endfor; ?>
                                </select>
                                
                                Liczba: 
                                <select onchange="changeRecordsPerPage(this.value)">
                                    <option value="10" <?php echo $records_per_page == 10 ? 'selected' : ''; ?>>10</option>
                                    <option value="25" <?php echo $records_per_page == 25 ? 'selected' : ''; ?>>25</option>
                                    <option value="50" <?php echo $records_per_page == 50 ? 'selected' : ''; ?>>50</option>
                                    <option value="100" <?php echo $records_per_page == 100 ? 'selected' : ''; ?>>100</option>
                                </select>
                                
                                <?php
                                $start_record = $offset + 1;
                                $end_record = min($offset + $records_per_page, $total_records);
                                echo "$start_record-$end_record z $total_records";
                                ?>
                            </div>
                            
                            <div class="pagination-controls">
                                <a href="?page=1&per_page=<?php echo $records_per_page; ?>" <?php echo $current_page == 1 ? 'class="disabled"' : ''; ?>><<</a>
                                <a href="?page=<?php echo max(1, $current_page-1); ?>&per_page=<?php echo $records_per_page; ?>" <?php echo $current_page == 1 ? 'class="disabled"' : ''; ?>><</a>
                                <a href="?page=<?php echo min($total_pages, $current_page+1); ?>&per_page=<?php echo $records_per_page; ?>" <?php echo $current_page == $total_pages ? 'class="disabled"' : ''; ?>>></a>
                                <a href="?page=<?php echo $total_pages; ?>&per_page=<?php echo $records_per_page; ?>" <?php echo $current_page == $total_pages ? 'class="disabled"' : ''; ?>>>></a>
                            </div>
                        </div>

                        <div id="userModal" class="modal">
                            <div class="modal-content">
                                <span class="close">&times;</span>
                                <h3 id="modalTitle">Dodaj nowego użytkownika</h3>
                                <form id="addUserForm">
                                    <input type="hidden" id="modal-id" name="id">
                                    
                                    <div class="form-group">
                                        <label for="modal-imie">Imię:</label>
                                        <input type="text" id="modal-imie" name="imie" class="form-control" required>
                                    </div>
                                    
                                    <div class="form-group">
                                        <label for="modal-nazwisko">Nazwisko:</label>
                                        <input type="text" id="modal-nazwisko" name="nazwisko" class="form-control" required>
                                    </div>

                                    <div class="form-group">
                                        <label for="modal-login">Login:</label>
                                        <input type="text" id="modal-login" name="login" class="form-control" required>
                                    </div>

                                    <div class="form-group">
                                        <label for="modal-email">Email:</label>
                                        <input type="email" id="modal-email" name="email" class="form-control" required>
                                    </div>

                                    <div class="form-group" id="changePasswordGroup" style="display: none;">
                                        <label>
                                            <input type="checkbox" id="changePassword" name="changePassword">
                                            Zmień hasło
                                        </label>
                                    </div>

                                    <div class="form-group password-group">
                                        <label for="modal-haslo">Hasło:</label>
                                        <input type="password" id="modal-haslo" name="haslo" class="form-control" required>
                                    </div>

                                    <div class="form-group password-group">
                                        <label for="modal-haslo2">Powtórz hasło:</label>
                                        <input type="password" id="modal-haslo2" name="haslo2" class="form-control" required>
                                    </div>

                                    <div class="form-group">
                                        <label for="modal-ranga">Ranga:</label>
                                        <select id="modal-ranga" name="ranga" class="form-control" required>
                                            <option value="Administrator">Administrator</option>
                                            <option value="Sprzedawca">Sprzedawca</option>
                                            <option value="Magazynier">Magazynier</option>
                                        </select>
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
<script src="uzytkownicy.js"></script>