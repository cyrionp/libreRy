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
    /// Interaction logic for ucAdminKitaplariListele.xaml
    /// </summary>
    public partial class ucAdminKitaplariListele : UserControl
    {
        public ucAdminKitaplariListele()
        {
            InitializeComponent();
            TabloGuncelle();
            HizliArama();
        }
        string KitapId = "";

        SQLiteConnection con = new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + "\\db\\kutuphane.db3;Version=3; Catalog=Sample;");
        SQLiteCommand cmd;
        private void TabloGuncelle()
        {
            try
            {
                con.Open();
                string Query = "SELECT * FROM books";
                SQLiteCommand createCommand = new SQLiteCommand(Query, con);
                createCommand.ExecuteNonQuery();
                SQLiteDataAdapter dataAdp = new SQLiteDataAdapter(createCommand);
                DataTable dt = new DataTable("books");
                dataAdp.Fill(dt);
                dtgKitaplar.ItemsSource = dt.DefaultView;
                dataAdp.Update(dt);
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void KitapAra()
        {
            try
            {
                con.Open();
                string Query = "SELECT * FROM books WHERE ad LIKE '%" + this.txtAd.Text + "%' AND  adet LIKE '%" + this.txtAdet.Text + "%' AND kategori LIKE '%" + this.txtKategori.Text + "%' AND sayfa LIKE '%" + this.txtSayfa.Text + "%' AND yazar LIKE '%" + this.txtYazar.Text + "%' AND yil LIKE '%" + this.txtYil.Text + "%'";
                SQLiteCommand createCommand = new SQLiteCommand(Query, con);
                createCommand.ExecuteNonQuery();
                SQLiteDataAdapter dataAdp = new SQLiteDataAdapter(createCommand);
                DataTable dt = new DataTable("books");
                dataAdp.Fill(dt);
                dtgKitaplar.ItemsSource = dt.DefaultView;
                dataAdp.Update(dt);
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void HizliArama()
        {
            try
            {
                this.txtAd.KeyDown += new KeyEventHandler(txtAd_KeyDown);
                KitapAra();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dtgKitaplar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                txtAd.Text = row_selected["ad"].ToString();
                txtAdet.Text = row_selected["adet"].ToString();
                txtSayfa.Text = row_selected["sayfa"].ToString();
                txtYazar.Text = row_selected["yazar"].ToString();
                txtYil.Text = row_selected["yil"].ToString();
                txtKategori.Text = row_selected["kategori"].ToString();
                KitapId = row_selected["id"].ToString();
            }
        }

        private void btnKitapEkle_Click(object sender, RoutedEventArgs e)
        {
            if (txtAd.Text != "" && txtAdet.Text != "" && txtKategori.Text != "" && txtSayfa.Text != "" && txtYazar.Text != "" && txtYil.Text != "")
            {
                try
                {
                    cmd = new SQLiteCommand("INSERT INTO books(ad,adet,kategori,sayfa,yazar,yil) values(@ad,@adet,@kategori,@sayfa,@yazar,@yil)", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@ad", txtAd.Text);
                    cmd.Parameters.AddWithValue("@adet", txtAdet.Text);
                    cmd.Parameters.AddWithValue("@kategori", txtKategori.Text);
                    cmd.Parameters.AddWithValue("@sayfa", txtSayfa.Text);
                    cmd.Parameters.AddWithValue("@yazar", txtYazar.Text);
                    cmd.Parameters.AddWithValue("@yil", txtYil.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Book added");

                    txtAd.Clear();
                    txtAdet.Clear();
                    txtKategori.Clear();
                    txtSayfa.Clear();
                    txtYazar.Clear();
                    txtYil.Clear();
                    KitapId = "";

                    TabloGuncelle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnKitapGuncelle_Click(object sender, RoutedEventArgs e)
        {
            SQLiteConnection sqliteCon = new SQLiteConnection(con);
            if (txtAd.Text != "" && txtAdet.Text != "" && txtKategori.Text != "" && txtSayfa.Text != "" && txtYazar.Text != "" && txtYil.Text != "")
            {
                try
                {
                    sqliteCon.Open();
                    string Query_Update = "UPDATE books SET ad='" + this.txtAd.Text + "', adet='" + this.txtAdet.Text + "', kategori='" + this.txtKategori.Text + "', sayfa='" + this.txtSayfa.Text + "', yazar='" + this.txtYazar.Text + "', yil='" + this.txtYil.Text + "' WHERE id='" + KitapId + "'";
                    SQLiteCommand createCommand = new SQLiteCommand(Query_Update, sqliteCon);
                    createCommand.ExecuteNonQuery();
                    MessageBox.Show("Book Updated");
                    sqliteCon.Close();

                    txtAd.Clear();
                    txtAdet.Clear();
                    txtKategori.Clear();
                    txtSayfa.Clear();
                    txtYazar.Clear();
                    txtYil.Clear();
                    KitapId = "";

                    TabloGuncelle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnKitapSil_Click(object sender, RoutedEventArgs e)
        {
            SQLiteConnection sqliteCon = new SQLiteConnection(con);
            if (txtAd.Text != "" && txtAdet.Text != "" && txtKategori.Text != "" && txtSayfa.Text != "" && txtYazar.Text != "" && txtYil.Text != "")
            {
                try
                {
                    sqliteCon.Open();
                    string Query = "DELETE FROM books WHERE id='" + KitapId + "'";
                    SQLiteCommand createCommand = new SQLiteCommand(Query, sqliteCon);
                    createCommand.ExecuteNonQuery();
                    MessageBox.Show("Book Deleted");
                    sqliteCon.Close();

                    txtAd.Clear();
                    txtAdet.Clear();
                    txtKategori.Clear();
                    txtSayfa.Clear();
                    txtYazar.Clear();
                    txtYil.Clear();
                    KitapId = "";

                    TabloGuncelle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnKitapAra_Click(object sender, RoutedEventArgs e)
        {
            if (txtAd.Text != "" || txtAdet.Text!=""|| txtKategori.Text != "" || txtSayfa.Text!="" || txtYazar.Text != "" || txtYil.Text != "")
            {
                KitapAra();
            }
            else { MessageBox.Show("You have to type something"); }
        }

        private void txtAd_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtAd.Text != "" || txtAdet.Text != "" || txtKategori.Text != "" || txtSayfa.Text != "" || txtYazar.Text != "" || txtYil.Text != "")
            {
                KitapAra();
            }
            //else { MessageBox.Show("You have to type something"); }
        }
    }
}
