using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BuildingMaterials
{
    public class SqlConnection
    {
        MySqlConnection connection;
        MySqlDataReader reader;

        public SqlConnection(string host, string database, string login, string password)
        {
            string con = $"server={host};user={login};database={database};password={password};CharSet=utf8;";
            connection = new MySqlConnection(con);
            connection.Open(); // открытие соединения 
        }

        public MySqlDataReader data(string sqlQuery) // передаем текст запроса
        {
            MySqlCommand command = new MySqlCommand(sqlQuery, connection);

            reader = command.ExecuteReader(); // исполнение запроса и получение данных из БД

            return reader;
        }

        public void query(string sqlQuery) // передаем текст запроса
        {
            MySqlCommand command = new MySqlCommand(sqlQuery, connection);
        }

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://localhost/phpmyadmin/"))
                {
                    return true;
                }
            }
            catch (WebException)
            {
                return false;
            }
        }
    }
}
