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
            MySqlDataReader reader = con.data($"select * from {name}");
            table.Rows.Clear();
            table.Columns.Clear();
            if (name == "good")
            {
                for (int i = 1; i < reader.FieldCount; i++)
                {
                    table.Columns.Add(columnName:reader.GetName(i), headerText: reader.GetName(i));
                }
                while(reader.Read())
                {
                    table.Rows.Add(reader.GetString("good_name").ToString(),
                    reader.GetString("brc_code").ToString(),
                    reader.GetString("Field_Of_Application").ToString(),
                    reader.GetString("Packaging").ToString(),
                    reader.GetString("Technical_Characteristics").ToString(),
                    reader.GetString("Instructions_For_Use").ToString(),
                    reader.GetString("Precautions").ToString(),
                    reader.GetString("Storage_And_Transportation").ToString(),
                    reader.GetString("Certificates").ToString(),
                    reader.GetString("Manufacturer_information").ToString()
                    );
                }
            }
            else if (name == "store")
            {
                for (int i = 1; i < reader.FieldCount; i++)
                {
                    table.Columns.Add(columnName: reader.GetName(i), headerText: reader.GetName(i));
                }
                while (reader.Read())
                {
                    table.Rows.Add(reader.GetString("store_name").ToString(),
                    reader.GetString("legal_address").ToString(),
                    reader.GetString("real_address").ToString(),
                    reader.GetString("Director_Second_name").ToString(),
                    reader.GetString("Director_First_name").ToString(),
                    reader.GetString("Director_Father_name").ToString(),
                    reader.GetString("phone_number").ToString()
                    );
                }
            }
            reader.Close();
        }
    }
}