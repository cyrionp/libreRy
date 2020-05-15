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
using System.IO;
using System.Data;
using System.Data.SQLite;
using System.Security.Cryptography;

namespace Libre_Ry.UserControls
{
    /// <summary>
    /// Interaction logic for ucKayit.xaml
    /// </summary>
    public partial class ucKayit : UserControl
    {
        private static string srKayitAd = "", srKayitSoyad = "", srKayitEmail = "", srKayitNo = "", srKayitSifre = "", srKayitSifreTekrar = "";
        string dbAdres = @"Data Source=" + Environment.CurrentDirectory + "\\db\\kutuphane.db3; Version=3; Compress=True;";

        public ucKayit()
        {
            InitializeComponent(); //Background="#FF633232"

            txtKayitAd.Text = srKayitAd;
            txtKayitSoyad.Text = srKayitSoyad;
            txtKayitEmail.Text = srKayitEmail;
            txtKayitNo.Text = srKayitNo;
            txtKayitSifre.Password = srKayitSifre;
            txtKayitSifreTekrar.Password = srKayitSifreTekrar;
        }

        private bool Kaydolabilme()
        {
            bool giris = false;
            SQLiteConnection con = new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + "\\db\\kutuphane.db3;Version=3; Catalog=Sample;");
            con.Open();
            SQLiteCommand cmdEmail = new SQLiteCommand("SELECT * FROM users WHERE email=@email", con);
            SQLiteCommand cmdNo = new SQLiteCommand("SELECT * FROM users WHERE no=@no", con);
            cmdEmail.Parameters.AddWithValue("@email", this.txtKayitEmail.Text);
            cmdNo.Parameters.AddWithValue("@no", this.txtKayitNo.Text);
            var cmdEmail_ = cmdEmail.ExecuteScalar();
            var cmdNo_ = cmdNo.ExecuteScalar();
            con.Close();
            if (cmdEmail_ == null && cmdNo_ == null)
            {
                giris = true;
            }
            if (cmdEmail_ != null)
            {
                giris = false;
            }
            if (cmdNo_ != null)
            {
                giris = false;
            }
            return giris;
        }

        private void btnucKayitRegister_Click(object sender, RoutedEventArgs e)
        {
            if (txtKayitEmail.Text != "" && txtKayitAd.Text != "" && txtKayitSoyad.Text != "" && txtKayitNo.Text != "" && txtKayitSifre.Password != "" && txtKayitSifreTekrar.Password != "")
            {
                SQLiteConnection con = new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + "\\db\\kutuphane.db3;Version=3; Catalog=Sample;");
                if (txtKayitEmail.Text.Contains("@") && txtKayitEmail.Text.Contains("."))
                {
                    if (Kaydolabilme() == true)
                    {
                        SHA1 sha = new SHA1CryptoServiceProvider();
                        string SifrelenecekSifre = txtKayitSifre.Password;
                        string SifrelenmisSifre = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(SifrelenecekSifre)));

                        if (cbOgretmen.IsChecked == true)
                        {
                            con.Open();
                            SQLiteCommand cmd;
                            string Query = "INSERT INTO users (ad,soyad,email,no,sifre,uyelik) values('" + this.txtKayitAd.Text + "','" + this.txtKayitSoyad.Text + "'," +
                                                       "'" + this.txtKayitEmail.Text + "'," + "'" + this.txtKayitNo.Text + "','" + SifrelenmisSifre + "','" + 4 + "')";
                            cmd = new SQLiteCommand(Query, con);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Registered Successfully\nbut until admin verify your account, you can't login");
                            con.Close();
                        }
                        else
                        {
                            try
                            {
                                con.Open();
                                SQLiteCommand cmd;
                                string Query = "INSERT INTO users (ad,soyad,email,no,sifre,uyelik) values('" + this.txtKayitAd.Text + "','" + this.txtKayitSoyad.Text + "'," +
                                                           "'" + this.txtKayitEmail.Text + "'," + "'" + this.txtKayitNo.Text + "','" + SifrelenmisSifre + "','" + 2 + "')";
                                cmd = new SQLiteCommand(Query, con);
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Registered Successfully!");
                                con.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("This user already exists.");
                    }
                }
                else
                {
                    MessageBox.Show("Please write a valid email address");
                }
            }
            else
            {
                MessageBox.Show("You missed something");
                return;
            }
        }
    }
}
