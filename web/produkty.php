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
                        <?php if (isset($_GET['deleted']) && $_GET['deleted'] === 'true'): ?>
                            <script>
                                alert('Produkt został pomyślnie usunięty');
                            </script>
                        <?php endif; ?>
                        
                        <?php if (isset($_GET['error'])): ?>
                            <script>
                                alert('Wystąpił błąd: <?php echo addslashes($_GET['error']); ?>');
                            </script>
                        <?php endif; ?>

                        <h2>Produkty</h2>
                        <form class="client-form" id="searchForm" method="GET">
                            <div class="form-group">
                                <label for="nazwa">Nazwa:</label>
                                <input type="text" id="nazwa" name="nazwa" class="form-control" value="<?php echo isset($_GET['nazwa']) ? htmlspecialchars($_GET['nazwa']) : ''; ?>">
                            </div>
                            
                            <div class="form-group">
                                <label for="cechy">Cechy:</label>
                                <input type="text" id="cechy" name="cechy" class="form-control" value="<?php echo isset($_GET['cechy']) ? htmlspecialchars($_GET['cechy']) : ''; ?>">
                            </div>
                            
                            <div class="form-group">
                                <label for="cena">Cena:</label>
                                <input type="number" step="0.01" id="cena" name="cena" class="form-control" value="<?php echo isset($_GET['cena']) ? htmlspecialchars($_GET['cena']) : ''; ?>">
                            </div>

                            <div class="form-group">
                                <label for="waga">Waga:</label>
                                <input type="number" step="0.01" id="waga" name="waga" class="form-control" value="<?php echo isset($_GET['waga']) ? htmlspecialchars($_GET['waga']) : ''; ?>">
                            </div>

                            <div class="form-group">
                                <label for="ilosc">Ilość:</label>
                                <input type="number" id="ilosc" name="ilosc" class="form-control" value="<?php echo isset($_GET['ilosc']) ? htmlspecialchars($_GET['ilosc']) : ''; ?>">
                            </div>

                            <div class="form-group button-group"><br>
                                <button type="submit" class="search-btn">Szukaj</button>
                                <button type="button" class="reset-btn" onclick="resetForm()">Reset</button>
                            </div>
                        </form>

                        <div class="button-container">
                            <button type="button" class="add-client-btn" onclick="openModal()">Dodaj produkt</button>
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
                                        <th>Cechy</th>
                                        <th class="sortable" onclick="sortTable('cena')">
                                            Cena
                                            <div class="sort-arrows">
                                                <span class="arrow up">▲</span>
                                                <span class="arrow down">▼</span>
                                            </div>
                                        </th>
                                        <th class="sortable" onclick="sortTable('waga')">
                                            Waga
                                            <div class="sort-arrows">
                                                <span class="arrow up">▲</span>
                                                <span class="arrow down">▼</span>
                                            </div>
                                        </th>
                                        <th>Ilość</th>
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

                                    if (!empty($_GET['cechy'])) {
                                        $where_conditions[] = "LOWER(cechy) LIKE LOWER($" . $param_counter . ")";
                                        $params[] = "%" . $_GET['cechy'] . "%";
                                        $param_counter++;
                                    }

                                    if (!empty($_GET['cena'])) {
                                        $where_conditions[] = "cena = $" . $param_counter;
                                        $params[] = $_GET['cena'];
                                        $param_counter++;
                                    }

                                    if (!empty($_GET['waga'])) {
                                        $where_conditions[] = "waga = $" . $param_counter;
                                        $params[] = $_GET['waga'];
                                        $param_counter++;
                                    }

                                    if (!empty($_GET['ilosc'])) {
                                        $where_conditions[] = "ilosc = $" . $param_counter;
                                        $params[] = $_GET['ilosc'];
                                        $param_counter++;
                                    }

                                    $count_query = "SELECT COUNT(*) as total FROM produkty";
                                    if (!empty($where_conditions)) {
                                        $count_query .= " WHERE " . implode(" AND ", $where_conditions);
                                    }
                                    $stmt = pg_prepare($connection, "count_filtered", $count_query);
                                    $count_result = pg_execute($connection, "count_filtered", $params);
                                    $total_records = pg_fetch_assoc($count_result)['total'];
                                    $total_pages = ceil($total_records / $records_per_page);

                                    $query = "SELECT * FROM produkty";
                                    if (!empty($where_conditions)) {
                                        $query .= " WHERE " . implode(" AND ", $where_conditions);
                                    }
                                    $query .= " ORDER BY nazwa LIMIT $records_per_page OFFSET $offset";

                                    $stmt = pg_prepare($connection, "select_filtered", $query);
                                    $result = pg_execute($connection, "select_filtered", $params);
                                    
                                    while ($row = pg_fetch_assoc($result)) {
                                        echo "<tr>";
                                        echo "<td>" . htmlspecialchars($row['nazwa']) . "</td>";
                                        echo "<td>" . htmlspecialchars($row['cechy']) . "</td>";
                                        echo "<td>" . htmlspecialchars($row['cena']) . " zł</td>";
                                        echo "<td>" . htmlspecialchars($row['waga']) . " kg</td>";
                                        echo "<td>" . htmlspecialchars($row['ilosc']) . "</td>";
                                        echo "<td>
                                                <button onclick='editProduct(" . $row['id'] . ")'>Edytuj</button>
                                                <form method='POST' action='delete_product.php' style='display: inline;' onsubmit='return confirm(\"Czy na pewno chcesz usunąć ten produkt? Zostanie on również usunięty ze wszystkich paczek!\");'>
                                                    <input type='hidden' name='product_id' value='" . $row['id'] . "'>
                                                    <button type='submit' class='delete-btn'>Usuń</button>
                                                </form>
                                              </td>";
                                        echo "</tr>";
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

                        <div id="productModal" class="modal">
                            <div class="modal-content">
                                <span class="close" onclick="closeModal()">&times;</span>
                                <h3 id="modalTitle">Dodaj nowy produkt</h3>
                                <form id="addProductForm">
                                    <input type="hidden" id="modal-id" name="id">
                                    <div class="form-group">
                                        <label for="modal-nazwa">Nazwa:</label>
                                        <input type="text" id="modal-nazwa" name="nazwa" class="form-control" required>
                                    </div>
                                    
                                    <div class="form-group">
                                        <label for="modal-cechy">Cechy:</label>
                                        <input type="text" id="modal-cechy" name="cechy" class="form-control">
                                    </div>
                                    
                                    <div class="form-group">
                                        <label for="modal-cena">Cena:</label>
                                        <input type="number" step="0.01" id="modal-cena" name="cena" class="form-control" required>
                                    </div>

                                    <div class="form-group">
                                        <label for="modal-waga">Waga (kg):</label>
                                        <input type="number" step="0.01" id="modal-waga" name="waga" class="form-control" required>
                                    </div>

                                    <div class="form-group">
                                        <label for="modal-ilosc">Ilość:</label>
                                        <input type="number" id="modal-ilosc" name="ilosc" class="form-control" required>
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
<script src="produkty.js"></script>