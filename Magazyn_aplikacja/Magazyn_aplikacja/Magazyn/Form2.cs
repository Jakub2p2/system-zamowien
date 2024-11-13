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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Magazyn
{
    public partial class add_user : Form
    {
        public string connect_string = "";
        private void Connect_db() // Polaczenie z baza danych
        {
            string strConnDtb = "Server=pg-26a19d25-paczkimagazyn.h.aivencloud.com; port=13890; user id=avnadmin; password=AVNS_3UbLex9BxU_ZYRZvxaY; database=paczuszki; ";
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
            var login_form = new Form_Login();
            login_form.ShowDialog();
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
                string name = textBox1.Text;
                string surname = textBox2.Text;
                string login = textBox3.Text;
                string email = textBox4.Text;
                string role = role_select.Text;
                string password = password_text.Text;
                string confirm_password = confirm_pass_text.Text;
                Connect_db();
                if (password.Equals(confirm_password)) // jeśli hasła są takie same
                {
                    using (NpgsqlConnection connection = new NpgsqlConnection(connect_string))
                    {
                        connection.Open();

                        string get_id_query = "SELECT COUNT(*) FROM uzytkownicy;";
                        int id_rows = 0;
                        using (NpgsqlCommand command = new NpgsqlCommand(get_id_query, connection)) // liczenie ilości wierszy w tabeli
                        {
                            object result = command.ExecuteScalar();
                            id_rows = Convert.ToInt32(result);
                            id_rows++;
                        }
                        string query = "INSERT INTO uzytkownicy(id, imie, nazwisko, login, haslo, email, ranga) VALUES("
                            + id_rows + ",'" + name + "','" + surname + "','" + login + "','" + password + "','" + email + "','" + role + "')";
                        using (NpgsqlCommand command = new NpgsqlCommand(query, connection)) // wysyłanie danych
                        {
                            try
                            {
                                command.Parameters.AddWithValue("id", id_rows);
                                command.Parameters.AddWithValue("imie", name);
                                command.Parameters.AddWithValue("nazwisko", surname);
                                command.Parameters.AddWithValue("login", login);
                                command.Parameters.AddWithValue("haslo", password);
                                command.Parameters.AddWithValue("email", email);
                                command.Parameters.AddWithValue("ranga", role);
                                int rowsAffected = command.ExecuteNonQuery(); // wysłanie danych
                                MessageBox.Show("Dodano użytkownika!!!");
                                this.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Hasła nie są takie same!");
                }
            }
        }

        private void add_user_Load(object sender, EventArgs e)
        {

        }

        private void add_user_Load_1(object sender, EventArgs e)
        {

        }

        private void role_select_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
