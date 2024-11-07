<?php

$host = 'pg-26a19d25-paczkimagazyn.h.aivencloud.com';
$db = 'paczuszki';
$user = 'avnadmin';
$pass = 'AVNS_3UbLex9BxU_ZYRZvxaY';
$port = '13890';

$connectionString = "host=$host port=$port dbname=$db user=$user password=$pass";
$connection = pg_connect($connectionString);

if (!$connection) {
    throw new Exception("Błąd połączenia z bazą danych.");
}

?>
