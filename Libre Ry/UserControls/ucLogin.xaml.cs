using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Libre_Ry.Classes;
using Libre_Ry.UserControls;
using System.IO;
using System.Data;
using System.Data.SQLite;
using System.Security.Cryptography;

namespace Libre_Ry.UserControls
{
    /// <summary>
    /// Interaction logic for ucLogin.xaml
    /// </summary>
    public partial class ucLogin : UserControl
    {
        public ucLogin()
        {
            InitializeComponent();
        }

        public static object[] UserInfo = new object[7];

        private void btnucLoginLogin_Click(object sender, RoutedEventArgs e)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            string SifrelenecekSifre = txtLoginPassword.Password;
            string SifrelenmisSifre = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(SifrelenecekSifre)));

            DataTable dtTable = new DataTable();
            string cmdStr = "SELECT * FROM users WHERE no=@no and sifre=@sifre";

            using (SQLiteConnection connection = new SQLiteConnection(dbBaglanti.dbAdres))
            using (SQLiteCommand command = new SQLiteCommand(cmdStr, connection))
            {
                command.Parameters.AddWithValue("@no", txtLoginNo.Text);
                command.Parameters.AddWithValue("@sifre", SifrelenmisSifre);
                connection.Open();
                dtTable.Load(command.ExecuteReader());
                if (dtTable.Rows.Count == 0)
                {
                    MessageBox.Show("Wrong no or password");
                    return;
                }
                else
                {
                    var acikPencere = Window.GetWindow(this);
                    UserInfo = dtTable.Rows[0].ItemArray;
                    switch (dtTable.Rows[0]["uyelik"].ToString())
                    {
                        case "0":
                            break;
                        case "1":
                            panelAdmin admin = new panelAdmin();
                            admin.Show();
                            acikPencere.Close();
                            break;
                        case "2":
                            panelOgrenci ogrenci = new panelOgrenci();
                            ogrenci.Show();
                            acikPencere.Hide();
                            break;
                        case "3":
                            panelOgretmen ogretmen = new panelOgretmen();
                            ogretmen.Show();
                            acikPencere.Hide();
                            break;
                        case "4":
                            MessageBox.Show("Your instructor account isn't verified by Admin");
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}