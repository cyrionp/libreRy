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
using System.Data.SQLite;

namespace Libre_Ry
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class panelLogin : Window
    {
        public panelLogin()
        {
            InitializeComponent();
            ucCagir.ucGuncelle(gridLogin, new ucLogin());
            btnpanelLoginucLogin.Visibility = Visibility.Hidden;
        }

        private void btnpanelLoginucKayit_Click(object sender, RoutedEventArgs e)
        {
            ucCagir.ucGuncelle(gridLogin, new ucKayit());
            btnpanelLoginucKayit.Visibility = Visibility.Hidden;
            btnpanelLoginucLogin.Visibility = Visibility.Visible;
        }

        private void btnpanelLoginucLogin_Click(object sender, RoutedEventArgs e)
        {
            ucCagir.ucGuncelle(gridLogin, new ucLogin());
            btnpanelLoginucKayit.Visibility = Visibility.Visible;
            btnpanelLoginucLogin.Visibility = Visibility.Hidden;
        }
    }
}
