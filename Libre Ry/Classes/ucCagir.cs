using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Libre_Ry.Classes
{
    class ucCagir
    {
        public static void ucGuncelle(Grid grd, UserControl uc)
        {
            if (grd.Children.Count > 0)
            {
                grd.Children.Clear(); //Ekrandaki grid silinir
                grd.Children.Add(uc); //Seçilen gridi ekrana geçirir
            }
            else { grd.Children.Add(uc); } //Grid ekranının boş olmasına izin vermiyorum
        }

    }
}
