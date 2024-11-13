<?php
function getUserMenuItems($ranga) {
    $menu_items = array(
        'Administrator' => [
            ['url' => 'dashboard.php', 'name' => 'Strona główna'],
            ['url' => 'paczki.php', 'name' => 'Zarządzanie paczkami'],
            ['url' => 'produkty.php', 'name' => 'Zarządzanie produktami'],
            ['url' => 'klienci.php', 'name' => 'Zarządzanie klientami'],
            ['url' => 'uzytkownicy.php', 'name' => 'Lista użytkowników'],
            ['url' => 'dostawy.php', 'name' => 'Zarządzanie dostawami']
        ],
        'Magazynier' => [
            ['url' => 'dashboard.php', 'name' => 'Strona główna'],
            ['url' => 'paczki.php', 'name' => 'Zarządzanie paczkami'],
            ['url' => 'produkty.php', 'name' => 'Zarządzanie produktami']
        ],
        'Sprzedawca' => [
            ['url' => 'dashboard.php', 'name' => 'Strona główna'],
            ['url' => 'paczki.php', 'name' => 'Zarządzanie paczkami'],
            ['url' => 'klienci.php', 'name' => 'Zarządzanie klientami']
        ]
    );

    return isset($menu_items[$ranga]) ? $menu_items[$ranga] : [];
}

function hasPageAccess($ranga, $current_page) {
    $public_pages = ['zmien_haslo.php'];
    
    if (in_array($current_page, $public_pages)) {
        return true;
    }

    $allowed_pages = array_column(getUserMenuItems($ranga), 'url');
    return in_array($current_page, $allowed_pages);
}

if (isset($_SESSION['user_ranga'])) {
    $menu_items = getUserMenuItems($_SESSION['user_ranga']);
} else {
    $menu_items = [];
} 