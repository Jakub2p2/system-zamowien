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
                        <h2>Klienci</h2>
                        <form class="client-form" id="searchForm" method="GET">
                            <div class="form-group">
                                <label for="nazwa">Nazwa:</label>
                                <input type="text" id="nazwa" name="nazwa" class="form-control" value="<?php echo isset($_GET['nazwa']) ? htmlspecialchars($_GET['nazwa']) : ''; ?>">
                            </div>
                            
                            <div class="form-group">
                                <label for="nip">NIP:</label>
                                <input type="text" id="nip" name="nip" class="form-control" value="<?php echo isset($_GET['nip']) ? htmlspecialchars($_GET['nip']) : ''; ?>">
                            </div>

                            <div class="form-group">
                                <label for="regon">REGON:</label>
                                <input type="text" id="regon" name="regon" class="form-control" value="<?php echo isset($_GET['regon']) ? htmlspecialchars($_GET['regon']) : ''; ?>">
                            </div>

                            <div class="form-group">
                                <label for="pesel">PESEL:</label>
                                <input type="text" id="pesel" name="pesel" class="form-control" value="<?php echo isset($_GET['pesel']) ? htmlspecialchars($_GET['pesel']) : ''; ?>">
                            </div>

                            <div class="form-group">
                                <label for="email">Email:</label>
                                <input type="email" id="email" name="email" class="form-control" value="<?php echo isset($_GET['email']) ? htmlspecialchars($_GET['email']) : ''; ?>">
                            </div>

                            <div class="form-group">
                                <label for="telefon">Telefon:</label>
                                <input type="tel" id="telefon" name="telefon" class="form-control" value="<?php echo isset($_GET['telefon']) ? htmlspecialchars($_GET['telefon']) : ''; ?>">
                            </div>

                            <div class="form-group">
                                <label for="adres">Adres zamieszkania:</label>
                                <input type="text" id="adres" name="adres" class="form-control" value="<?php echo isset($_GET['adres']) ? htmlspecialchars($_GET['adres']) : ''; ?>">
                            </div>

                            <div class="form-group button-group"><br>
                                <button type="submit" class="search-btn">Szukaj</button>
                                <button type="button" class="reset-btn" onclick="resetForm()">Reset</button>
                            </div>
                        </form>
                        
                        <div class="button-container">
                            <button type="button" class="add-client-btn" onclick="openModal()">Dodaj klienta</button>
                        </div>

                        <div class="table-container">
                            <table class="clients-table">
                                <thead>
                                    <tr>
                                        <th class="sortable" onclick="sortTable('nazwa')">
                                            Nazwa
                                            <div class="sort-arrows">
                                                <span class="arrow up">▲</span>
                                                <span class="arrow down">▼</span>
                                            </div>
                                        </th>
                                        <th>NIP</th>
                                        <th>REGON</th>
                                        <th>PESEL</th>
                                        <th>Email</th>
                                        <th>Telefon</th>
                                        <th>Adres</th>
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

                                    if (!empty($_GET['nazwa'])) {
                                        $where_conditions[] = "LOWER(nazwa) LIKE LOWER($" . $param_counter . ")";
                                        $params[] = "%" . $_GET['nazwa'] . "%";
                                        $param_counter++;
                                    }

                                    if (!empty($_GET['nip'])) {
                                        $where_conditions[] = "nip LIKE $" . $param_counter;
                                        $params[] = "%" . $_GET['nip'] . "%";
                                        $param_counter++;
                                    }

                                    if (!empty($_GET['regon'])) {
                                        $where_conditions[] = "region LIKE $" . $param_counter;
                                        $params[] = "%" . $_GET['regon'] . "%";
                                        $param_counter++;
                                    }

                                    if (!empty($_GET['pesel'])) {
                                        $where_conditions[] = "pesel LIKE $" . $param_counter;
                                        $params[] = "%" . $_GET['pesel'] . "%";
                                        $param_counter++;
                                    }

                                    if (!empty($_GET['email'])) {
                                        $where_conditions[] = "LOWER(email) LIKE LOWER($" . $param_counter . ")";
                                        $params[] = "%" . $_GET['email'] . "%";
                                        $param_counter++;
                                    }

                                    if (!empty($_GET['telefon'])) {
                                        $where_conditions[] = "telefon LIKE $" . $param_counter;
                                        $params[] = "%" . $_GET['telefon'] . "%";
                                        $param_counter++;
                                    }

                                    if (!empty($_GET['adres'])) {
                                        $where_conditions[] = "LOWER(adres) LIKE LOWER($" . $param_counter . ")";
                                        $params[] = "%" . $_GET['adres'] . "%";
                                        $param_counter++;
                                    }

                                    $count_query = "SELECT COUNT(*) as total FROM klienci";
                                    if (!empty($where_conditions)) {
                                        $count_query .= " WHERE " . implode(" AND ", $where_conditions);
                                    }
                                    $stmt = pg_prepare($connection, "count_filtered", $count_query);
                                    $count_result = pg_execute($connection, "count_filtered", $params);
                                    $total_records = pg_fetch_assoc($count_result)['total'];
                                    $total_pages = ceil($total_records / $records_per_page);

                                    $query = "SELECT * FROM klienci";
                                    if (!empty($where_conditions)) {
                                        $query .= " WHERE " . implode(" AND ", $where_conditions);
                                    }
                                    $query .= " ORDER BY nazwa LIMIT $records_per_page OFFSET $offset";

                                    $stmt = pg_prepare($connection, "select_filtered", $query);
                                    $result = pg_execute($connection, "select_filtered", $params);
                                    
                                    if ($result) {
                                        while ($row = pg_fetch_assoc($result)) {
                                            echo "<tr>";
                                            echo "<td>" . htmlspecialchars($row['nazwa']) . "</td>";
                                            echo "<td>" . htmlspecialchars($row['nip']) . "</td>";
                                            echo "<td>" . htmlspecialchars($row['region']) . "</td>";
                                            echo "<td>" . htmlspecialchars($row['pesel']) . "</td>";
                                            echo "<td>" . htmlspecialchars($row['email']) . "</td>";
                                            echo "<td>" . htmlspecialchars($row['telefon']) . "</td>";
                                            echo "<td>" . htmlspecialchars($row['adres']) . "</td>";
                                            echo "<td>
                                                    <button onclick='editClient(" . $row['id'] . ")'>Edytuj</button>
                                                    <button onclick='deleteClient(" . $row['id'] . ")'>Usuń</button>
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

                        <div id="clientModal" class="modal">
                            <div class="modal-content">
                                <span class="close" onclick="closeModal()">&times;</span>
                                <h3 id="modalTitle">Dodaj nowego klienta</h3>
                                <form id="addClientForm">
                                    <input type="hidden" id="modal-id" name="id">
                                    <div class="form-group">
                                        <label for="modal-nazwa">Nazwa:</label>
                                        <input type="text" id="modal-nazwa" name="nazwa" class="form-control" required>
                                    </div>
                                    
                                    <div class="form-group">
                                        <label for="modal-nip">NIP:</label>
                                        <input type="text" id="modal-nip" name="nip" class="form-control">
                                    </div>

                                    <div class="form-group">
                                        <label for="modal-regon">REGON:</label>
                                        <input type="text" id="modal-regon" name="regon" class="form-control">
                                    </div>

                                    <div class="form-group">
                                        <label for="modal-pesel">PESEL:</label>
                                        <input type="text" id="modal-pesel" name="pesel" class="form-control">
                                    </div>

                                    <div class="form-group">
                                        <label for="modal-email">Email:</label>
                                        <input type="email" id="modal-email" name="email" class="form-control" required>
                                    </div>

                                    <div class="form-group">
                                        <label for="modal-telefon">Telefon:</label>
                                        <input type="tel" id="modal-telefon" name="telefon" class="form-control">
                                    </div>

                                    <div class="form-group">
                                        <label for="modal-adres">Adres zamieszkania:</label>
                                        <input type="text" id="modal-adres" name="adres" class="form-control">
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
<script src = "klienci.js"></script>