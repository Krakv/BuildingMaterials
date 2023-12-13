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
                reader = con.data($"SELECT good.good_id, good.good_name AS \"������������ ������\",\r\ngood.brc_code AS \"��� ���\",\r\ngood.Field_Of_Application AS \"������� ����������\", \r\ngood.Packaging AS \"��������\", \r\ngood.Technical_Characteristics AS \"����������� ��������������\",\r\ngood.Instructions_For_Use AS \"���������� �� ����������\", \r\ngood.Precautions AS \"���������������\", \r\ngood.Storage_And_Transportation AS \"�������� � ���������������\",\r\ngood.Certificates AS \"�����������\", \r\ngood.Manufacturer_information AS \"�������� � �������������\"\r\nFROM good;");
                
            }
            else if (name == "store")
            {
                reader = con.data($"SELECT store.store_id, store.store_name AS \"�������� ��������\",\r\nstore.Legal_Address AS \"����������� �����\",\r\nstore.real_address AS \"���������� �����\",\r\nstore.Director_Second_Name AS \"������� ���������\",\r\nstore.Director_First_Name AS \"��� ���������\",\r\nstore.Director_Father_Name AS \"�������� ���������\",\r\nstore.Phone_Number AS \"����� ��������\"\r\nFROM store");
                
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
                reader = con.data($"SELECT worker.worker_id, worker.Worker_Second_Name AS \"�������\", \r\nworker.Worker_First_Name AS \"���\", \r\nworker.Worker_Father_Name AS \"��������\", \r\nworker.Worker_Post AS \"���������\",\r\nworker.Phone_Number AS \"����� ��������\" from worker;");
                
            }
            else if (name == "good_selled")
            {
                comboboxGood = GetComboBox("good", con);
                comboboxStore = GetComboBox("store", con);
                comboboxOrder = GetComboBox("order_t", con);
                reader = con.data("SELECT good_selled.good_selled_id, good_selled.order_t_id AS \"����� ������\", good.good_name AS \"������������ ������\", \r\nstore.real_address AS \"���������� ����� ��������\", \r\ngood_selled.good_count AS \"����������\", \r\ngood_selled.good_cost AS \"���������\"FROM good, store, good_selled, order_t\r\nWHERE good.good_id = good_selled.good_id\r\nAND store.store_id = good_selled.store_id\r\nAND order_t.order_t_id = good_selled.order_t_id\r\nORDER BY order_t.order_date;");
                
            }
            else if (name == "good_available")
            {
                comboboxGood = GetComboBox("good", con);
                comboboxStore = GetComboBox("store", con);
                reader = con.data("SELECT good_available.good_available_id, good.good_name AS \"������������ ������\",  \r\nstore.real_address AS \"���������� ����� ��������\", \r\ngood_available.good_count AS \"����������\", \r\ngood_available.good_cost AS \"���������\"\r\nFROM good, store, good_available\r\nWHERE good.good_id = good_available.good_id\r\nAND store.store_id = good_available.store_id\r\nORDER BY good_available.good_id;");
                
            }
            else if (name == "order_t")
            {
                comboboxWorker = GetComboBox("worker", con);
                reader = con.data("SELECT order_t.order_t_id AS \"����� ������\",\r\norder_t.worker_id AS \"����� ����������\",\r\norder_t.Customer_Second_Name AS \"������� ����������\",\r\norder_t.Customer_First_Name AS \"��� ����������\",\r\norder_t.Customer_Father_Name AS \"�������� ����������\",\r\norder_t.Customer_Phone_Number AS \"����� �������� ����������\",\r\norder_t.order_date AS \"���� ������\"\r\nFROM order_t;");
                
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
                    table.Columns.Remove("������������ ������");
                    table.Columns.Insert(1, comboboxGood);
                    table.Columns.Remove("���������� ����� ��������");
                    table.Columns.Insert(2, comboboxStore);
                }
                if (name == "good_selled")
                {
                    table.Columns.Remove("������������ ������");
                    table.Columns.Insert(2, comboboxGood);
                    table.Columns.Remove("���������� ����� ��������");
                    table.Columns.Insert(3, comboboxStore);
                    table.Columns.Remove("����� ������");
                    table.Columns.Insert(1, comboboxOrder);
                }
                if (name == "order_t")
                {
                    table.Columns.Remove("����� ����������");
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
                MySqlDataReader storesReader = con.data("select order_t.order_t_id from order_t ORDER BY order_t.order_t_id");
                while (storesReader.Read())
                {
                    combobox.Items.Add(storesReader.GetString("order_t_id"));
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