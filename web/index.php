<?php
session_start();
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
    <div class="error-message" style="display: <?= isset($_SESSION['error']) ? 'block' : 'none'; ?>;">
        <?= isset($_SESSION['error']) ? $_SESSION['error'] : 'Brak błędów do wyświetlenia.'; ?>
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
                    <option value="administrator">Administrator</option>
                    <option value="sprzedawca">Sprzedawca</option>
                    <option value="magazynier">Magazynier</option>
                </select>
                
                <button type="submit" class="form-button">Zarejestruj się</button>
            </form>
        </div>
    </div>
</body>
</html>
<?php
unset($_SESSION['error']);
?>
