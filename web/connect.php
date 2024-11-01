<?php

$host = '185.157.80.106';
$db = 'Uzytkownicy';
$user = 'postgres';
$pass = '123';
$port = '5432';

$connectionString = "host=$host port=$port dbname=$db user=$user password=$pass";
$connection = pg_connect($connectionString);

if (!$connection) {
    throw new Exception("Błąd połączenia z bazą danych.");
}

?>
