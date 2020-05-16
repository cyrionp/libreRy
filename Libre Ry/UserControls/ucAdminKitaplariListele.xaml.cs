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
            TextBosalt();
            KitapAra();
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

        private void TextBosalt()
        {
            this.txtAd.Clear();
            this.txtAdet.Clear();
            this.txtKategori.Clear();
            this.txtSayfa.Clear();
            this.txtYazar.Clear();
            this.txtYil.Clear();
            this.KitapId = "";
        }

        private void KitapAra()
        {
            if (txtAd.Text != "" || txtAdet.Text != "" || txtKategori.Text != "" || txtSayfa.Text != "" || txtYazar.Text != "" || txtYil.Text != "")
            {
                try
                {
                    this.txtAd.KeyDown += new KeyEventHandler(txtAd_KeyDown);
                    this.txtAdet.KeyDown += new KeyEventHandler(txtAdet_KeyDown);
                    con.Open();
                    string Query = "SELECT * FROM books WHERE ad LIKE '%" + this.txtAd.Text + "%' AND  adet LIKE '" + this.txtAdet.Text + "%' AND kategori LIKE '%" + this.txtKategori.Text + "%' AND sayfa LIKE '" + this.txtSayfa.Text + "%' AND yazar LIKE '%" + this.txtYazar.Text + "%' AND yil LIKE '" + this.txtYil.Text + "%'";
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

                    TextBosalt();
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

                    TextBosalt();
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

                    TextBosalt();
                    TabloGuncelle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnKitapYenile_Click(object sender, RoutedEventArgs e)
        {
            TabloGuncelle();
            TextBosalt();
        }

        private void txtAd_KeyDown(object sender, KeyEventArgs e) { KitapAra(); }

        private void txtYazar_KeyDown(object sender, KeyEventArgs e) { KitapAra(); }
        
        private void txtYil_KeyDown(object sender, KeyEventArgs e) { KitapAra(); }

        private void txtSayfa_KeyDown(object sender, KeyEventArgs e) { KitapAra(); }

        private void txtAdet_KeyDown(object sender, KeyEventArgs e) { KitapAra(); }

        private void txtKategori_KeyDown(object sender, KeyEventArgs e) { KitapAra(); }

        private void txtAd_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.txtAd.Text == "") { TabloGuncelle(); }
            else
            {
                if (e.Key == Key.Back)
                {
                    KitapAra();
                }
            }
        }

        private void txtYazar_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.txtYazar.Text == "") { TabloGuncelle(); }
            else
            {
                if (e.Key == Key.Back)
                {
                    KitapAra();
                }
            }
        }

        private void txtYil_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.txtYil.Text == "") { TabloGuncelle(); }
            else
            {
                if (e.Key == Key.Back)
                {
                    KitapAra();
                }
            }
        }

        private void txtSayfa_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.txtSayfa.Text == "") { TabloGuncelle(); }
            else
            {
                if (e.Key == Key.Back)
                {
                    KitapAra();
                }
            }
        }

        private void txtAdet_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.txtAdet.Text == "") { TabloGuncelle(); }
            else
            {
                if (e.Key == Key.Back)
                {
                    KitapAra();
                }
            }
        }

        private void txtKategori_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.txtKategori.Text == "") { TabloGuncelle(); }
            else
            {
                if (e.Key == Key.Back)
                {
                    KitapAra();
                }
            }
        }
    }
}
