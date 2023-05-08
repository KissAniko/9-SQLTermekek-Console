using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9_SQLTermekek_Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string kapcsolatleiro = "datasource=127.0.0.1;port=3306;database=hardver;username=root;password=;";

            MySqlConnection SQLkapcsolat = new MySqlConnection(kapcsolatleiro);
            try
            {
                SQLkapcsolat.Open();
            }
            catch (MySqlException hiba)
            {
                Console.WriteLine(hiba.Message);
                Environment.Exit(1);
            }

            // Listázza ki azokat a merevlemezes termékeket (annak minden adatát),
            // amelyeg drágábbak mint a Seagate gyártó kínálatában szereplő legdrágább ilyen termék.
            // A lista legyen ár szerint növekvő. (Merevlemez --> Winchester 'szó')

            string SQLSelect = " SELECT * " +
                               " FROM termékek " +
                               " WHERE kategória " +
                               " LIKE 'Winchester%' " +
                               " AND Ár > ( SELECT MAX(Ár) " +
                                            " FROM termékek" +
                                            " WHERE kategória " +
                                            " LIKE 'Winchester%' " +
                                            " GROUP BY Gyártó " +
                                            " HAVING Gyártó = 'Seagate') " +
                               " ORDER BY Ár ";
            MySqlCommand SQLparancs = new MySqlCommand(SQLSelect, SQLkapcsolat);
            MySqlDataReader eredmenyOlvaso = SQLparancs.ExecuteReader();
            while (eredmenyOlvaso.Read())
            {

                Console.Write(eredmenyOlvaso.GetInt32("cikkszám"));
                Console.Write(eredmenyOlvaso.GetString("kategória"));
                Console.Write(eredmenyOlvaso.GetString("gyártó"));
                Console.Write(eredmenyOlvaso.GetString("Név"));
                Console.Write(eredmenyOlvaso.GetInt32("Ár"));
                Console.Write(eredmenyOlvaso.GetInt32("Garidő"));
                Console.Write(eredmenyOlvaso.GetString("Készlet"));
                Console.Write(eredmenyOlvaso.GetDouble("cikkszám"));   
                
                // beérkezés???? 

            }
            eredmenyOlvaso.Close();
            SQLkapcsolat.Close();
        }
    }
}
