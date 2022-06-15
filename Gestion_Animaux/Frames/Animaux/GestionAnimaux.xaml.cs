using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace Gestion_Animaux.Frames.Animaux
{
    /// <summary>
    /// Logique d'interaction pour GestionAnimaux.xaml
    /// </summary>
    public partial class GestionAnimaux : Page
    {
        public ObservableCollection<Animal> ListeAnimaux { get; set; }
        List<Animal> modifsListe;

        public GestionAnimaux()
        {
            InitializeComponent();

            ListeAnimaux = new ObservableCollection<Animal>();

            foreach (var item in ApplicationData.listeAnimaux)
            {
                ListeAnimaux.Add(item);
            }

            modifsListe = new List<Animal>();

            this.DataContext = this;

        }

        private void DGAnimaux_CurrentCellChanged(object sender, EventArgs e)
        {
            this.select.Content = DGAnimaux.SelectedIndex.ToString();
        }

        private void Page_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void DGAnimaux_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGAnimaux.SelectedIndex == -1)
                this.Supprimer.IsEnabled = false;
            else
                this.Supprimer.IsEnabled = true;
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.modifs.IsChecked == true)
                DGAnimaux.IsReadOnly = true;
            else
            {
                this.Supprimer.IsEnabled = false;
                DGAnimaux.IsReadOnly = false;
            }
        }

        private void DGAnimaux_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            modifsListe.Clear();

            foreach (var item in DGAnimaux.Items)
            {
                modifsListe.Add((Animal)item);
            }
        }

        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            List<Animal> diff = new List<Animal>();

            modifsListe.Sort(Animal.CompareById);
            ApplicationData.listeAnimaux.Sort(Animal.CompareById);

            List<int> index = new List<int>();


            for (int i = 0; i < modifsListe.Count; i++)
            {
                if (modifsListe[i] != ApplicationData.listeAnimaux[i])
                    index.Add(i);
            }



            //this.select.Content = modifsListe[0].IdAnimal.ToString();

        }
    }
}
