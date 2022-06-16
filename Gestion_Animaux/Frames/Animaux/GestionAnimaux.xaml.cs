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
        List<int> indexMofifs;
        bool canSet = true;
        private List<Animal> startupList;

        public List<Animal> StartupList
        {
            get { return startupList; }
            set 
            { 
                if (canSet)
                    startupList = value; 
            }
        }


        public GestionAnimaux()
        {
            InitializeComponent();

            ListeAnimaux = new ObservableCollection<Animal>();

            StartupList = new List<Animal>();

            StartupList = ApplicationData.listeAnimaux;

            foreach (var item in ApplicationData.listeAnimaux)
            {
                ListeAnimaux.Add(item);
            }

            canSet = false;

            modifsListe = new List<Animal>();

            indexMofifs = new List<int>();

            this.DataContext = this;

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
                DGAnimaux.IsReadOnly = false;
            else
            {
                this.Supprimer.IsEnabled = false;
                DGAnimaux.IsReadOnly = true;
            }
        }

        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            UpdateModifList(indexMofifs);

            foreach (var item in modifsListe)
            {
                item.Update();
            }
        }

        void UpdateModifList(List<int> index)
        {
            Animal newAnimal, oldAnimal;

            foreach (var item in index)
            {
                newAnimal = (Animal)DGAnimaux.Items[item];
                oldAnimal = StartupList.Find(x => x.IdAnimal == newAnimal.IdAnimal);

                if (oldAnimal != null /*&& oldAnimal != newAnimal*/)
                {
                    modifsListe.Add(newAnimal);
                }
            }                
        }

        private void DGAnimaux_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            indexMofifs.Add(e.Row.GetIndex());
        }
    }
}
