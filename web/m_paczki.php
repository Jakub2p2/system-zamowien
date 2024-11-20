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
                        <h2>Szczegóły Paczki</h2>
                        <?php
                        if (isset($_GET['id'])) {
                            $id_paczki = htmlspecialchars($_GET['id']);

                            $query = "SELECT p.*, u.imie as creator_imie, u.nazwisko as creator_nazwisko, u.login, 
                                      k.nazwa, k.nip, k.region, k.pesel, k.email, k.telefon, k.adres
                                      FROM paczki p 
                                      LEFT JOIN uzytkownicy u ON p.created_by = u.id 
                                      LEFT JOIN klienci k ON p.klient_id = k.id 
                                      WHERE p.id = $1";
                            $stmt = pg_prepare($connection, "get_package", $query);
                            $result = pg_execute($connection, "get_package", [$id_paczki]);
                            
                            if ($result && $row = pg_fetch_assoc($result)) {
                                echo "<div class='package-details'>";
                                echo "<h3>Paczka o numerze: " . htmlspecialchars($row['id']) . "</h3>";
                                
                                echo "<div style='display: flex; justify-content: space-between;'>";

                                echo "<div class='package-info' style='width: 48%;'>";
                                echo "<p><strong>Dane paczki</strong></p>";
                                echo "<p>Data utworzenia paczki: " . htmlspecialchars($row['data_utworzenia']) . "</p>";
                                echo "<p>Status paczki: " . htmlspecialchars($row['status']) . "</p>";

                                $creator_info = "";
                                if ($row['creator_imie'] && $row['creator_nazwisko'] && $row['login']) {
                                    $creator_info = htmlspecialchars($row['creator_imie'] . " " . $row['creator_nazwisko'] . " (" . $row['login'] . ")");
                                } else {
                                    $creator_info = "Nieznany";
                                }
                                echo "<p>Utworzona przez: " . $creator_info . "</p>";
                                echo "</div>";

                                echo "<div class='package-info' style='width: 48%;'>";
                                echo "<p><strong>Dane klienta</strong></p>";
                                echo "<p>Imię i nazwisko/nazwa: " . htmlspecialchars($row['nazwa']) . "</p>";

                                if (!empty($row['nip'])) {
                                    echo "<p>NIP: " . htmlspecialchars($row['nip']) . "</p>";
                                }
                                if (!empty($row['region'])) {
                                    echo "<p>REGON: " . htmlspecialchars($row['region']) . "</p>";
                                }
                                if (!empty($row['pesel'])) {
                                    echo "<p>PESEL: " . htmlspecialchars($row['pesel']) . "</p>";
                                }
                                
                                if (!empty($row['adres'])) {
                                    echo "<p>Adres: " . htmlspecialchars($row['adres']) . "</p>";
                                }
                                
                                echo "</div>";
                                
                                echo "</div>";

                                echo "<div style='display: flex; justify-content: space-between; margin-top: 20px;'>";

                                $query_stats = "SELECT 
                                    COUNT(*) as total,
                                    COUNT(CASE WHEN spakowany = true THEN 1 END) as packed,
                                    SUM(pp.ilosc * p.cena) as total_value,
                                    SUM(pp.ilosc * p.waga) as total_weight
                                FROM paczki_produkty pp
                                JOIN produkty p ON pp.produkt_id = p.id
                                WHERE pp.paczka_id = $1";

                                $result_stats = pg_query_params($connection, $query_stats, [$id_paczki]);
                                $stats = pg_fetch_assoc($result_stats);

                                $total_products = $stats['total'] ?? 0;
                                $packed_products = $stats['packed'] ?? 0;
                                $total_value = $stats['total_value'] ?? 0;
                                $total_weight = $stats['total_weight'] ?? 0;

                                $pack_percentage = $total_products > 0 ? round(($packed_products / $total_products) * 100) : 0;

                                echo "<div class='package-info' style='width: 48%;'>";
                                echo "<p><strong>Podsumowanie paczki</strong></p>";
                                echo "<p>Status paczki: " . htmlspecialchars($row['status']) . " " . $pack_percentage . "%</p>";
                                echo "<p>Spakowanych produktów: " . $packed_products . "/" . $total_products . "</p>";
                                echo "<p>Wartość paczki: " . number_format($total_value, 2) . " zł</p>";
                                echo "<p>Waga paczki: " . number_format($total_weight, 2) . " kg</p>";
                                echo "</div>";

                                echo "<div class='package-info' style='width: 48%;'>";
                                echo "<p><strong>Szczegóły dostawy</strong></p>";
                                echo "<p>Dostawca: " . (empty($row['dostawca']) ? "-" : htmlspecialchars($row['dostawca'])) . "</p>";
                                echo "<p>Numer listu przewozowego: " . (empty($row['nr_listu']) ? "-" : htmlspecialchars($row['nr_listu'])) . "</p>";
                                echo "<p>Data odbioru paczki: " . (empty($row['data_odbioru']) ? "-" : htmlspecialchars($row['data_odbioru'])) . "</p>";
                                echo "<p>Data dostarczenia paczki: " . (empty($row['data_dostarczenia']) ? "-" : htmlspecialchars($row['data_dostarczenia'])) . "</p>";
                                echo "<p>Koszt transportu: " . (empty($row['koszt_transportu']) ? "0" : htmlspecialchars($row['koszt_transportu'])) . " zł</p>";
                                echo "<p>Ubezpieczenie: " . ($row['ubezpieczenie'] ? "TAK" : "NIE") . "</p>";
                                echo "</div>";

                                echo "</div>";

                                echo "<div class='package-products' style='width: 100%; margin-top: 20px;'>";
                                echo "<h3>Produkty w paczce</h3>";

                                $check_products = "SELECT COUNT(*) as count FROM paczki_produkty WHERE paczka_id = $1";
                                $stmt = pg_prepare($connection, "check_products", $check_products);
                                $result = pg_execute($connection, "check_products", [$id_paczki]);
                                $has_products = (pg_fetch_assoc($result)['count'] > 0);

                                if ($row['status'] == 'nowa') {
                                    echo "<button onclick='openProductModal()' class='add-product-btn'>Dodaj produkt do paczki</button>";
                                }

                                echo "<div class='table-container' style='margin-top: 15px;'>";
                                echo "<table class='products-table'>
                                        <thead>
                                            <tr>";
                                                if ($row['status'] == 'komplementacja paczki') {
                                                    echo "<th>Spakowany</th>";
                                                }
                                            echo "          <th>Nazwa produktu</th>
                                                <th>Cena</th>
                                                <th>Waga</th>
                                                <th>Ilość</th>";
                                                if ($row['status'] == 'nowa') {
                                                    echo "<th>Akcje</th>";
                                                }
                                            echo "      </tr>
                                        </thead>
                                        <tbody>";

                                $query = "SELECT pp.id as pozycja_id, pp.ilosc, pp.spakowany, p.nazwa, p.cena, p.waga 
                                          FROM paczki_produkty pp 
                                          JOIN produkty p ON pp.produkt_id = p.id 
                                          WHERE pp.paczka_id = $1 
                                          ORDER BY pp.created_at DESC";
                                $stmt = pg_prepare($connection, "get_package_products", $query);
                                $result = pg_execute($connection, "get_package_products", [$id_paczki]);

                                while ($produkt = pg_fetch_assoc($result)) {
                                    echo "<tr>";
                                    if ($row['status'] == 'komplementacja paczki') {
                                        echo "<td>
                                                <input type='checkbox' 
                                                       onchange='updatePackedStatus(" . $produkt['pozycja_id'] . ", this.checked, this)'
                                                       " . ($produkt['spakowany'] == 't' ? 'checked' : '') . ">
                                              </td>";
                                    }
                                    echo "<td>" . htmlspecialchars($produkt['nazwa']) . "</td>";
                                    echo "<td>" . htmlspecialchars($produkt['cena']) . " zł</td>";
                                    echo "<td>" . htmlspecialchars($produkt['waga']) . " kg</td>";
                                    echo "<td>" . htmlspecialchars($produkt['ilosc']) . "</td>";
                                    if ($row['status'] == 'nowa') {
                                        echo "<td><button onclick='deleteProduct(" . $produkt['pozycja_id'] . ")' class='delete-btn'>Usuń</button></td>";
                                    }
                                    echo "</tr>";
                                }

                                echo "</tbody></table>";
                                echo "</div>";

                                echo "<div id='productModal' class='modal'>
                                        <div class='modal-content'>
                                            <span class='close' onclick='closeProductModal()'>&times;</span>
                                            <h3>Dodaj produkt do paczki</h3>
                                            <form id='addProductForm'>
                                                <input type='hidden' name='paczka_id' value='" . $id_paczki . "'>
                                                <div class='form-group'>
                                                    <label for='produkt_id'>Wybierz produkt:</label>
                                                    <select id='produkt_id' name='produkt_id' required class='form-control'>
                                                        <option value=''>Wybierz produkt</option>";

                                                $produkty_query = "SELECT * FROM produkty WHERE ilosc > 0 ORDER BY nazwa";
                                                $produkty_result = pg_query($connection, $produkty_query);

                                                while ($produkt = pg_fetch_assoc($produkty_result)) {
                                                    echo "<option value='" . $produkt['id'] . "'>" . 
                                                         htmlspecialchars($produkt['nazwa']) . 
                                                         " (Dostępne: " . $produkt['ilosc'] . " szt., " .
                                                         "Cena: " . $produkt['cena'] . " zł, " .
                                                         "Waga: " . $produkt['waga'] . " kg)</option>";
                                                }

                                                echo "</select>
                                                </div>
                                                <div class='form-group'>
                                                    <label for='ilosc'>Ilość:</label>
                                                    <input type='number' id='ilosc' name='ilosc' min='1' value='1' required class='form-control'>
                                                </div>
                                                <button type='submit' class='submit-btn'>Dodaj</button>
                                            </form>
                                        </div>
                                    </div>";

                                echo "</div>";

                                if ($row['status'] == 'nowa') {
                                    echo "<button id='sendToWarehouse' 
                                          class='action-button " . ($has_products ? '' : 'disabled') . "'
                                          onclick='sendToWarehouse(" . $id_paczki . ")'
                                          " . ($has_products ? '' : 'disabled') . ">
                                            Prześlij do magazynu
                                          </button>";
                                } elseif ($row['status'] == 'towar zamowiony') {
                                    echo "<button class='action-button' onclick='startCompletion(" . $id_paczki . ")'>
                                            Kompletacja paczki
                                          </button>";
                                } elseif ($row['status'] == 'komplementacja paczki') {
                                    $check_packed = "SELECT 
                                                        COUNT(*) as total, 
                                                        COUNT(CASE WHEN spakowany = true THEN 1 END) as packed 
                                                     FROM paczki_produkty 
                                                     WHERE paczka_id = $1";
                                    $stmt = pg_prepare($connection, "check_packed", $check_packed);
                                    $result = pg_execute($connection, "check_packed", [$id_paczki]);
                                    $packed_status = pg_fetch_assoc($result);
                                    $all_packed = ($packed_status['total'] == $packed_status['packed'] && $packed_status['total'] > 0);

                                    echo "<button id='readyToShip' 
                                          class='action-button " . ($all_packed ? '' : 'disabled') . "'
                                          onclick='markReadyToShip(" . $id_paczki . ")'
                                          " . ($all_packed ? '' : 'disabled') . ">
                                            Towar przygotowany do wysyłki
                                          </button>";
                                } elseif ($row['status'] == 'towar przygotowany do wysylki') {
                                    echo "<button class='action-button' onclick='waitForCourier(" . $id_paczki . ")'>
                                            Oczekiwanie na kuriera
                                          </button>";
                                } elseif ($row['status'] == 'oczekiwanie na kuriera') {
                                    echo "<button class='action-button' onclick='markPickedUpByCourier(" . $id_paczki . ")'>
                                            Towar odebrany przez kuriera
                                          </button>";
                                }

                                echo "</div>";
                            } else {
                                echo "<div class='error-message'>Nie znaleziono paczki</div>";
                            }
                        } else {
                            echo "<div class='error-message'>Nie podano ID paczki</div>";
                        }
                        ?>
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
<script src="package_products.js"></script>
</body>
</html>