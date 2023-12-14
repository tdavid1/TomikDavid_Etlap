using Org.BouncyCastle.Utilities;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Etlap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EtlapSource etlapsource;
        List<Etel> etelek;
        public MainWindow()
        {
            InitializeComponent();
            this.etlapsource = new EtlapSource();
            tombletrehozas();
            foreach (var item in etelek)
            {
                if (EtlapTable.SelectedItem == item)
                {
                    leiras.Text = item.Leiras.ToString();
                }
            }
            
        }
        private void tombletrehozas()
        {
            EtlapTable.ItemsSource = etlapsource.GetAllTable();
            etelek = etlapsource.GetAllTable();
        }

        private void Uj_Felvetele(object sender, RoutedEventArgs e)
        {
            UjEtel ujetel = new UjEtel(etlapsource);
            ujetel.Closed += (_, _) =>
            {
               tombletrehozas();
            };
            ujetel.ShowDialog();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            Etel selected = EtlapTable.SelectedItem as Etel;
            if (selected == null)
            {
                MessageBox.Show("Törléshez előbb válasszon ki egy Ételt!");
                return;
            }
            MessageBoxResult selectedButton =
                MessageBox.Show($"Biztos, hogy törölni szeretné az alábbi Ételt: {selected.Nev}?",
                    "Biztos?", MessageBoxButton.YesNo);
            if (selectedButton == MessageBoxResult.Yes)
            {
                if (etlapsource.Delete(selected.Id))
                {
                    MessageBox.Show("Sikeres törlés!");
                }
                else
                {
                    MessageBox.Show("Hiba történt a törlés során, a megadott elem nem található");
                }
                tombletrehozas();
            }
        }

        private void a_emeles(object sender, RoutedEventArgs e)
        {
            if(int.Parse(adott.Text)<50 || int.Parse(adott.Text) > 3000)
            {
                MessageBox.Show("Az áremelésnek nagyobbnak kell lennie mint 50 ftnál vagy kissebbnekk mint 3000 ftnál");
            }
            else
            {
                Etel selected = EtlapTable.SelectedItem as Etel;
                if (selected == null)
                {
                    foreach (var item in etelek)
                    {
                        int ar = item.Ar + int.Parse(adott.Text);
                        etlapsource.Aremeles(item.Id, ar);
                    }
                }
                else
                {
                    int ar = selected.Ar + int.Parse(adott.Text);
                    etlapsource.Aremeles(selected.Id, ar);
                }
                tombletrehozas();
            }
        }

        private void sz_emeles(object sender, RoutedEventArgs e)
        {
            if (int.Parse(szazalek.Text) < 5 || int.Parse(szazalek.Text) > 50)
            {
                MessageBox.Show("Az áremelésnek nagyobbnak kell lennie mint 5%-nál vagy kissebbnekk mint 50%-nál");
            }
            else
            {
                Etel selected = EtlapTable.SelectedItem as Etel;
                if (selected == null)
                {
                    foreach (var item in etelek)
                    {
                        double seged = 1 + (double.Parse(szazalek.Text) / 100);
                        double ar = item.Ar * seged;
                        etlapsource.Aremeles(item.Id, ar);
                    }
                    
                }
                else
                {
                    double seged = 1 + (double.Parse(szazalek.Text) / 100);
                    double ar = selected.Ar * seged;
                    etlapsource.Aremeles(selected.Id, ar);
                }
                tombletrehozas();
            }
        }
    }
}