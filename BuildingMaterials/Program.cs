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
                reader = con.data($"SELECT good.good_name AS \"������������ ������\",\r\ngood.brc_code AS \"��� ���\",\r\ngood.Field_Of_Application AS \"������� ����������\", \r\ngood.Packaging AS \"��������\", \r\ngood.Technical_Characteristics AS \"����������� ��������������\",\r\ngood.Instructions_For_Use AS \"���������� �� ����������\", \r\ngood.Precautions AS \"���������������\", \r\ngood.Storage_And_Transportation AS \"�������� � ���������������\",\r\ngood.Certificates AS \"�����������\", \r\ngood.Manufacturer_information AS \"�������� � �������������\"\r\nFROM good;");
                
            }
            else if (name == "store")
            {
                reader = con.data($"SELECT store.store_name AS \"�������� ��������\",\r\nstore.Legal_Address AS \"����������� �����\",\r\nstore.real_address AS \"���������� �����\",\r\nstore.Director_Second_Name AS \"������� ���������\",\r\nstore.Director_First_Name AS \"��� ���������\",\r\nstore.Director_Father_Name AS \"�������� ���������\",\r\nstore.Phone_Number AS \"����� ��������\"\r\nFROM store");
                
            }
            else if (name == "worker")
            {
                comboboxStore = GetComboBox("store", con);
                reader = con.data($"SELECT worker.Worker_Second_Name AS \"�������\", \r\nworker.Worker_First_Name AS \"���\", \r\nworker.Worker_Father_Name AS \"��������\", \r\nworker.Worker_Post AS \"���������\",\r\nworker.Phone_Number AS \"����� ��������\",\r\nstore.real_address AS \"���������� ����� ��������\"\r\nfrom worker, worker_store, store\r\nWHERE worker.worker_id = worker_store.worker_id\r\nAND worker_store.store_id = store.store_id;");
                
            }
            else if (name == "good_selled")
            {
                comboboxGood = GetComboBox("good", con);
                comboboxStore = GetComboBox("store", con);
                comboboxOrder = GetComboBox("order_t", con);
                reader = con.data("SELECT good_selled.order_id AS \"����� ������\", good.good_name AS \"������������ ������\", \r\nstore.real_address AS \"���������� ����� ��������\", \r\ngood_selled.good_count AS \"����������\", \r\ngood_selled.good_cost AS \"���������\", \r\norder_t.order_date AS \"���� � �����\"\r\nFROM good, store, good_selled, order_t\r\nWHERE good.good_id = good_selled.good_id\r\nAND store.store_id = good_selled.store_id\r\nAND order_t.order_id = good_selled.order_id\r\nORDER BY order_t.order_date;");
                
            }
            else if (name == "good_available")
            {
                comboboxGood = GetComboBox("good", con);
                comboboxStore = GetComboBox("store", con);
                reader = con.data("SELECT good.good_name AS \"������������ ������\",  \r\nstore.real_address AS \"���������� ����� ��������\", \r\ngood_available.good_count AS \"����������\", \r\ngood_available.good_cost AS \"���������\"\r\nFROM good, store, good_available\r\nWHERE good.good_id = good_available.good_id\r\nAND store.store_id = good_available.store_id\r\nORDER BY good_available.good_id;");
                
            }
            else if (name == "order_t")
            {
                comboboxWorker = GetComboBox("worker", con);
                reader = con.data("SELECT order_t.order_id AS \"����� ������\",\r\norder_t.worker_id AS \"����� ����������\",\r\norder_t.Customer_Second_Name AS \"������� ����������\",\r\norder_t.Customer_First_Name AS \"��� ����������\",\r\norder_t.Customer_Father_Name AS \"�������� ����������\",\r\norder_t.Customer_Phone_Number AS \"����� �������� ����������\",\r\norder_t.order_date AS \"���� ������\"\r\nFROM order_t;");
                
            }
            if (reader != null)
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    table.Columns.Add(columnName: reader.GetName(i), headerText: reader.GetName(i));
                }
                if (name == "worker")
                {
                    table.Columns.Remove("���������� ����� ��������");
                    table.Columns.Add(comboboxStore);
                }
                if (name == "good_available")
                {
                    table.Columns.Remove("������������ ������");
                    table.Columns.Insert(0, comboboxGood);
                    table.Columns.Remove("���������� ����� ��������");
                    table.Columns.Insert(1, comboboxStore);
                }
                if (name == "good_selled")
                {
                    table.Columns.Remove("������������ ������");
                    table.Columns.Insert(1, comboboxGood);
                    table.Columns.Remove("���������� ����� ��������");
                    table.Columns.Insert(2, comboboxStore);
                    table.Columns.Remove("����� ������");
                    table.Columns.Insert(0, comboboxOrder);
                }
                if (name == "order_t")
                {
                    table.Columns.Remove("����� ����������");
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
                combobox.HeaderText = "���������� ����� ��������";
                combobox.Name = "���������� ����� ��������";
                storesReader.Close();
            }
            if (name == "good")
            {
                MySqlDataReader storesReader = con.data("select good.good_name from good");
                while (storesReader.Read())
                {
                    combobox.Items.Add(storesReader.GetString("good_name"));
                }
                combobox.HeaderText = "������������ ������";
                combobox.Name = "������������ ������";
                storesReader.Close();
            }
            if (name == "order_t")
            {
                MySqlDataReader storesReader = con.data("select order_t.order_id from order_t ORDER BY order_t.order_id");
                while (storesReader.Read())
                {
                    combobox.Items.Add(storesReader.GetString("order_id"));
                }
                combobox.HeaderText = "����� ������";
                combobox.Name = "����� ������";
                storesReader.Close();
            }
            if (name == "worker")
            {
                MySqlDataReader storesReader = con.data("select worker.worker_id from worker ORDER BY worker.worker_id");
                while (storesReader.Read())
                {
                    combobox.Items.Add(storesReader.GetString("worker_id"));
                }
                combobox.HeaderText = "����� ����������";
                combobox.Name = "����� ����������";
                storesReader.Close();
            }
            return combobox;
        }
    }
}