using MySql.Data.MySqlClient;
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
            DataGridViewComboBoxColumn combobox = new DataGridViewComboBoxColumn();
            if (name == "worker")
            {
                MySqlDataReader storesReader = con.data("select store.real_address from store");
                while (storesReader.Read())
                {
                    combobox.Items.Add(storesReader.GetString("real_address"));
                }
                combobox.HeaderText = "���������� �����";
                combobox.Name = "���������� �����";
                storesReader.Close();
            }
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
                reader = con.data($"SELECT worker.Worker_Second_Name AS \"�������\", \r\nworker.Worker_First_Name AS \"���\", \r\nworker.Worker_Father_Name AS \"��������\", \r\nworker.Worker_Post AS \"���������\",\r\nworker.Phone_Number AS \"����� ��������\",\r\nstore.real_address AS \"���������� �����\"\r\nfrom worker, worker_store, store\r\nWHERE worker.worker_id = worker_store.worker_id\r\nAND worker_store.store_id = store.store_id;");
                
            }
            else if (name == "good_selled")
            {
                reader = con.data("SELECT good.good_name AS \"�������� ������\", \r\nstore.store_name AS \"�������� ��������\", \r\nstore.real_address AS \"���������� ����� ��������\", \r\ngood_selled.good_count AS \"����������\", \r\ngood_selled.good_cost AS \"���������\", \r\norder_t.order_date AS \"���� � �����\"\r\nFROM good, store, good_selled, order_t\r\nWHERE good.good_id = good_selled.good_id\r\nAND store.store_id = good_selled.store_id\r\nAND order_t.order_id = good_selled.order_id\r\nORDER BY order_t.order_date;");
                
            }
            else if (name == "good_available")
            {
                reader = con.data("SELECT good.good_name AS \"�������� ������\", \r\nstore.store_name AS \"�������� ��������\", \r\nstore.real_address AS \"���������� ����� ��������\", \r\ngood_available.good_count AS \"����������\", \r\ngood_available.good_cost AS \"���������\"\r\nFROM good, store, good_available\r\nWHERE good.good_id = good_available.good_id\r\nAND store.store_id = good_available.store_id\r\nORDER BY good_available.good_id;");
                
            }
            else if (name == "order_t")
            {
                reader = con.data("SELECT good.good_name AS \"�������� ������\", \r\nstore.store_name AS \"�������� ��������\", \r\nstore.real_address AS \"���������� ����� ��������\", \r\ngood_available.good_count AS \"����������\", \r\ngood_available.good_cost AS \"���������\"\r\nFROM good, store, good_available\r\nWHERE good.good_id = good_available.good_id\r\nAND store.store_id = good_available.store_id\r\nORDER BY good_available.good_id;");
                
            }
            if (reader != null)
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    table.Columns.Add(columnName: reader.GetName(i), headerText: reader.GetName(i));
                }
                if (name == "worker")
                {
                    table.Columns.Remove("���������� �����");
                    table.Columns.Add(combobox);
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
    }
}