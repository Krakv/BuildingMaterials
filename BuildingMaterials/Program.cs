using MySql.Data.MySqlClient;
using System.Net;

namespace BuildingMaterials
{
    internal static class Program
    {


        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }

        
    }
}