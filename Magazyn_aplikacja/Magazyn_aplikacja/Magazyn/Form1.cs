using Microsoft.VisualBasic.ApplicationServices;
using Npgsql;
using System.Configuration;
using System.Data;
using System.Diagnostics.Metrics;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace Magazyn{
    public partial class Form1 : Form
    {
        string strConnDtb = "Server=pg-26a19d25-paczkimagazyn.h.aivencloud.com; port=13890; user id=avnadmin; password=AVNS_3UbLex9BxU_ZYRZvxaY; database=paczuszki;";
        NpgsqlConnection vCon;
        NpgsqlCommand vCmd;
        string table = "";
        public string connect_string = "";
        string uzyt = "";
        string password = "";

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
        public DataTable getData(string sql) // pobieranie danych z bazy
        {
            DataTable dt = new DataTable();
            Connect_db();
            vCmd = new NpgsqlCommand();
            vCmd.Connection = vCon;
            vCmd.CommandText = sql;
            NpgsqlDataReader dr = vCmd.ExecuteReader();
            dt.Load(dr);
            vCon.Close();
            return dt;
        }
        public Form1(string uzytkownik, string pass)
        {
            InitializeComponent();
            uzyt = uzytkownik;
            password = pass;
        }
        public void show_table() // funkcja pokazujaca tabelę
        {
            try
            {
                DataTable dtgetdata = new DataTable();
                switch (table)
                {
                    case "uzytkownicy":
                        dtgetdata = getData("SELECT id, imie, nazwisko, login, email, ranga" + "  FROM " + table + " ORDER BY id;");
                        break;
                    case "klienci":
                        dtgetdata = getData("SELECT id, nazwa, nip, region, adres, email, telefon, pesel  FROM " + table + " ORDER BY id;");
                        break;
                    case "produkty":
                        dtgetdata = getData("SELECT id, nazwa, cechy, cena, waga, ilosc FROM " + table + " ORDER BY id;");
                        break;
                    case "dostawy":
                        dtgetdata = getData("SELECT id, nazwa, cena_za_kg, cena_ubezpieczenia, link_do_śledzenia FROM " + table + " ORDER BY id;");
                        break;
                    case "paczki":
                        dtgetdata = getData("SELECT id, status, data_utworzenia, data_odbioru, data_dostarczenia, ubezpieczenie, koszt_transportu, nr_listu, wartosc, klient_id FROM " + table + " ORDER BY id;");
                        break;
                }
                tabela.DataSource = dtgetdata;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bład połączenia z tabelą: " + ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursor.Current;
        }
        private void create_btn(string tab) // dodawanie przyciskow edytuj i usuń
        {
            int j = 0;
            Control[] drop_btns = new Control[100]; // wartość 100, którą trzeba zmienić
            foreach (Control control in Controls) // przypisywanie przycisków "edytuj" oraz "usuń" do tablicy
            {
                if (control is System.Windows.Forms.Button && (control.Name.StartsWith("edit_btn") || control.Name.StartsWith("delete_btn")))
                {
                    drop_btns[j] = control;
                    j++;
                }
            }
            for (int i = 0; i < j; i++) Controls.Remove(drop_btns[i]); // usuwanie przycisków "edytuj" i "usuń"

            using (NpgsqlConnection connection = new NpgsqlConnection(connect_string))
            {
                connection.Open();
                int id_rows = 0;
                string get_id_query = "SELECT COUNT(*) FROM " + tab + ";";
                using (NpgsqlCommand command = new NpgsqlCommand(get_id_query, connection))
                {
                    object result = command.ExecuteScalar();
                    id_rows = Convert.ToInt32(result);
                    id_rows++;
                }

                System.Windows.Forms.Button[] edit_arr = new System.Windows.Forms.Button[id_rows];
                System.Windows.Forms.Button[] delete_arr = new System.Windows.Forms.Button[id_rows];

                string get_ids_query = $"SELECT id FROM {table} ORDER BY id ASC;";
                using (NpgsqlCommand command = new NpgsqlCommand(get_ids_query, connection))
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    int height = 225;
                    int i = 0;
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);

                        edit_arr[i] = new System.Windows.Forms.Button();
                        edit_arr[i].Height = 23;
                        edit_arr[i].Width = 75;
                        edit_arr[i].Name = "edit_btn" + id.ToString();
                        edit_arr[i].Location = new Point(968, height);
                        edit_arr[i].Text = "Edytuj " + id;
                        edit_arr[i].Click += EditButtonClick;
                        this.Controls.Add(edit_arr[i]);

                        delete_arr[i] = new System.Windows.Forms.Button();
                        delete_arr[i].Height = 23;
                        delete_arr[i].Width = 75;
                        delete_arr[i].Name = "delete_btn" + id.ToString();
                        delete_arr[i].Location = new Point(1050, height);
                        delete_arr[i].Text = "Usuń " + id;
                        delete_arr[i].Click += DeleteButtonClick;
                        this.Controls.Add(delete_arr[i]);

                        i++;
                        height += 25;
                    }
                }
            }
        }
        private void EditButtonClick(object sender, EventArgs e) // zdzarzenie edit
        {
            System.Windows.Forms.Button clickedButton = (System.Windows.Forms.Button)sender;
            string buttonName = clickedButton.Name;
            //MessageBox.Show("edit " + buttonName);
            buttonName = buttonName.Replace("edit_btn", "");
            string query = "";
            switch (table)
            {
                case "uzytkownicy":
                    query = $"SELECT imie, nazwisko, login, email, ranga, haslo FROM uzytkownicy WHERE id = {buttonName};";
                    break;
                case "dostawy":
                    query = $"SELECT nazwa, cena_za_kg, cena_ubezpieczenia, link_do_śledzenia FROM {table} WHERE id = {buttonName};";
                    break;
                case "produkty":
                    query = $"SELECT nazwa, cechy, cena, waga, ilosc  FROM {table} WHERE id = {buttonName};";
                    break;
                case "klienci":
                    query = $"SELECT imie, nip, region, pesel, email, telefon, adres FROM {table} WHERE id = {buttonName};";
                    break;
                case "paczki":
                    query = $"SELECT nazwa, cena_za_kg, cena_ubezpieczenia, link_do_śledzenia FROM {table} WHERE id = {buttonName};"; //do poprawienia
                    break;
            }

            string[] userData;
            using (NpgsqlConnection connection = new NpgsqlConnection(connect_string))
            {
                try
                {
                    connection.Open();
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                switch (table)
                                {
                                    case "uzytkownicy":
                                        string imie = reader.GetString(0);
                                        string nazwisko = reader.GetString(1);
                                        string login = reader.GetString(2);
                                        string email = reader.GetString(3);
                                        string role = reader.GetString(4);
                                        string password = reader.GetString(5);
                                        userData = new string[6];
                                        userData[0] = imie;
                                        userData[1] = nazwisko;
                                        userData[2] = login;
                                        userData[3] = email;
                                        userData[4] = role;
                                        userData[5] = password;
                                        add_CreateEdit(true, Convert.ToInt32(buttonName), userData);
                                        break;
                                    case "dostawy":
                                        string name = reader.GetString(0);
                                        double cena_kg = reader.GetDouble(1);
                                        int cena_ubz = reader.GetInt32(2);
                                        string link = reader.GetString(3);

                                        userData = new string[4];
                                        userData[0] = name;
                                        userData[1] = cena_ubz.ToString();
                                        userData[2] = link;
                                        userData[3] = cena_kg.ToString();
                                        add_CreateEdit(true, Convert.ToInt32(buttonName), userData);
                                        break;
                                    case "produkty":
                                        string nazwa = reader.GetString(0);
                                        string cechy = reader.GetString(1);
                                        double cena = reader.GetDouble(2);
                                        double waga = reader.GetDouble(3);
                                        int ilosc = reader.GetInt32(4);

                                        userData = new string[5];
                                        userData[0] = nazwa.ToString();
                                        userData[1] = cechy.ToString();
                                        userData[2] = waga.ToString();
                                        userData[3] = ilosc.ToString();
                                        userData[4] = cena.ToString();
                                        add_CreateEdit(true, Convert.ToInt32(buttonName), userData);
                                        break;
                                    case "klienci":
                                        string im = reader.GetString(0);
                                        string nip = reader.GetString(1);
                                        string region = reader.GetString(2);
                                        string pesel = reader.GetString(3);
                                        string emil = reader.GetString(4);
                                        string telefon = reader.GetString(5);
                                        string adres = reader.GetString(6);

                                        userData = new string[7];
                                        userData[0] = im.ToString();
                                        userData[1] = nip.ToString();
                                        userData[2] = region.ToString();
                                        userData[3] = pesel.ToString();
                                        userData[4] = emil.ToString();
                                        userData[5] = telefon.ToString();
                                        userData[6] = adres.ToString();
                                        add_CreateEdit(true, Convert.ToInt32(buttonName), userData);
                                        break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Brak użytkownika o podanym ID.");
                            }
                        }
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }
        private void DeleteButtonClick(object sender, EventArgs e) // zdarzenie usuń
        {
            System.Windows.Forms.Button clickedButton = (System.Windows.Forms.Button)sender;
            string buttonName = clickedButton.Name;
            DialogResult dialogResult = MessageBox.Show("Czy napewno chcesz usunąć tą kolumnę?", "UWAGA!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connect_string))
                {
                    connection.Open();
                    buttonName = buttonName.Replace("delete_btn", "");
                    string delete_query = $"DELETE FROM {table} WHERE id = {buttonName};";
                    using (NpgsqlCommand command = new NpgsqlCommand(delete_query, connection))
                    {
                        object result = command.ExecuteScalar();
                    }
                    connection.Close();
                    show_table();
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e) // przy załadowaniu aplikacji
        {
            Connect_db();
        }
        /// ZDARZENIA PRZYCISKÓW
        private void button_users_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            table = "uzytkownicy";
            add_btn.Text = "Dodaj użytkownika";
            show_table();
            create_btn(table);
            search_btn.Enabled = true;
            clear_btn.Enabled = true;
            search_btn.Visible = true;
            clear_btn.Visible = true;
            filtruj_tabele();
        }
        private void button_client_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            table = "klienci";
            add_btn.Text = "Dodaj Klienta";
            show_table();
            create_btn(table);
            search_btn.Enabled = true;
            clear_btn.Enabled = true;
            search_btn.Visible = true;
            clear_btn.Visible = true;
            filtruj_tabele();
        }
        private void button_products_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            table = "produkty";
            add_btn.Text = "Dodaj nowy produkt";
            show_table();
            create_btn(table);
            search_btn.Enabled = true;
            clear_btn.Enabled = true;
            search_btn.Visible = true;
            clear_btn.Visible = true;
            filtruj_tabele();
        }
        private void button_delivery_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            table = "dostawy";
            add_btn.Text = "Dodaj sposób dostawy";
            show_table();
            create_btn(table);
            search_btn.Enabled = false;
            clear_btn.Enabled = false;
            search_btn.Visible = false;
            clear_btn.Visible = false;
            filtruj_tabele();
        }
        private void button_package_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            table = "paczki";
            add_btn.Text = "Dodaj nową paczkę";
            show_table();
            create_btn(table);
            search_btn.Enabled = true;
            clear_btn.Enabled = true;
            search_btn.Visible = true;
            clear_btn.Visible = true;
            filtruj_tabele();
        }
        private void add_CreateEdit(bool edit, int id, string[] datas)
        {
            switch (table)
            {
                case "uzytkownicy":
                    var add_usr_form = new add_user(edit, datas, id);
                    add_usr_form.Show();
                    break;
                case "klienci":
                    var add_client_form = new add_client(edit, datas, id);
                    add_client_form.Show();
                    break;
                case "produkty":
                    var addproduct_form = new product_form(edit, datas, id);
                    addproduct_form.Show();
                    break;
                case "dostawy":
                    var addelivery_form = new delivery_form(edit, datas, id);
                    addelivery_form.Show();
                    break;
                case "paczki":
                    var addpackage_form = new package_form();
                    addpackage_form.Show();
                    break;
            }
        }
        private void add_btn_Click(object sender, EventArgs e) // otwiera formularz zależnie od wyboru
        {
            add_CreateEdit(false, 0, []);
        }
        private void filtruj_tabele()
        {
            czysc_pola_filtr(true);
            switch (table)
            {
                case "dostawy":
                    break;
                case "produkty":
                    filtr_lbl1.Visible = true;
                    filtr_lbl2.Visible = true;
                    filtr_txt1.Visible = true;
                    filtr_txt2.Visible = true;
                    filtr_lbl1.Text = "Nazwa:";
                    filtr_lbl2.Text = "Cechy:";
                    break;
                case "uzytkownicy":
                    filtr_lbl1.Visible = true;
                    filtr_lbl2.Visible = true;
                    filtr_lbl3.Visible = true;
                    filtr_lbl4.Visible = true;
                    filtr_lbl8.Visible = true;

                    filtr_txt1.Visible = true;
                    filtr_txt2.Visible = true;
                    filtr_txt3.Visible = true;
                    filtr_txt4.Visible = true;
                    comboBox_txt.Visible = true;

                    filtr_lbl1.Text = "Nazwa:";
                    filtr_lbl2.Text = "Nazwisko:";
                    filtr_lbl3.Text = "Email";
                    filtr_lbl4.Text = "Login";
                    filtr_lbl8.Text = "Rola";
                    string[] combobox_role = { "Brak", "Administrator", "Sprzedawca", "Magazynier" };
                    comboBox_txt.Items.AddRange(combobox_role);
                    break;
                case "klienci":
                    filtr_lbl1.Visible = true;
                    filtr_lbl2.Visible = true;
                    filtr_lbl3.Visible = true;
                    filtr_lbl4.Visible = true;
                    filtr_lbl5.Visible = true;
                    filtr_lbl6.Visible = true;
                    filtr_lbl7.Visible = true;

                    filtr_txt1.Visible = true;
                    filtr_txt2.Visible = true;
                    filtr_txt3.Visible = true;
                    filtr_txt4.Visible = true;
                    filtr_txt5.Visible = true;
                    filtr_txt6.Visible = true;
                    filtr_txt7.Visible = true;

                    filtr_lbl1.Text = "Nazwa/Imię i nazwisko:";
                    filtr_lbl2.Text = "NIP:";
                    filtr_lbl3.Text = "REGION:";
                    filtr_lbl4.Text = "PESEL:";
                    filtr_lbl5.Text = "Email:";
                    filtr_lbl6.Text = "Telefon:";
                    filtr_lbl7.Text = "Adres:";
                    break;
                case "paczki":
                    filtr_lbl1.Visible = true;
                    filtr_lbl2.Visible = true;
                    filtr_lbl8.Visible = true;
                    filtr_lbl9.Visible = true;

                    filtr_txt1.Visible = true;
                    filtr_txt2.Visible = true;
                    comboBox_txt.Visible = true;
                    date_utworzenia_txt.Visible = true;

                    filtr_lbl1.Text = "Nr listu przewozowego:";
                    filtr_lbl2.Text = "Klient:";
                    filtr_lbl8.Text = "Status:";
                    filtr_lbl9.Text = "Data utworzenia:";
                    string[] combobox_status = { "--Wszystkie--", "Nowa", "Towar zamówiony", "Kompletacja paczki", "Towar przygotowany od wysyłki",
                    "Oczekiwanie na kuriera", "Towar odebrany przez kuriera", "Wstrzymany"};
                    comboBox_txt.Items.AddRange(combobox_status);
                    break;
            }
        }
        private void search_btn_Click(object sender, EventArgs e)
        {
            switch (table)
            {
                case "produkty":
                    try
                    {
                        DataTable dtgetdata = new DataTable();
                        dtgetdata = getData("SELECT * FROM " + table + " WHERE nazwa = '" + filtr_txt1.Text + "' OR cechy= '" + filtr_txt2.Text + "' ;");
                        tabela.DataSource = dtgetdata;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Bład połączenia z tabelą: " + ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    create_btn(table);
                    break;
                case "paczki":
                    try
                    {
                        DataTable dtgetdata = new DataTable();
                        dtgetdata = getData("SELECT * FROM " + table + " WHERE nr_listu = '" + filtr_txt1.Text + "' OR klient_id = '" + filtr_txt2.Text + "' OR status ='"
                            + comboBox_txt.Text + "' OR data_utworzenia ='" + date_utworzenia_txt.Text + "' ;");
                        tabela.DataSource = dtgetdata;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Bład połączenia z tabelą: " + ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    create_btn(table);
                    break;
                case "klienci":
                    try
                    {
                        DataTable dtgetdata = new DataTable();
                        dtgetdata = getData("SELECT * FROM " + table + " WHERE nazwa = '" + filtr_txt1.Text + "' OR nip = '" + filtr_txt2.Text +
                        "' OR region = '" + filtr_txt3.Text + "' OR pesel = '" + filtr_txt4.Text + "' OR email = '" + filtr_txt5.Text + "' OR telefon = '" + filtr_txt6.Text + "' OR adres = '" + filtr_txt7.Text + "' ;");
                        tabela.DataSource = dtgetdata;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Bład połączenia z tabelą: " + ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    create_btn(table);
                    break;
                case "uzytkownicy":
                    try
                    {
                        DataTable dtgetdata = new DataTable();
                        dtgetdata = getData("SELECT * FROM " + table + " WHERE imie = '" + filtr_txt1.Text + "' OR nazwisko = '" + filtr_txt2.Text + "' OR email = '" + filtr_txt3.Text +
                            "' OR login = '" + filtr_txt4.Text + "' OR ranga = '" + comboBox_txt.Text + "' ;");
                        tabela.DataSource = dtgetdata;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Bład połączenia z tabelą: " + ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    create_btn(table);
                    break;
            }
        }

        private void clear_btn_Click(object sender, EventArgs e)
        {
            czysc_pola_filtr(false);
        }
        private void czysc_pola_filtr(bool clean_every)
        {
            if (clean_every)
            {
                filtr_lbl1.Visible = false;
                filtr_lbl2.Visible = false;
                filtr_lbl3.Visible = false;
                filtr_lbl4.Visible = false;
                filtr_lbl5.Visible = false;
                filtr_lbl6.Visible = false;
                filtr_lbl7.Visible = false;
                filtr_lbl8.Visible = false;
                filtr_lbl9.Visible = false;

                filtr_txt1.Visible = false;
                filtr_txt2.Visible = false;
                filtr_txt3.Visible = false;
                filtr_txt4.Visible = false;
                filtr_txt5.Visible = false;
                filtr_txt6.Visible = false;
                filtr_txt7.Visible = false;
                comboBox_txt.Visible = false;
                date_utworzenia_txt.Visible = false;
            }
            filtr_txt1.Text = "";
            filtr_txt2.Text = "";
            filtr_txt3.Text = "";
            filtr_txt4.Text = "";
            filtr_txt5.Text = "";
            filtr_txt6.Text = "";
            filtr_txt7.Text = "";
            comboBox_txt.Items.Clear();
            date_utworzenia_txt.Text = "";
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void zmieńHasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var change_pass = new Form8(uzyt, password);
            change_pass.Show();
        }
    }
}
