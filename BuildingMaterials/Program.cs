using MySql.Data.MySqlClient;
using System.Net;

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
                reader = con.data($"SELECT worker.Worker_Second_Name AS \"Фамилия\", \r\nworker.Worker_First_Name AS \"Имя\", \r\nCOUNT(good_selled.GOOD_SELLED_ID) AS \"Количество заказов\", \r\nSUM(good_selled.Good_Count * good_selled.Good_Cost) AS \"Сумма заказов\"\r\nFROM worker, order_t, good_selled\r\nWHERE worker.WORKER_ID = order_t.WORKER_ID \r\nAND order_t.ORDER_ID = good_selled.order_id\r\nGROUP BY order_t.WORKER_ID;");
                
            }
            else if (name == "good_selled")
            {
                reader = con.data("SELECT good.good_name AS \"Название товара\", \r\nstore.store_name AS \"Название магазина\", \r\nstore.real_address AS \"Физический адрес магазина\", \r\ngood_selled.good_count AS \"Количество\", \r\ngood_selled.good_cost AS \"Стоимость\", \r\norder_t.order_date AS \"Дата и время\"\r\nFROM good, store, good_selled, order_t\r\nWHERE good.good_id = good_selled.good_id\r\nAND store.store_id = good_selled.store_id\r\nAND order_t.order_id = good_selled.order_id\r\nORDER BY order_t.order_date;");
                
            }
            else if (name == "good_available")
            {
                reader = con.data("SELECT good.good_name AS \"Название товара\", \r\nstore.store_name AS \"Название магазина\", \r\nstore.real_address AS \"Физический адрес магазина\", \r\ngood_available.good_count AS \"Количество\", \r\ngood_available.good_cost AS \"Стоимость\"\r\nFROM good, store, good_available\r\nWHERE good.good_id = good_available.good_id\r\nAND store.store_id = good_available.store_id\r\nORDER BY good_available.good_id;");
                
            }
            else if (name == "order_t")
            {
                reader = con.data("SELECT good.good_name AS \"Название товара\", \r\nstore.store_name AS \"Название магазина\", \r\nstore.real_address AS \"Физический адрес магазина\", \r\ngood_available.good_count AS \"Количество\", \r\ngood_available.good_cost AS \"Стоимость\"\r\nFROM good, store, good_available\r\nWHERE good.good_id = good_available.good_id\r\nAND store.store_id = good_available.store_id\r\nORDER BY good_available.good_id;");
                
            }
            if (reader != null)
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    table.Columns.Add(columnName: reader.GetName(i), headerText: reader.GetName(i));
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