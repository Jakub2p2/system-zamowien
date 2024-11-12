<?php
require_once 'connect.php';
require_once 'czy_zalogowany.php';

header('Content-Type: application/json');

try {
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

    $allowed_columns = ['imie', 'nazwisko', 'login', 'email'];
    $sort_column = isset($_GET['column']) && in_array($_GET['column'], $allowed_columns) ? $_GET['column'] : 'nazwisko';
    $sort_direction = isset($_GET['direction']) && strtoupper($_GET['direction']) === 'DESC' ? 'DESC' : 'ASC';

    $count_query = "SELECT COUNT(*) as total FROM uzytkownicy";
    if (!empty($where_conditions)) {
        $count_query .= " WHERE " . implode(" AND ", $where_conditions);
    }
    
    $count_result = pg_query_params($connection, $count_query, $params);
    $total_records = pg_fetch_assoc($count_result)['total'];

    $query = "SELECT id, imie, nazwisko, login, email, ranga FROM uzytkownicy";
    if (!empty($where_conditions)) {
        $query .= " WHERE " . implode(" AND ", $where_conditions);
    }
    $query .= " ORDER BY $sort_column $sort_direction";
    if ($records_per_page > 0) {
        $query .= " LIMIT $records_per_page OFFSET $offset";
    }

    $result = pg_query_params($connection, $query, $params);
    if (!$result) {
        throw new Exception(pg_last_error($connection));
    }

    $users = [];
    while ($row = pg_fetch_assoc($result)) {
        $users[] = $row;
    }

    echo json_encode([
        'users' => $users,
        'total_records' => (int)$total_records,
        'current_page' => $current_page,
        'records_per_page' => $records_per_page
    ]);

} catch (Exception $e) {
    http_response_code(500);
    echo json_encode([
        'error' => true,
        'message' => 'Wystąpił błąd podczas pobierania danych: ' . $e->getMessage()
    ]);
} 