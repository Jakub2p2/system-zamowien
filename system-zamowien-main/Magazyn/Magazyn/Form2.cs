using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Magazyn
{
    public partial class add_user : Form
    {
        public string connect_string = "";
        private void Connect_db() // Polaczenie z baza danych
        {
            string strConnDtb = "Server=185.157.80.106; port=5432; user id=postgres; password=123; database=Uzytkownicy; ";
            NpgsqlConnection vCon;
            NpgsqlCommand vCmd;
            try
            {
                vCon = new NpgsqlConnection();
                vCon.ConnectionString = strConnDtb;
                connect_string = vCon.ConnectionString;
                if (vCon.State == System.Data.ConnectionState.Closed) vCon.Open();
            }
            catch
            { // w razie braku połączenia z bazą
                MessageBox.Show("Błąd połączenia z bazą danych", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
        public add_user()
        {
            InitializeComponent();
        }

        private void password_text_TextChanged(object sender, EventArgs e) // zakrywanie wyrazu
        {
            password_text.PasswordChar = '*';
        }

        private void confirm_pass_text_TextChanged(object sender, EventArgs e) // zakrywanie wyrazu
        {
            confirm_pass_text.PasswordChar = '*';
        }

        private void close_window_Click(object sender, EventArgs e) // zamykanie okna
        {
            this.Close();
        }
        public bool check_Empty() // sprawdzianie czy pola sa puste (nie dziala ze spacja)
        {
            if (textBox1.TextLength == 0 || textBox2.TextLength == 0 || textBox3.TextLength == 0 || textBox4.TextLength == 0 || role_select.Text == null || password_text.TextLength == 0 || confirm_pass_text.TextLength == 0)
            return true;
            else return false;
        }
        private void save_button_Click(object sender, EventArgs e)
        {
            if (check_Empty()) MessageBox.Show("Nie wszystkie pola są uzupełnione");
            else // jesli wszystkie pola sa zapelnione
            {
                //string name = textBox1.Text;
                //string surname = textBox2.Text;
                string login = textBox3.Text;
                //string email = textBox4.Text;
                string role = role_select.Text;
                string password = password_text.Text;
                string confirm_password = confirm_pass_text.Text;
                Connect_db();
                if (password.Equals(confirm_password)) // jeśli hasła są takie same
                {
                    // WYSYŁANIE KWERENDY NIE DZIAŁA 
                    string query = "INSERT INTO uzytkownicy(username, password, ranga) VALUES('" + login + "','" + password + "','" + role + "')";
                    using var dataSource = NpgsqlDataSource.Create(connect_string);
                    // Create a command
                    using var cmd = dataSource.CreateCommand(query);
                    // Bind parameters
                    cmd.Parameters.AddWithValue("username", login);
                    cmd.Parameters.AddWithValue("password", password);
                    cmd.Parameters.AddWithValue("ranga", role);
                    cmd.ExecuteNonQueryAsync();

                    MessageBox.Show(query);

                    //https://neon.tech/postgresql/postgresql-csharp/postgresql-csharp-insert stąd wziąłem kod jak chcesz to poszperaj
                }
                else
                {
                    MessageBox.Show("Hasła nie są takie same!");
                }
            }
        }
    }
}
