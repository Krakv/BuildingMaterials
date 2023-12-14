using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuildingMaterials
{
    public partial class Admin : Form
    {
        Form1 owner;
        SqlConnection connection;
        string current = "good";
        static string[] editRow = null;
        static int editId = -1;
        static int editIndex = -1;

        public Admin(SqlConnection connection, Form1 owner)
        {
            this.connection = connection;
            this.owner = owner;
            InitializeComponent();
            Reload(current);
        }

        public void Reload(string name)
        {
            Program.InitializeTables(dataGridView1, name, connection);
        }

        private void goodTableMenu_Click(object sender, EventArgs e)
        {
            current = "good";
            Reload(current);
        }

        private void goodAvailableTableMenu_Click(object sender, EventArgs e)
        {
            current = "good_available";
            Reload(current);
        }

        private void orderTableMenu_Click(object sender, EventArgs e)
        {
            current = "order_t";
            Reload(current);
        }

        private void workerTableMenu_Click(object sender, EventArgs e)
        {
            current = "worker";
            Reload(current);
        }

        private void storeTableMenu_Click(object sender, EventArgs e)
        {
            current = "store";
            Reload(current);
        }

        private void goodSelledTableMenu_Click(object sender, EventArgs e)
        {
            current = "good_selled";
            Reload(current);
        }
        private void Admin_FormClosed(object sender, FormClosedEventArgs e)
        {
            owner.Visible = true;
            owner.Activate();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            MySqlDataReader reader = null;
            string values = "";
            try
            {
                string[] arr = new string[dataGridView1.ColumnCount];
                int index = dataGridView1.Rows.GetLastRow(new DataGridViewElementStates());
                for (int i = 1; i < dataGridView1.ColumnCount; i++)
                {
                    var temp = dataGridView1.Rows[index].Cells[i].Value;
                    if (temp != null)
                        arr[i - 1] = temp.ToString();
                    else arr[i - 1] = "NULL";
                }
                if (current == "good")
                    values = $"NULL, \"{arr[0]}\", \"{arr[1]}\", \"{arr[2]}\", \"{arr[3]}\", \"{arr[4]}\", \"{arr[5]}\", \"{arr[6]}\", \"{arr[7]}\", \"{arr[8]}\", \"{arr[9]}\"";
                if (current == "order_t")
                    values = $"NULL, {arr[0]}, \"{arr[1]}\", \"{arr[2]}\", \"{arr[3]}\", \"{arr[4]}\", \"{arr[5]}\"";
                if (current == "store")
                    values = $"NULL, \"{arr[0]}\", \"{arr[1]}\", \"{arr[2]}\", \"{arr[3]}\", \"{arr[4]}\", \"{arr[5]}\", \"{arr[6]}\"";
                if (current == "worker")
                {
                    if (arr[5] != "NULL")
                    {
                        reader = connection.data($"select store.store_id from store where store.real_address = \"{arr[5]}\"");
                        reader.Read(); arr[5] = reader.GetString(0); reader.Close();
                    }
                    values = $"NULL, \"{arr[0]}\", \"{arr[1]}\", \"{arr[2]}\", \"{arr[3]}\", \"{arr[4]}\"";
                }
                if (current == "good_available")
                {
                    reader = connection.data($"select good.good_id from good where good.good_name = \"{arr[0]}\"");
                    reader.Read(); arr[0] = reader.GetString(0); reader.Close();
                    reader = connection.data($"select store.store_id from store where store.real_address = \"{arr[1]}\"");
                    reader.Read(); arr[1] = reader.GetString(0); reader.Close();
                    values = $"NULL, {arr[0]}, {arr[1]}, {arr[2]}, {arr[3]}";
                }
                if (current == "good_selled")
                {
                    reader = connection.data($"select good.good_id from good where good.good_name = \"{arr[1]}\"");
                    reader.Read(); arr[1] = reader.GetString(0); reader.Close();
                    reader = connection.data($"select store.store_id from store where store.real_address = \"{arr[2]}\"");
                    reader.Read(); arr[2] = reader.GetString(0); reader.Close();
                    values = $"NULL, {arr[1]}, {arr[0]}, {arr[2]}, {arr[3]}, {arr[4]}";
                }

                try
                {
                    reader = connection.data($"INSERT INTO {current} VALUES ({values})");
                    reader.Read();
                    reader.Close();
                    if (current == "worker" && arr[5] != "NULL")
                    {
                        reader = connection.data($"select MAX(worker.worker_id) from worker");
                        reader.Read(); string str = reader.GetString(0); reader.Close();
                        reader = connection.data($"INSERT INTO worker_store VALUES (NULL, {str}, {arr[5]})");
                        reader.Read(); reader.Close();
                    }

                }
                catch (Exception ex)
                {
                    if (reader != null)
                        reader.Close();
                    MessageBox.Show("Невозможно добавить\n" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                if (reader != null)
                    reader.Close();
                MessageBox.Show("Неправильно введены данные\n" + ex.Message);
            }
            Reload(current);
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            var m = MessageBox.Show("Вы точно хотите удалить значения?", "Удаление", MessageBoxButtons.YesNo);
            if (m == DialogResult.Yes)
            {
                int index = dataGridView1.CurrentCell.RowIndex;
                MySqlDataReader reader = null;
                try
                {
                    var value = dataGridView1[0, index].Value;
                    if (value != null)
                    {
                        reader = connection.data($"delete from {current} where {current}_id = {value.ToString()}");
                        reader.Read();
                        reader.Close();
                    }
                    else
                        MessageBox.Show("Выбрана неверная строка");
                }
                catch (Exception ex)
                {
                    if (reader != null)
                        reader.Close();
                    MessageBox.Show("Невозможно удалить\n" + ex.Message);
                }
                Reload(current);
            }
        }

        private void update_Click(object sender, EventArgs e)
        {
            Reload(current);
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            MySqlDataReader reader = null;
            string values = "";
            try
            {
                if (editIndex == -1)
                {
                    int index = dataGridView1.CurrentCell.RowIndex;
                    editIndex = index;
                    editId = Int32.Parse(dataGridView1[0, index].Value.ToString());
                    dataGridView1.Rows[index].ReadOnly = false;
                }
                else
                {
                    string[] arr = new string[dataGridView1.ColumnCount];
                    for (int i = 1; i < dataGridView1.ColumnCount; i++)
                    {
                        var temp = dataGridView1.Rows[editIndex].Cells[i].Value;
                        if (temp != null)
                            arr[i - 1] = temp.ToString();
                        else arr[i - 1] = "NULL";
                    }
                    if (current == "good")
                        values = $"good_name = \"{arr[0]}\", BRC_Code = \"{arr[1]}\", Field_Of_Application = \"{arr[2]}\", Packaging = \"{arr[3]}\", Technical_Characteristics = \"{arr[4]}\", Instructions_For_Use = \"{arr[5]}\", Precautions = \"{arr[6]}\", Storage_And_Transportation = \"{arr[7]}\", Certificates = \"{arr[8]}\", Manufacturer_Information = \"{arr[9]}\"";
                    if (current == "order_t")
                        values = $"WORKER_ID = {arr[0]}, Customer_Second_Name = \"{arr[1]}\", Customer_First_Name = \"{arr[2]}\", Customer_Father_Name = \"{arr[3]}\", Customer_Phone_Number = \"{arr[4]}\", Order_Date = \"{arr[5]}\"";
                    if (current == "store")
                        values = $"Store_Name = \"{arr[0]}\", Legal_Address = \"{arr[1]}\", Real_Address = \"{arr[2]}\", Director_Second_Name = \"{arr[3]}\", Director_First_Name = \"{arr[4]}\", Director_Father_Name = \"{arr[5]}\", Phone_Number = \"{arr[6]}\"";
                    if (current == "worker")
                    {
                        //if (arr[5] != "NULL")
                        //{
                        //    reader = connection.data($"select store.store_id from store where store.real_address = \"{arr[5]}\"");
                        //    reader.Read(); arr[5] = reader.GetString(0); reader.Close();
                        //}
                        values = $"Worker_Second_Name = \"{arr[0]}\", Worker_First_Name = \"{arr[1]}\", Worker_Father_Name = \"{arr[2]}\", Worker_Post = \"{arr[3]}\", Phone_Number = \"{arr[4]}\"";
                    }
                    if (current == "good_available")
                    {
                        reader = connection.data($"select good.good_id from good where good.good_name = \"{arr[0]}\"");
                        reader.Read(); arr[0] = reader.GetString(0); reader.Close();
                        reader = connection.data($"select store.store_id from store where store.real_address = \"{arr[1]}\"");
                        reader.Read(); arr[1] = reader.GetString(0); reader.Close();
                        values = $"GOOD_ID = {arr[0]}, STORE_ID = {arr[1]}, Good_Count = {arr[2]}, Good_Cost = {arr[3]}";
                    }
                    if (current == "good_selled")
                    {
                        reader = connection.data($"select good.good_id from good where good.good_name = \"{arr[1]}\"");
                        reader.Read(); arr[1] = reader.GetString(0); reader.Close();
                        reader = connection.data($"select store.store_id from store where store.real_address = \"{arr[2]}\"");
                        reader.Read(); arr[2] = reader.GetString(0); reader.Close();
                        values = $"GOOD_ID = {arr[1]}, ORDER_T_ID = {arr[0]}, STORE_ID = {arr[2]}, Good_Count = {arr[3]}, Good_Cost = {arr[4]}";
                    }

                    reader = connection.data($"UPDATE {current} SET {values} where {current}_id = {editId}");
                    reader.Read();
                    reader.Close();
                    //if (current == "worker" && arr[5] != "NULL")
                    //{
                    //    reader = connection.data($"select MAX(worker.worker_id) from worker");
                    //    reader.Read(); string str = reader.GetString(0); reader.Close();
                    //    reader = connection.data($"INSERT INTO worker_store VALUES (NULL, {str}, {arr[5]})");
                    //    reader.Read(); reader.Close();
                    //}
                    dataGridView1.Rows[editIndex].ReadOnly = true;
                    editIndex = -1;
                    editId = -1;
                    Reload(current);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Невозможно редактировать " + ex.Message);
                if (editIndex != -1)
                    dataGridView1.Rows[editIndex].ReadOnly = true;
                editId = -1;
                editIndex = -1;
                Reload(current);
            }
            if (reader != null)
            {
                reader.Close();
            }
            
            
        }

        //private void Edit_Click(object sender, EventArgs e)
        //{
        //    MySqlDataReader reader = null;
        //    try
        //    {
        //        if (editRow == null)
        //        {
        //            int index = dataGridView1.CurrentCell.RowIndex;
        //            editRow = GetStrings(dataGridView1.Rows[index].Cells);
        //            editIndex = index;
        //            dataGridView1.Rows[index].ReadOnly = false;
        //        }
        //        else
        //        {
        //            reader = connection.data($"select * from {current}");
        //            List<string> names = new List<string>();
        //            for (int i = 0; i < reader.FieldCount; i++)
        //            {
        //                names.Add(reader.GetName(i));
        //            }
        //            reader.Read(); reader.Close();
        //            string query = $"update {current} set ";
        //            for (int i = 0; i < dataGridView1.Columns.Count; i++)
        //            {
        //                if (editRow[i] != dataGridView1.Rows[editIndex].Cells[i].ToString())
        //                {
        //                    if (i != 0) query += ',';
        //                    query += $"{names[i]} = \"{dataGridView1[i, editIndex].Value.ToString()}\" ";
        //                    Console.WriteLine(names[i] + " " + dataGridView1[i, editIndex].Value.ToString() + " " + names[i] == dataGridView1[i, editIndex].Value.ToString());
        //                }
        //            }
        //            query += $" where {current}_id = {dataGridView1[0, editIndex].Value}";
        //            reader = connection.data(query);
        //            reader.Read(); reader.Close();
        //            dataGridView1.Rows[editIndex].ReadOnly = true;
        //            editRow = null;
        //            Reload(current);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Невозможно редактировать " + ex.Message);
        //        dataGridView1.Rows[editIndex].ReadOnly = true;
        //        editRow = null;
        //        Reload(current);
        //    }
        //    if (reader != null)
        //    {
        //        reader.Close();
        //    }


        //}

        static string[] GetStrings(DataGridViewCellCollection arr)
        {
            string[] res = new string[arr.Count];
            int index = 0;
            foreach (DataGridViewCell cell in arr)
            {
                var temp = cell.Value;
                if (temp != null)
                    res[index++] = temp.ToString();
                else
                    res[index++] = "";
            }
            return res;
        }
    }
}
