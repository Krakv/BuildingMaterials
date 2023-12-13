using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

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
            List<List<string>> list = new List<List<string>>();
            MySqlDataReader reader = null;
            table.Rows.Clear();
            table.Columns.Clear();
            DataGridViewComboBoxColumn comboboxStore = new DataGridViewComboBoxColumn();
            DataGridViewComboBoxColumn comboboxGood = new DataGridViewComboBoxColumn();
            DataGridViewComboBoxColumn comboboxOrder = new DataGridViewComboBoxColumn();
            DataGridViewComboBoxColumn comboboxWorker = new DataGridViewComboBoxColumn();
            if (name == "good")
            {
                reader = con.data($"SELECT good.good_id, good.good_name AS \"Наименование товара\",\r\ngood.brc_code AS \"Код КСР\",\r\ngood.Field_Of_Application AS \"Область применения\", \r\ngood.Packaging AS \"Упаковка\", \r\ngood.Technical_Characteristics AS \"Технические характеристики\",\r\ngood.Instructions_For_Use AS \"Инструкция по применению\", \r\ngood.Precautions AS \"Предостережения\", \r\ngood.Storage_And_Transportation AS \"Хранение и транспортировка\",\r\ngood.Certificates AS \"Сертификаты\", \r\ngood.Manufacturer_information AS \"Сведения о производителе\"\r\nFROM good;");
                
            }
            else if (name == "store")
            {
                reader = con.data($"SELECT store.store_id, store.store_name AS \"Название магазина\",\r\nstore.Legal_Address AS \"Юридический адрес\",\r\nstore.real_address AS \"Физический адрес\",\r\nstore.Director_Second_Name AS \"Фамилия директора\",\r\nstore.Director_First_Name AS \"Имя директора\",\r\nstore.Director_Father_Name AS \"Отчество директора\",\r\nstore.Phone_Number AS \"Номер телефона\"\r\nFROM store");
                
            }
            else if (name == "worker")
            {
                reader = con.data($"SELECT worker.worker_id, store.real_address from worker, worker_store, store WHERE worker.worker_id = worker_store.worker_id AND worker_store.store_id = store.store_id");
                int index = 0;
                while (reader.Read())
                {
                    list.Add(new List<string>());
                    list[index].Add(reader.GetString("worker_id"));
                    list[index++].Add(reader.GetString("real_address"));
                }
                reader.Close();
                comboboxStore = GetComboBox("store", con);
                reader = con.data($"SELECT worker.worker_id, worker.Worker_Second_Name AS \"Фамилия\", \r\nworker.Worker_First_Name AS \"Имя\", \r\nworker.Worker_Father_Name AS \"Отчество\", \r\nworker.Worker_Post AS \"Должность\",\r\nworker.Phone_Number AS \"Номер телефона\" from worker;");
                
            }
            else if (name == "good_selled")
            {
                comboboxGood = GetComboBox("good", con);
                comboboxStore = GetComboBox("store", con);
                comboboxOrder = GetComboBox("order_t", con);
                reader = con.data("SELECT good_selled.good_selled_id, good_selled.order_t_id AS \"Номер заказа\", good.good_name AS \"Наименование товара\", \r\nstore.real_address AS \"Физический адрес магазина\", \r\ngood_selled.good_count AS \"Количество\", \r\ngood_selled.good_cost AS \"Стоимость\"FROM good, store, good_selled, order_t\r\nWHERE good.good_id = good_selled.good_id\r\nAND store.store_id = good_selled.store_id\r\nAND order_t.order_t_id = good_selled.order_t_id\r\nORDER BY order_t.order_date;");
                
            }
            else if (name == "good_available")
            {
                comboboxGood = GetComboBox("good", con);
                comboboxStore = GetComboBox("store", con);
                reader = con.data("SELECT good_available.good_available_id, good.good_name AS \"Наименование товара\",  \r\nstore.real_address AS \"Физический адрес магазина\", \r\ngood_available.good_count AS \"Количество\", \r\ngood_available.good_cost AS \"Стоимость\"\r\nFROM good, store, good_available\r\nWHERE good.good_id = good_available.good_id\r\nAND store.store_id = good_available.store_id\r\nORDER BY good_available.good_id;");
                
            }
            else if (name == "order_t")
            {
                comboboxWorker = GetComboBox("worker", con);
                reader = con.data("SELECT order_t.order_t_id AS \"Номер заказа\",\r\norder_t.worker_id AS \"Номер сотрудника\",\r\norder_t.Customer_Second_Name AS \"Фамилия покупателя\",\r\norder_t.Customer_First_Name AS \"Имя покупателя\",\r\norder_t.Customer_Father_Name AS \"Отчество покупателя\",\r\norder_t.Customer_Phone_Number AS \"Номер телефона покупателя\",\r\norder_t.order_date AS \"Дата заказа\"\r\nFROM order_t;");
                
            }
            if (reader != null)
            {
                table.Columns.Add(columnName: reader.GetName(0), headerText: reader.GetName(0));
                if (name != "order_t")
                    table.Columns[0].Visible = false;
                for (int i = 1; i < reader.FieldCount; i++)
                {
                    table.Columns.Add(columnName: reader.GetName(i), headerText: reader.GetName(i));
                }
                if (name == "worker")
                {
                    table.Columns.Add(comboboxStore);
                }
                if (name == "good_available")
                {
                    table.Columns.Remove("Наименование товара");
                    table.Columns.Insert(1, comboboxGood);
                    table.Columns.Remove("Физический адрес магазина");
                    table.Columns.Insert(2, comboboxStore);
                }
                if (name == "good_selled")
                {
                    table.Columns.Remove("Наименование товара");
                    table.Columns.Insert(2, comboboxGood);
                    table.Columns.Remove("Физический адрес магазина");
                    table.Columns.Insert(3, comboboxStore);
                    table.Columns.Remove("Номер заказа");
                    table.Columns.Insert(1, comboboxOrder);
                }
                if (name == "order_t")
                {
                    table.Columns.Remove("Номер сотрудника");
                    table.Columns.Insert(1, comboboxWorker);
                }

                int index = 0;
                int j = 0;
                while (reader.Read())
                {
                    string[] arr = new string[reader.FieldCount + 1];
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var temp = reader.GetValue(i);
                        if (temp != null) arr[i] = temp.ToString();
                        else arr[i] = "";
                    }
                    if (name == "worker" && j < list.Count && list[j][0] == arr[0])
                        arr[6] = list[j++][1];
                    table.Rows.Add(arr);
                    table.Rows[index++].ReadOnly = true;
                }
                reader.Close();
                
                reader.Close();
                table.Rows.Add();
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
                MySqlDataReader storesReader = con.data("select order_t.order_t_id from order_t ORDER BY order_t.order_t_id");
                while (storesReader.Read())
                {
                    combobox.Items.Add(storesReader.GetString("order_t_id"));
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