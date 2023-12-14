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
using System.Xml.Linq;

namespace Etlap
{
    /// <summary>
    /// Interaction logic for UjEtel.xaml
    /// </summary>
    public partial class UjEtel : Window
    {
        private EtlapSource etlapsource;
        public UjEtel(EtlapSource etlapSource)
        {
            InitializeComponent();
            this.etlapsource = etlapSource;

        }


        private void btAdd(object sender, RoutedEventArgs e)
        {
            try
            {
                Etel etel = CreateEtel();
                if (etlapsource.Create(etel))
                {
                    MessageBox.Show("Sikeres hozzáadás");
                    tnev.Text = "";
                    tar.Text = "";
                    tkategoria.Text = "";
                    tleiras.Text = "";
                }
                else
                {
                    MessageBox.Show("Hiba történt a hozzáadás során");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private Etel CreateEtel()
        {
            string nev = tnev.Text.Trim();
            string leiras = tleiras.Text.Trim();
            string artext = tar.Text.Trim();
            string kategoria = tkategoria.Text.Trim();

            if (string.IsNullOrEmpty(nev))
            {
                throw new Exception("Név megadása kötelező");
            }

            if (string.IsNullOrEmpty(artext))
            {
                throw new Exception("Ár megadása kötelező");
            }
            if (!int.TryParse(artext, out int ar))
            {
                throw new Exception("Ár csak szám lehet");
            }

            if (string.IsNullOrEmpty(kategoria))
            {
                throw new Exception("Kategoria megadása kötelező");
            }

            if (ar < 5)
            {
                throw new Exception("Az Árának legalább 5 forintnak kell lennie");
            }

            Etel etel = new Etel();
            etel.Nev = nev;
            etel.Leiras= leiras;
            etel.Ar = ar;
            etel.Kategoria = kategoria;
            return etel;
        }
    }
}
