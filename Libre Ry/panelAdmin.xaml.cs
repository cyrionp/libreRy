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
using System.Windows.Shapes;
using System.IO;
using System.Data;
using System.Data.SQLite;
using Libre_Ry.Classes;
using Libre_Ry.UserControls;

namespace Libre_Ry
{
    /// <summary>
    /// Interaction logic for panelAdmin.xaml
    /// </summary>
    public partial class panelAdmin : Window
    {
        public panelAdmin()
        {
            InitializeComponent();
            lblpanelAdminAd.Content = ucLogin.UserInfo[4];
            lblpanelAdminSoyad.Content = ucLogin.UserInfo[5];
        }

        private void btnpanelAdminKitaplariListele_Click(object sender, RoutedEventArgs e)
        {
            ucCagir.ucGuncelle(gridAdmin, new ucAdminKitaplariListele());
            btnpanelAdminKitaplariListele.IsEnabled = false;
            btnpanelAdminKullanımdakiKitaplar.IsEnabled = true;
            btnpanelAdminKullanicilar.IsEnabled = true;
        }

        private void btnpanelAdminKullanımdakiKitaplar_Click(object sender, RoutedEventArgs e)
        {
            ucCagir.ucGuncelle(gridAdmin, new ucAdminKullanimdakiKitaplar());
            btnpanelAdminKitaplariListele.IsEnabled = true;
            btnpanelAdminKullanımdakiKitaplar.IsEnabled = false;
            btnpanelAdminKullanicilar.IsEnabled = true;
        }

        private void btnpanelAdminKullanicilar_Click(object sender, RoutedEventArgs e)
        {
            ucCagir.ucGuncelle(gridAdmin, new ucAdminKullanicilar());
            btnpanelAdminKitaplariListele.IsEnabled = true;
            btnpanelAdminKullanımdakiKitaplar.IsEnabled = true;
            btnpanelAdminKullanicilar.IsEnabled = false;
        }

        private void btnpanelAdminCikis_Click(object sender, RoutedEventArgs e)
        {
            panelLogin pl = new panelLogin();
            pl.Show();
            this.Close();
        }
    }
}
