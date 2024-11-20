using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Magazyn
{
    public partial class add_client : Form
    {
        public string connect_string = "";
        string strConnDtb = "Server=pg-26a19d25-paczkimagazyn.h.aivencloud.com; port=13890; user id=avnadmin; password=AVNS_3UbLex9BxU_ZYRZvxaY; database=paczuszki; ";
        NpgsqlConnection vCon;
        NpgsqlCommand vCmd;
        string table = "";
        public string addORedit = "";
        public int edit_id;
        private Form1 main_form;
        private void Connect_db() // Polaczenie z baza danych
        {
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
        public add_client(bool edit, string[] datas, int p_id, Form1 main_form)
        {
            InitializeComponent();
            if (edit)
            {
                name_txt.Text = datas[0];
                nip_txt.Text = datas[1];
                region_txt.Text = datas[2];
                pesel_txt.Text = datas[3];
                email_txt.Text = datas[4];
                tel_txt.Text = datas[5];
                adres_txt.Text = datas[6];
                edit_id = p_id;
                addORedit = "edit";
                this.Text = "Edytuj Klienta";
            }
            else
            {
                addORedit = "add";
                this.Text = "Dodaj Klienta";
            }

            this.main_form = main_form;

        }
        public bool check_Empty() // sprawdzianie czy pola sa puste (nie dziala ze spacja)
        {
            if (email_txt.Text == " ")
                return true;
            else return false;
        }
        private void close_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string email = email_txt.Text;
            string nip = nip_txt.Text;
            string region = region_txt.Text;
            string pesel = pesel_txt.Text;
            if (check_Empty()) MessageBox.Show("Nie wszystkie pola sa wypelnione");
            else if (!corrEmail(email)) MessageBox.Show("Podaj prawidlowy email");
            else if (!corrNip(nip)) MessageBox.Show("Bledny kod NIP");
            else if (!corrRegon(region)) MessageBox.Show("Bledny REGON");
            else if (!corrPesel(pesel)) MessageBox.Show("Bledny Pesel");
            else // jesli wszystkie pola sa zapelnione i wprowadzone dane sa poprawne
            {
                string name = name_txt.Text;
                string tel = tel_txt.Text;
                string adres = adres_txt.Text;
                Connect_db();
                using (NpgsqlConnection connection = new NpgsqlConnection(connect_string))
                {
                    connection.Open();
                    string get_id_query = "SELECT MAX(id) FROM klienci;";
                    int id_rows = 0;
                    using (NpgsqlCommand command = new NpgsqlCommand(get_id_query, connection)) // liczenie ilości wierszy w tabeli
                    {
                        object result = command.ExecuteScalar();
                        id_rows = Convert.ToInt32(result);
                        id_rows++;
                    }
                    string query = "";
                    if (addORedit == "add")
                    {
                        query = "INSERT INTO klienci(id, nazwa, nip, region, pesel, email, telefon, adres) VALUES(" + id_rows + ",'" + name + "','" + nip
                        + "','" + region + "','" + pesel + "','" + email + "','" + tel + "','" + adres + "')";
                    }
                    else if (addORedit == "edit")
                    {
                        query = "UPDATE klienci SET nazwa = @nazwa, nip = @nip, region = @region, pesel = @pesel, email = @email, " +
                            "telefon = @telefon, adres = @adres WHERE id = @id";
                    }
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection)) // wysyłanie danych
                    {
                        if (addORedit == "add") command.Parameters.AddWithValue("id", id_rows);
                        if (addORedit == "edit") command.Parameters.AddWithValue("@id", edit_id);
                        command.Parameters.AddWithValue("@nazwa", name);
                        command.Parameters.AddWithValue("@nip", nip);
                        command.Parameters.AddWithValue("@region", region);
                        command.Parameters.AddWithValue("@pesel", pesel);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@telefon", tel);
                        command.Parameters.AddWithValue("@adres", adres);
                        int rowsAffected = command.ExecuteNonQuery(); // wysłanie danych (nie działa)
                        if (addORedit == "add") MessageBox.Show("Dodano nowego klienta!!!");
                        if (addORedit == "edit") MessageBox.Show("Zmieniono klienta!!!");

                        this.Close();
                        main_form.show_table();
                        main_form.create_btn("klienci");
                    }
                }
            }
        }
        public static bool corrEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailRegex);
        }
        public static bool corrNip(string nip)
        {
            if (nip.Length != 10 || !Regex.IsMatch(nip, @"^\d{10}$"))
                return false;

            int[] weights = { 6, 5, 7, 2, 3, 4, 5, 6, 7 };
            int checksum = 0;

            for (int i = 0; i < 9; i++)
            {
                checksum += weights[i] * (nip[i] - '0');
            }

            int controlDigit = checksum % 11;
            return controlDigit == (nip[9] - '0');
        }
        public static bool corrRegon(string region)
        {
            if (!(region.Length == 9 || region.Length == 14) || !Regex.IsMatch(region, @"^\d+$"))
                return false;

            int[] weights9 = { 8, 9, 2, 3, 4, 5, 6, 7 };
            int[] weights14 = { 2, 4, 8, 5, 0, 9, 7, 3, 6, 1, 2, 4, 8 };

            int[] weights = region.Length == 9 ? weights9 : weights14;
            int checksum = 0;

            for (int i = 0; i < weights.Length; i++)
            {
                checksum += weights[i] * (region[i] - '0');
            }

            int controlDigit = checksum % 11;
            if (controlDigit == 10)
                controlDigit = 0;

            return controlDigit == (region[^1] - '0');
        }
        public static bool corrPesel(string pesel)
        {
            if (pesel.Length != 11 || !Regex.IsMatch(pesel, @"^\d{11}$"))
                return false;

            int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
            int checksum = 0;

            for (int i = 0; i < 10; i++)
            {
                checksum += weights[i] * (pesel[i] - '0');
            }

            int controlDigit = 10 - (checksum % 10);
            if (controlDigit == 10)
                controlDigit = 0;

            return controlDigit == (pesel[10] - '0');
        }
        private void add_client_Load(object sender, EventArgs e)
        {

        }
    }
}
