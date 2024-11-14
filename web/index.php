<?php
session_start();
if (isset($_SESSION['user_id'])) {
    header("Location: dashboard.php");
    exit();
}
$errorMessage = isset($_SESSION['error']) ? $_SESSION['error'] : '';
$successMessage = isset($_SESSION['success']) ? $_SESSION['success'] : '';

unset($_SESSION['error']);
unset($_SESSION['success']);


?>
<!DOCTYPE html>
<html lang="pl">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Strona Główna</title>
    <link rel="stylesheet" href="style.css">
    <script>
        function showForm(formType) {
            document.getElementById('loginForm').style.display = formType === 'login' ? 'block' : 'none';
            document.getElementById('registerForm').style.display = formType === 'register' ? 'block' : 'none';
        }
    </script>
</head>
<body>
    <div class="button-bar">
        <button onclick="showForm('login')">Zaloguj się</button>
        <button onclick="showForm('register')">Zarejestruj się</button>
    </div>
    <div class="error-message" style="display: <?= !empty($errorMessage) ? 'block' : 'none'; ?>;">
        <?= $errorMessage ?: 'Brak błędów do wyświetlenia.'; ?>
    </div>
    <div class="success-message" style="display: <?= !empty($successMessage) ? 'block' : 'none'; ?>;">
        <?= $successMessage ?: 'Brak sukcesów do wyświetlenia.'; ?>
    </div>
    <div class="rotating-image"></div>
    <div class="form-container">
        <div id="loginForm" style="display:none;">
            <h2>Zaloguj się</h2>
            <form action="login.php" method="post">
                <label for="username">Login:</label>
                <input type="text" id="username" name="username" required autocomplete="new-username">
                <label for="password">Hasło:</label>
                <input type="password" id="password" name="password" required autocomplete="new-password">
                <button type="submit">Zaloguj się</button>
            </form>
        </div>

        <div id="registerForm" style="display:none;">
            <h2>Rejestracja</h2>
            <form action="register.php" method="post">
                <label for="firstName">Imię:</label>
                <input type="text" id="firstName" name="firstName" required autocomplete="given-name" class="form-input">
                
                <label for="lastName">Nazwisko:</label>
                <input type="text" id="lastName" name="lastName" required autocomplete="family-name" class="form-input">
                
                <label for="newUsername">Login:</label>
                <input type="text" id="newUsername" name="newUsername" required autocomplete="new-username" class="form-input">
                
                <label for="newPassword">Hasło:</label>
                <input type="password" id="newPassword" name="newPassword" required autocomplete="new-password" class="form-input">
                
                <label for="confirmPassword">Powtórz Hasło:</label>
                <input type="password" id="confirmPassword" name="confirmPassword" required autocomplete="new-password" class="form-input">
                
                <label for="email">E-mail:</label>
                <input type="email" id="email" name="email" required autocomplete="email" class="form-input">
                
                <label for="role">Rola użytkownika:</label>
                <select id="role" name="role" required class="form-input">
                    <option value="Administrator">Administrator</option>
                    <option value="Sprzedawca">Sprzedawca</option>
                    <option value="Magazynier">Magazynier</option>
                </select>
                
                <button type="submit" class="form-button">Zarejestruj się</button>
            </form>
        </div>
    </div>
</body>
</html>
