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
                        <h2>Paczki</h2>
                        <form class="client-form" id="searchForm" method="GET">
                            <div class="form-group">
                                <label for="nr_listu">Numer listu:</label>
                                <input type="text" id="nr_listu" name="nr_listu" class="form-control" value="<?php echo isset($_GET['nr_listu']) ? htmlspecialchars($_GET['nr_listu']) : ''; ?>">
                            </div>
                            
                            <div class="form-group">
                                <label for="status">Status:</label>
                                <input type="text" id="status" name="status" class="form-control" value="<?php echo isset($_GET['status']) ? htmlspecialchars($_GET['status']) : ''; ?>">
                            </div>
                            
                            <div class="form-group">
                                <label for="data_utworzenia">Data utworzenia:</label>
                                <input type="date" id="data_utworzenia" name="data_utworzenia" class="form-control" value="<?php echo isset($_GET['data_utworzenia']) ? htmlspecialchars($_GET['data_utworzenia']) : ''; ?>">
                            </div>

                            <div class="form-group">
                                <label for="data_odbioru">Data odbioru:</label>
                                <input type="date" id="data_odbioru" name="data_odbioru" class="form-control" value="<?php echo isset($_GET['data_odbioru']) ? htmlspecialchars($_GET['data_odbioru']) : ''; ?>">
                            </div>

                            <div class="form-group">
                                <label for="data_dostarczenia">Data dostarczenia:</label>
                                <input type="date" id="data_dostarczenia" name="data_dostarczenia" class="form-control" value="<?php echo isset($_GET['data_dostarczenia']) ? htmlspecialchars($_GET['data_dostarczenia']) : ''; ?>">
                            </div>

                            <div class="form-group">
                                <label for="wartosc">Wartość:</label>
                                <input type="number" step="0.01" id="wartosc" name="wartosc" class="form-control" value="<?php echo isset($_GET['wartosc']) ? htmlspecialchars($_GET['wartosc']) : ''; ?>">
                            </div>

                            <div class="form-group">
                                <label for="ubezpieczenie">Ubezpieczenie:</label>
                                <input type="number" step="0.01" id="ubezpieczenie" name="ubezpieczenie" class="form-control" value="<?php echo isset($_GET['ubezpieczenie']) ? htmlspecialchars($_GET['ubezpieczenie']) : ''; ?>">
                            </div>

                            <div class="form-group">
                                <label for="koszt_transportu">Koszt transportu:</label>
                                <input type="number" step="0.01" id="koszt_transportu" name="koszt_transportu" class="form-control" value="<?php echo isset($_GET['koszt_transportu']) ? htmlspecialchars($_GET['koszt_transportu']) : ''; ?>">
                            </div>

                            <div class="form-group button-group"><br>
                                <button type="submit" class="search-btn">Szukaj</button>
                                <button type="button" class="reset-btn" onclick="resetForm()">Reset</button>
                            </div>
                        </form>
                        
                        <div class="button-container">
                            <button type="button" class="add-client-btn" onclick="openModal()">Dodaj paczkę</button>
                        </div>

                        <div class="table-container">
                            <table class="clients-table">
                                <thead>
                                    <tr>
                                        <th class="sortable" onclick="sortTable('nr_listu')">
                                            Numer listu
                                            <div class="sort-arrows">
                                                <span class="arrow up <?php echo ($sort === 'nr_listu' && $order === 'ASC') ? 'active' : ''; ?>">▲</span>
                                                <span class="arrow down <?php echo ($sort === 'nr_listu' && $order === 'DESC') ? 'active' : ''; ?>">▼</span>
                                            </div>
                                        </th>
                                        <th class="sortable" onclick="sortTable('status')">
                                            Status
                                            <div class="sort-arrows">
                                                <span class="arrow up <?php echo ($sort === 'status' && $order === 'ASC') ? 'active' : ''; ?>">▲</span>
                                                <span class="arrow down <?php echo ($sort === 'status' && $order === 'DESC') ? 'active' : ''; ?>">▼</span>
                                            </div>
                                        </th>
                                        <th class="sortable" onclick="sortTable('data_utworzenia')">
                                            Data utworzenia
                                            <div class="sort-arrows">
                                                <span class="arrow up <?php echo ($sort === 'data_utworzenia' && $order === 'ASC') ? 'active' : ''; ?>">▲</span>
                                                <span class="arrow down <?php echo ($sort === 'data_utworzenia' && $order === 'DESC') ? 'active' : ''; ?>">▼</span>
                                            </div>
                                        </th>
                                        <th class="sortable" onclick="sortTable('data_odbioru')">
                                            Data odbioru
                                            <div class="sort-arrows">
                                                <span class="arrow up <?php echo ($sort === 'data_odbioru' && $order === 'ASC') ? 'active' : ''; ?>">▲</span>
                                                <span class="arrow down <?php echo ($sort === 'data_odbioru' && $order === 'DESC') ? 'active' : ''; ?>">▼</span>
                                            </div>
                                        </th>
                                        <th class="sortable" onclick="sortTable('data_dostarczenia')">
                                            Data dostarczenia
                                            <div class="sort-arrows">
                                                <span class="arrow up <?php echo ($sort === 'data_dostarczenia' && $order === 'ASC') ? 'active' : ''; ?>">▲</span>
                                                <span class="arrow down <?php echo ($sort === 'data_dostarczenia' && $order === 'DESC') ? 'active' : ''; ?>">▼</span>
                                            </div>
                                        </th>
                                        <th class="sortable" onclick="sortTable('wartosc')">
                                            Wartość
                                            <div class="sort-arrows">
                                                <span class="arrow up <?php echo ($sort === 'wartosc' && $order === 'ASC') ? 'active' : ''; ?>">▲</span>
                                                <span class="arrow down <?php echo ($sort === 'wartosc' && $order === 'DESC') ? 'active' : ''; ?>">▼</span>
                                            </div>
                                        </th>
                                        <th class="sortable" onclick="sortTable('ubezpieczenie')">
                                            Ubezpieczenie
                                            <div class="sort-arrows">
                                                <span class="arrow up <?php echo ($sort === 'ubezpieczenie' && $order === 'ASC') ? 'active' : ''; ?>">▲</span>
                                                <span class="arrow down <?php echo ($sort === 'ubezpieczenie' && $order === 'DESC') ? 'active' : ''; ?>">▼</span>
                                            </div>
                                        </th>
                                        <th class="sortable" onclick="sortTable('koszt_transportu')">
                                            Koszt transportu
                                            <div class="sort-arrows">
                                                <span class="arrow up <?php echo ($sort === 'koszt_transportu' && $order === 'ASC') ? 'active' : ''; ?>">▲</span>
                                                <span class="arrow down <?php echo ($sort === 'koszt_transportu' && $order === 'DESC') ? 'active' : ''; ?>">▼</span>
                                            </div>
                                        </th>
                                        <th>Klient</th>
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

                                    if (!empty($_GET['nr_listu'])) {
                                        $where_conditions[] = "LOWER(nr_listu) LIKE LOWER($" . $param_counter . ")";
                                        $params[] = "%" . $_GET['nr_listu'] . "%";
                                        $param_counter++;
                                    }

                                    if (!empty($_GET['status'])) {
                                        $where_conditions[] = "LOWER(status) LIKE LOWER($" . $param_counter . ")";
                                        $params[] = "%" . $_GET['status'] . "%";
                                        $param_counter++;
                                    }

                                    if (!empty($_GET['data_utworzenia'])) {
                                        $where_conditions[] = "data_utworzenia::date = $" . $param_counter;
                                        $params[] = $_GET['data_utworzenia'];
                                        $param_counter++;
                                    }

                                    if (!empty($_GET['data_odbioru'])) {
                                        $where_conditions[] = "data_odbioru::date = $" . $param_counter;
                                        $params[] = $_GET['data_odbioru'];
                                        $param_counter++;
                                    }

                                    if (!empty($_GET['data_dostarczenia'])) {
                                        $where_conditions[] = "data_dostarczenia::date = $" . $param_counter;
                                        $params[] = $_GET['data_dostarczenia'];
                                        $param_counter++;
                                    }

                                    if (!empty($_GET['wartosc'])) {
                                        $where_conditions[] = "wartosc::text LIKE $" . $param_counter;
                                        $params[] = "%" . $_GET['wartosc'] . "%";
                                        $param_counter++;
                                    }

                                    if (!empty($_GET['ubezpieczenie'])) {
                                        $where_conditions[] = "ubezpieczenie::text LIKE $" . $param_counter;
                                        $params[] = "%" . $_GET['ubezpieczenie'] . "%";
                                        $param_counter++;
                                    }

                                    if (!empty($_GET['koszt_transportu'])) {
                                        $where_conditions[] = "koszt_transportu::text LIKE $" . $param_counter;
                                        $params[] = "%" . $_GET['koszt_transportu'] . "%";
                                        $param_counter++;
                                    }

                                    $count_query = "SELECT COUNT(*) as total FROM paczki";
                                    if (!empty($where_conditions)) {
                                        $count_query .= " WHERE " . implode(" AND ", $where_conditions);
                                    }
                                    $stmt = pg_prepare($connection, "count_filtered", $count_query);
                                    $count_result = pg_execute($connection, "count_filtered", $params);
                                    $total_records = pg_fetch_assoc($count_result)['total'];
                                    $total_pages = ceil($total_records / $records_per_page);

                                    $sort = isset($_GET['sort']) ? $_GET['sort'] : 'nr_listu';
                                    $order = isset($_GET['order']) ? $_GET['order'] : 'asc';

                                    $allowed_sort_columns = ['nr_listu', 'status', 'data_utworzenia', 'data_odbioru', 
                                                            'data_dostarczenia', 'wartosc', 'ubezpieczenie', 'koszt_transportu'];

                                    if (!in_array($sort, $allowed_sort_columns)) {
                                        $sort = 'nr_listu';
                                    }

                                    $order = strtolower($order) === 'desc' ? 'DESC' : 'ASC';

                                    $query = "SELECT p.*, k.imie, k.nazwisko, k.nip 
                                              FROM paczki p 
                                              LEFT JOIN klienci k ON p.klient_id = k.id";
                                    if (!empty($where_conditions)) {
                                        $query .= " WHERE " . implode(" AND ", $where_conditions);
                                    }
                                    $query .= " ORDER BY p." . pg_escape_identifier($connection, $sort) . " " . $order;
                                    $query .= " LIMIT $records_per_page OFFSET $offset";

                                    $stmt = pg_prepare($connection, "select_filtered", $query);
                                    $result = pg_execute($connection, "select_filtered", $params);

                                    if ($result === false) {
                                        echo "Błąd zapytania: " . pg_last_error($connection);
                                    } else {
                                        while ($row = pg_fetch_assoc($result)) {
                                            echo "<tr>";
                                            echo "<td>" . htmlspecialchars($row['nr_listu']) . "</td>";
                                            echo "<td>" . htmlspecialchars($row['status']) . "</td>";
                                            echo "<td>" . htmlspecialchars($row['data_utworzenia']) . "</td>";
                                            echo "<td>" . htmlspecialchars($row['data_odbioru']) . "</td>";
                                            echo "<td>" . htmlspecialchars($row['data_dostarczenia']) . "</td>";
                                            echo "<td>" . htmlspecialchars($row['wartosc']) . "</td>";
                                            echo "<td>" . htmlspecialchars($row['ubezpieczenie']) . "</td>";
                                            echo "<td>" . htmlspecialchars($row['koszt_transportu']) . "</td>";
                                            
                                            $klient_info = htmlspecialchars($row['nazwisko'] . ' ' . $row['imie']);
                                            if (!empty($row['nip'])) {
                                                $klient_info .= ' (NIP: ' . htmlspecialchars($row['nip']) . ')';
                                            }
                                            echo "<td>" . $klient_info . "</td>";
                                            
                                            echo "<td>
                                                    <button onclick='editPackage(" . $row['id'] . ")'>Edytuj</button>
                                                    <button onclick='deletePackage(" . $row['id'] . ")'>Usuń</button>
                                                  </td>";
                                            echo "</tr>";
                                        }
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

                        <div id="packageModal" class="modal">
                            <div class="modal-content">
                                <span class="close" onclick="closeModal()">&times;</span>
                                <h3 id="modalTitle">Dodaj nową paczkę</h3>
                                <form id="addPackageForm">
                                    <input type="hidden" id="modal-id" name="id">
                                    
                                    <div class="form-group">
                                        <label for="modal-nr_listu">Numer listu:</label>
                                        <input type="text" id="modal-nr_listu" name="nr_listu" class="form-control" required>
                                    </div>
                                    
                                    <div class="form-group">
                                        <label for="modal-status">Status:</label>
                                        <input type="text" id="modal-status" name="status" class="form-control" required>
                                    </div>
                                    
                                    <div class="form-group">
                                        <label for="modal-data_utworzenia">Data utworzenia:</label>
                                        <input type="date" id="modal-data_utworzenia" name="data_utworzenia" class="form-control">
                                    </div>

                                    <div class="form-group">
                                        <label for="modal-data_odbioru">Data odbioru:</label>
                                        <input type="date" id="modal-data_odbioru" name="data_odbioru" class="form-control">
                                    </div>

                                    <div class="form-group">
                                        <label for="modal-data_dostarczenia">Data dostarczenia:</label>
                                        <input type="date" id="modal-data_dostarczenia" name="data_dostarczenia" class="form-control">
                                    </div>

                                    <div class="form-group">
                                        <label for="modal-wartosc">Wartość:</label>
                                        <input type="number" step="0.01" id="modal-wartosc" name="wartosc" class="form-control">
                                    </div>

                                    <div class="form-group">
                                        <label for="modal-ubezpieczenie">Ubezpieczenie:</label>
                                        <input type="number" step="0.01" id="modal-ubezpieczenie" name="ubezpieczenie" class="form-control">
                                    </div>

                                    <div class="form-group">
                                        <label for="modal-koszt_transportu">Koszt transportu:</label>
                                        <input type="number" step="0.01" id="modal-koszt_transportu" name="koszt_transportu" class="form-control">
                                    </div>

                                    <div class="form-group">
                                        <label for="modal-klient_id">Klient:</label>
                                        <select id="modal-klient_id" name="klient_id" class="form-control" required>
                                            <option value="">Wybierz klienta</option>
                                            <?php
                                            $klienci_query = "SELECT id, imie, nazwisko, nip FROM klienci ORDER BY nazwisko, imie";
                                            $klienci_result = pg_query($connection, $klienci_query);
                                            
                                            while ($klient = pg_fetch_assoc($klienci_result)) {
                                                $klient_info = htmlspecialchars($klient['nazwisko'] . ' ' . $klient['imie']);
                                                if (!empty($klient['nip'])) {
                                                    $klient_info .= ' (NIP: ' . htmlspecialchars($klient['nip']) . ')';
                                                }
                                                echo '<option value="' . $klient['id'] . '">' . $klient_info . '</option>';
                                            }
                                            ?>
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
<script src="paczki.js"></script>