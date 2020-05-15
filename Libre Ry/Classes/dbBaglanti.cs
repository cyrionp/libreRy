using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.SQLite;

namespace Libre_Ry.Classes
{
    public class dbBaglanti
    {
        public static string dbAdres = @"Data Source=" + Environment.CurrentDirectory + "\\db\\kutuphane.db3;Version=3;New=False;Compress=True;Read Only=False";

        public static string ConnectState;
        public static void ConnectionTest()
        {
            using (SQLiteConnection conn = new SQLiteConnection(dbAdres))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    try
                    {
                        conn.Open();
                        ConnectState = "Veri Tabanına Bağlanıldı";
                    }
                    catch (Exception)
                    {
                        ConnectState = "Veri Tabanı Bağlantı Hatası ...";
                    }
                }
                else
                {
                    ConnectState = "Veri Tabanına Bağlanıldı";
                }
            }
        }

        internal static void Open()
        {
            throw new NotImplementedException();
        }
    }
}