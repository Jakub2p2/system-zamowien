using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Magazyn
{
    public partial class product_form : Form
    {
        public string connect_string = "";
        string strConnDtb = "Server=pg-26a19d25-paczkimagazyn.h.aivencloud.com; port=13890; user id=avnadmin; password=AVNS_3UbLex9BxU_ZYRZvxaY; database=paczuszki; ";
        NpgsqlConnection vCon;
        NpgsqlCommand vCmd;
        public string addORedit = "";
        string table = "";
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
        public product_form(bool edit, string[] datas, int p_id)
        {
            InitializeComponent();
            if (edit)
            {
                name_txt.Text = datas[0];
                cecha_txt.Text = datas[1];
                price_txt.Text = datas[2];
                weight_txt.Text = datas[3];
                count_txt.Text = datas[4];
                addORedit = "edit";
                this.Text = "Edytuj Produkt";
            }
            else
            {
                addORedit = "add";
                this.Text = "Dodaj Produkt";
            }
        }
        public bool check_Empty() // sprawdzianie czy pola sa puste (nie dziala ze spacja)
        {
            if (name_txt.TextLength == 0 || cecha_txt.TextLength == 0 || weight_txt.TextLength == 0 || count_txt == null || price_txt.TextLength == 0)
                return true;
            else return false;
        }
        private void product_form_Load(object sender, EventArgs e)
        {
        }

        private void quit_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void add_btn_Click(object sender, EventArgs e)
        {
            if (check_Empty()) MessageBox.Show("Nie wszystkie pola są uzupełnione");
            else // jesli wszystkie pola sa zapelnione
            {
                string name = name_txt.Text;
                string cecha = cecha_txt.Text;
                int price = Convert.ToInt32(price_txt.Text);
                int weight = Convert.ToInt32(weight_txt.Text);
                int count = Convert.ToInt32(count_txt.Value);
                Connect_db();
                using (NpgsqlConnection connection = new NpgsqlConnection(connect_string))
                {
                    connection.Open();
                    string query = "INSERT INTO produkty(nazwa, cechy, cena, waga, ilosc) VALUES('" + name + "','" + cecha
                        + "'," + price + "," + weight + ","+count+")";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection)) // wysyłanie danych
                    {
                        command.Parameters.AddWithValue("nazwa", name);
                        command.Parameters.AddWithValue("cechy", cecha);
                        command.Parameters.AddWithValue("cena", price);
                        command.Parameters.AddWithValue("waga", weight);
                        command.Parameters.AddWithValue("ilosc", count);
                        int rowsAffected = command.ExecuteNonQuery(); // wysłanie danych (nie działa)
                        MessageBox.Show("Dodano nowy produkt!!!");
                        this.Close();
                    }
                }
            }
        }
    }
}
