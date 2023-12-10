using MySql.Data.MySqlClient;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace BuildingMaterials
{
    public static class Program
    {


        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }

        public static void InitializeTables(DataGridView table, string name, SqlConnection con)
        {
            MySqlDataReader reader = null;
            table.Rows.Clear();
            table.Columns.Clear();
            DataGridViewComboBoxColumn comboboxStore = new DataGridViewComboBoxColumn();
            DataGridViewComboBoxColumn comboboxGood = new DataGridViewComboBoxColumn();
            DataGridViewComboBoxColumn comboboxOrder = new DataGridViewComboBoxColumn();
            DataGridViewComboBoxColumn comboboxWorker = new DataGridViewComboBoxColumn();
            if (name == "good")
            {
                reader = con.data($"SELECT good.good_name AS \"Наименование товара\",\r\ngood.brc_code AS \"Код КСР\",\r\ngood.Field_Of_Application AS \"Область применения\", \r\ngood.Packaging AS \"Упаковка\", \r\ngood.Technical_Characteristics AS \"Технические характеристики\",\r\ngood.Instructions_For_Use AS \"Инструкция по применению\", \r\ngood.Precautions AS \"Предостережения\", \r\ngood.Storage_And_Transportation AS \"Хранение и транспортировка\",\r\ngood.Certificates AS \"Сертификаты\", \r\ngood.Manufacturer_information AS \"Сведения о производителе\"\r\nFROM good;");
                
            }
            else if (name == "store")
            {
                reader = con.data($"SELECT store.store_name AS \"Название магазина\",\r\nstore.Legal_Address AS \"Юридический адрес\",\r\nstore.real_address AS \"Физический адрес\",\r\nstore.Director_Second_Name AS \"Фамилия директора\",\r\nstore.Director_First_Name AS \"Имя директора\",\r\nstore.Director_Father_Name AS \"Отчество директора\",\r\nstore.Phone_Number AS \"Номер телефона\"\r\nFROM store");
                
            }
            else if (name == "worker")
            {
                comboboxStore = GetComboBox("store", con);
                reader = con.data($"SELECT worker.Worker_Second_Name AS \"Фамилия\", \r\nworker.Worker_First_Name AS \"Имя\", \r\nworker.Worker_Father_Name AS \"Отчество\", \r\nworker.Worker_Post AS \"Должность\",\r\nworker.Phone_Number AS \"Номер телефона\",\r\nstore.real_address AS \"Физический адрес магазина\"\r\nfrom worker, worker_store, store\r\nWHERE worker.worker_id = worker_store.worker_id\r\nAND worker_store.store_id = store.store_id;");
                
            }
            else if (name == "good_selled")
            {
                comboboxGood = GetComboBox("good", con);
                comboboxStore = GetComboBox("store", con);
                comboboxOrder = GetComboBox("order_t", con);
                reader = con.data("SELECT good_selled.order_id AS \"Номер заказа\", good.good_name AS \"Наименование товара\", \r\nstore.real_address AS \"Физический адрес магазина\", \r\ngood_selled.good_count AS \"Количество\", \r\ngood_selled.good_cost AS \"Стоимость\", \r\norder_t.order_date AS \"Дата и время\"\r\nFROM good, store, good_selled, order_t\r\nWHERE good.good_id = good_selled.good_id\r\nAND store.store_id = good_selled.store_id\r\nAND order_t.order_id = good_selled.order_id\r\nORDER BY order_t.order_date;");
                
            }
            else if (name == "good_available")
            {
                comboboxGood = GetComboBox("good", con);
                comboboxStore = GetComboBox("store", con);
                reader = con.data("SELECT good.good_name AS \"Наименование товара\",  \r\nstore.real_address AS \"Физический адрес магазина\", \r\ngood_available.good_count AS \"Количество\", \r\ngood_available.good_cost AS \"Стоимость\"\r\nFROM good, store, good_available\r\nWHERE good.good_id = good_available.good_id\r\nAND store.store_id = good_available.store_id\r\nORDER BY good_available.good_id;");
                
            }
            else if (name == "order_t")
            {
                comboboxWorker = GetComboBox("worker", con);
                reader = con.data("SELECT order_t.order_id AS \"Номер заказа\",\r\norder_t.worker_id AS \"Номер сотрудника\",\r\norder_t.Customer_Second_Name AS \"Фамилия покупателя\",\r\norder_t.Customer_First_Name AS \"Имя покупателя\",\r\norder_t.Customer_Father_Name AS \"Отчество покупателя\",\r\norder_t.Customer_Phone_Number AS \"Номер телефона покупателя\",\r\norder_t.order_date AS \"Дата заказа\"\r\nFROM order_t;");
                
            }
            if (reader != null)
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    table.Columns.Add(columnName: reader.GetName(i), headerText: reader.GetName(i));
                }
                if (name == "worker")
                {
                    table.Columns.Remove("Физический адрес магазина");
                    table.Columns.Add(comboboxStore);
                }
                if (name == "good_available")
                {
                    table.Columns.Remove("Наименование товара");
                    table.Columns.Insert(0, comboboxGood);
                    table.Columns.Remove("Физический адрес магазина");
                    table.Columns.Insert(1, comboboxStore);
                }
                if (name == "good_selled")
                {
                    table.Columns.Remove("Наименование товара");
                    table.Columns.Insert(1, comboboxGood);
                    table.Columns.Remove("Физический адрес магазина");
                    table.Columns.Insert(2, comboboxStore);
                    table.Columns.Remove("Номер заказа");
                    table.Columns.Insert(0, comboboxOrder);
                }
                if (name == "order_t")
                {
                    table.Columns.Remove("Номер сотрудника");
                    table.Columns.Insert(1, comboboxWorker);
                }

                while (reader.Read())
                {
                    string[] arr = new string[reader.FieldCount];
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        arr[i] = reader.GetString(reader.GetName(i)).ToString();
                    }
                    table.Rows.Add(arr);
                }
                reader.Close();
            }

        }
        public static DataGridViewComboBoxColumn GetComboBox(string name, SqlConnection con)
        {
            DataGridViewComboBoxColumn combobox = new DataGridViewComboBoxColumn();
            if (name == "store")
            {
                MySqlDataReader storesReader = con.data("select store.real_address from store");
                while (storesReader.Read())
                {
                    combobox.Items.Add(storesReader.GetString("real_address"));
                }
                combobox.HeaderText = "Физический адрес магазина";
                combobox.Name = "Физический адрес магазина";
                storesReader.Close();
            }
            if (name == "good")
            {
                MySqlDataReader storesReader = con.data("select good.good_name from good");
                while (storesReader.Read())
                {
                    combobox.Items.Add(storesReader.GetString("good_name"));
                }
                combobox.HeaderText = "Наименование товара";
                combobox.Name = "Наименование товара";
                storesReader.Close();
            }
            if (name == "order_t")
            {
                MySqlDataReader storesReader = con.data("select order_t.order_id from order_t ORDER BY order_t.order_id");
                while (storesReader.Read())
                {
                    combobox.Items.Add(storesReader.GetString("order_id"));
                }
                combobox.HeaderText = "Номер заказа";
                combobox.Name = "Номер заказа";
                storesReader.Close();
            }
            if (name == "worker")
            {
                MySqlDataReader storesReader = con.data("select worker.worker_id from worker ORDER BY worker.worker_id");
                while (storesReader.Read())
                {
                    combobox.Items.Add(storesReader.GetString("worker_id"));
                }
                combobox.HeaderText = "Номер сотрудника";
                combobox.Name = "Номер сотрудника";
                storesReader.Close();
            }
            return combobox;
        }
    }
}