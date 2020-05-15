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
using System.Data;
using System.Data.SQLite;

namespace Libre_Ry.UserControls
{
    /// <summary>
    /// Interaction logic for ucAdminKullanicilar.xaml
    /// </summary>
    public partial class ucAdminKullanicilar : UserControl
    {
        public ucAdminKullanicilar()
        {
            InitializeComponent();
            TabloGuncelle();
        }

        SQLiteConnection con = new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + "\\db\\kutuphane.db3;Version=3; Catalog=Sample;");
        private void TabloGuncelle()
        {
            try
            {
                con.Open();
                string Query = "SELECT * FROM users";
                SQLiteCommand createCommand = new SQLiteCommand(Query, con);
                createCommand.ExecuteNonQuery();
                SQLiteDataAdapter dataAdp = new SQLiteDataAdapter(createCommand);
                DataTable dt = new DataTable("users");
                dataAdp.Fill(dt);
                dtgUyeler.ItemsSource = dt.DefaultView;
                dataAdp.Update(dt);
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dtgUyeler_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                string uyelik = row_selected["uyelik"].ToString();
                txtNo.Text = row_selected["no"].ToString();
                txtAd.Text = row_selected["ad"].ToString();
                txtSoyad.Text = row_selected["soyad"].ToString();
                txtEmail.Text = row_selected["email"].ToString();
                lblId.Content = row_selected["id"].ToString();
                lblUyelikTut.Content = uyelik;

                if (uyelik == "1")
                {
                    lblUyelik.Content = "Admin";
                }
                else if (uyelik == "2")
                {
                    lblUyelik.Content = "Student";
                }
                else if (uyelik == "3")
                {
                    lblUyelik.Content = "Intructor";
                }
                else if (uyelik == "4")
                {
                    lblUyelik.Content = "Wanna be instructor";
                }
            }
        }

        private void btnUyeSil_Click(object sender, RoutedEventArgs e)
        {
            SQLiteConnection sqliteCon = new SQLiteConnection(con);
            try
            {
                sqliteCon.Open();
                string Query = "DELETE FROM users WHERE email='" + this.txtEmail.Text + "'";
                SQLiteCommand createCommand = new SQLiteCommand(Query, sqliteCon);
                createCommand.ExecuteNonQuery();
                MessageBox.Show("User Deleted");
                sqliteCon.Close();

                lblId.Content = "";
                lblUyelik.Content = "";
                lblUyelikTut.Content = "";
                txtNo.Clear();
                txtAd.Clear();
                txtSoyad.Clear();
                txtEmail.Clear();

                TabloGuncelle();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUyeGuncelle_Click(object sender, RoutedEventArgs e)
        {
            SQLiteConnection sqliteCon = new SQLiteConnection(con);
            if (this.txtEmail.Text.Contains("@") && this.txtEmail.Text.Contains("."))
            {
                try
                {
                    sqliteCon.Open();
                    string Query_Update = "UPDATE users SET no='" + this.txtNo.Text + "', ad='" + this.txtAd.Text + "', soyad='" + this.txtSoyad.Text + "', email='" + this.txtEmail.Text + "', uyelik='" + this.lblUyelikTut.Content + "' WHERE id='" + lblId.Content + "'";
                    SQLiteCommand createCommand = new SQLiteCommand(Query_Update, sqliteCon);
                    createCommand.ExecuteNonQuery();
                    MessageBox.Show("User Updated");
                    sqliteCon.Close();

                    txtNo.Clear();
                    txtAd.Clear();
                    txtSoyad.Clear();
                    txtEmail.Clear();
                    lblId.Content = "";
                    lblUyelik.Content = "";
                    lblUyelikTut.Content = "";

                    TabloGuncelle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please write a valid email adress");
            }
        }

        private void btnOgrenciYap_Click(object sender, RoutedEventArgs e)
        {
            lblUyelikTut.Content = "2";
            lblUyelik.Content = "Will Be Student";
        }

        private void btnOgretmenYap_Click(object sender, RoutedEventArgs e)
        {
            lblUyelikTut.Content = "3";
            lblUyelik.Content = "Will Be Student";
        }

        private void btnAdminYap_Click(object sender, RoutedEventArgs e)
        {
            lblUyelikTut.Content = "1";
            lblUyelik.Content = "Will Be Admin";
        }
    }
}
