using Npgsql;
using System.Configuration;
using System.Data;
namespace Magazyn{
    public partial class Form1 : Form
    {
        string strConnDtb = "Server=pg-26a19d25-paczkimagazyn.h.aivencloud.com; port=13890; user id=avnadmin; password=AVNS_3UbLex9BxU_ZYRZvxaY; database=paczuszki;";
        NpgsqlConnection vCon;
        NpgsqlCommand vCmd;
        string table = "";
        public string connect_string = "";

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
            { // w razie braku po��czenia z baz�
                MessageBox.Show("B��d po��czenia z baz� danych", "B��d", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            return dt;
        }
        public Form1()
        {
            InitializeComponent();
        }
        public void show_table() // funkcja pokazujaca tabel�
        {
            try
            {
                DataTable dtgetdata = new DataTable();
                dtgetdata = getData("SELECT * FROM " + table + ";");
                
                tabela.DataSource = dtgetdata;
            }
            catch (Exception ex)
            {
                MessageBox.Show("B�ad po��czenia z tabel�: " + ex.Message, "B��d", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursor.Current;
        }
        private void create_btn(string tab) // dodawanie przyciskow edytuj i usu�
        {
            foreach (Control control in Controls) if (control is Button && control.Name == "edit_btn") Controls.Remove(control);// usuwanie przyciskow "edytuj"
            foreach (Control control in Controls) if (control is Button && control.Name == "delete_btn") Controls.Remove(control);// usuwanie przyciskow "usu�"
            using (NpgsqlConnection connection = new NpgsqlConnection(connect_string))
            {
                connection.Open();
                int id_rows = 0;
                string get_id_query = "SELECT COUNT(*) FROM " + table + ";";
                using (NpgsqlCommand command = new NpgsqlCommand(get_id_query, connection)) // liczenie ilo�ci wierszy w tabeli
                {
                    object result = command.ExecuteScalar();
                    id_rows = Convert.ToInt32(result);
                    id_rows++;
                }
                Button[] edit_arr = new Button[id_rows];
                Button[] delete_arr = new Button[id_rows];
                int height = 200;
                for (int i = 0; i < id_rows - 1; i++)
                {
                    height += 25;

                    edit_arr[i] = new Button(); // tworzenie przycisk�w edit
                    edit_arr[i].Height = 23;
                    edit_arr[i].Width = 75;
                    edit_arr[i].Name = "edit_btn";
                    edit_arr[i].Location = new Point(968, height);
                    edit_arr[i].Text = "edytuj";
                    edit_arr[i].Click += EditButtonClick;
                    this.Controls.Add(edit_arr[i]);

                    delete_arr[i] = new Button(); // tworzenie przycisk�w usu�
                    delete_arr[i].Height = 23;
                    delete_arr[i].Width = 75;
                    delete_arr[i].Name = "delete_btn";
                    delete_arr[i].Location = new Point(1050, height);
                    delete_arr[i].Text = "usu�";
                    delete_arr[i].Click += DeleteButtonClick;
                    this.Controls.Add(delete_arr[i]);
                    connection.Close();
                }
            }
        }
        private void EditButtonClick(object sender, EventArgs e) // zdzarzenie edit
        {
            MessageBox.Show("edit");
        }
        private void DeleteButtonClick(object sender, EventArgs e) // zdarzenie usu�
        {
            MessageBox.Show("usu�");
        }
        private void Form1_Load(object sender, EventArgs e) // przy za�adowaniu aplikacji
        {
            Connect_db();
        }
        /// ZDARZENIA PRZYCISK�W
        private void button_users_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            table = "uzytkownicy";
            add_btn.Text = "Dodaj u�ytkownika";
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
            add_btn.Text = "Dodaj spos�b dostawy";
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
            add_btn.Text = "Dodaj now� paczk�";
            show_table();
            create_btn(table);
            search_btn.Enabled = true;
            clear_btn.Enabled = true;
            search_btn.Visible = true;
            clear_btn.Visible = true;
            filtruj_tabele();
        }
        private void add_btn_Click(object sender, EventArgs e) // otwiera formularz zale�nie od wyboru
        {
            switch (table)
            {
                case "uzytkownicy":
                    var add_usr_form = new add_user();
                    add_usr_form.Show();
                    break;
                case "klienci":
                    var add_client_form = new add_client();
                    add_client_form.Show();
                    break;
                case "produkty":
                    var addproduct_form = new product_form();
                    addproduct_form.Show();
                    break;
                case "dostawy":
                    var addelivery_form = new delivery_form();
                    addelivery_form.Show();
                    break;
                case "paczki":
                    var addpackage_form = new package_form();
                    addpackage_form.Show();
                    break;
            }
        }
        private void filtruj_tabele()
        {
            czysc_pola_filtr();
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
                    string[] combobox_role = {"Brak", "Administrator", "Sprzedawca", "Magazynier"};
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

                    filtr_lbl1.Text = "Nazwa/Imi� i nazwisko:";
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
                    string[] combobox_status = { "--Wszystkie--", "Nowa", "Towar zam�wiony", "Kompletacja paczki", "Towar przygotowany od wysy�ki",
                    "Oczekiwanie na kuriera", "Towar odebrany przez kuriera", "Wstrzymany"};
                    comboBox_txt.Items.AddRange(combobox_status);
                    break;
            }
        }
        private void search_btn_Click(object sender, EventArgs e) //funkcja wyszukujaca rekordy w tabelach (nie dziala)
        {
            switch (table)
            {
                case "produkty":
                    try
                    {
                        DataTable dtgetdata = new DataTable();
                        dtgetdata = getData("SELECT * FROM" + table + " WHERE nazwa = '" + filtr_txt1.Text + "' OR cechy= '" + filtr_txt2.Text + "' ;");
                        tabela.DataSource = dtgetdata;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("B�ad po��czenia z tabel�: " + ex.Message, "B��d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    create_btn(table);
                    break;
                case "paczki":
                    try
                    {
                        DataTable dtgetdata = new DataTable();
                        dtgetdata = getData("SELECT * FROM" + table + " WHERE nr_listu = '" + filtr_txt1.Text + "' OR klient_id = '" + filtr_txt2.Text + "' OR status ='"
                            + comboBox_txt.Text +"' OR data_utworzenia ='" + date_utworzenia_txt.Text + "' ;");
                        tabela.DataSource = dtgetdata;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("B�ad po��czenia z tabel�: " + ex.Message, "B��d", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        MessageBox.Show("B�ad po��czenia z tabel�: " + ex.Message, "B��d", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        MessageBox.Show("B�ad po��czenia z tabel�: " + ex.Message, "B��d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    create_btn(table);
                    break;
            }
        }

        private void clear_btn_Click(object sender, EventArgs e)
        {
            filtr_txt1.Text = "";
            filtr_txt2.Text = "";
        }
        private void czysc_pola_filtr()
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
    }
}
