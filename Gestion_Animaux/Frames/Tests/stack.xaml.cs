using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Gestion_Animaux.Frames.Tests
{
    /// <summary>
    /// Logique d'interaction pour stack.xaml
    /// </summary>
    public partial class stack : Page

    {
        public ObservableCollection<Animal> ListeAnimaux { get; set; }
        List<Animal> modifsListe;
        List<int> indexMofifs;

        public stack()
        {
            InitializeComponent();

            Toggle();

            ListeAnimaux = new ObservableCollection<Animal>();

            foreach (var item in ApplicationData.listeAnimaux)
            {
                ListeAnimaux.Add(item);
            }

            modifsListe = new List<Animal>();

            indexMofifs = new List<int>();

            this.DataContext = this;

        }

        private void DGAnimaux_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ActiveDataChange(DGAnimaux.SelectedIndex);

            if ((DGAnimaux.SelectedIndex == -1) || (modifs.IsChecked == false))
                this.Supprimer.IsEnabled = false;
            else
                this.Supprimer.IsEnabled = true;
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            Toggle();
        }

        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            string message = $"Vous êtes sur le point de valider {indexMofifs.Count} modification(s).\nVoulez-vous continuer ?";
            string title = "Validation";
            var result = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (result == MessageBoxResult.Yes)
            {
                UpdateModifList(indexMofifs);

                load.IsActive = true;
                foreach (var item in modifsListe)
                {
                    item.Update();
                }
                load.IsActive = false;
            }

        }

        void UpdateModifList(List<int> index)
        {
            Animal newAnimal, oldAnimal;

            foreach (var item in index)
            {
                newAnimal = (Animal)DGAnimaux.Items[item];
                oldAnimal = ApplicationData.listeAnimaux.Find(x => x.IdAnimal == newAnimal.IdAnimal);

                if (oldAnimal != null /*&& oldAnimal != newAnimal*/)
                {
                    modifsListe.Add(newAnimal);
                }
            }
        }

        private void DGAnimaux_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            indexMofifs.Add(e.Row.GetIndex());

            e.Row.Background = Brushes.Orange;
        }

        void Toggle()
        {
            if (this.modifs.IsChecked == true)
            {
                DGAnimaux.IsReadOnly = false;
                modifsTexte.Content = "Modification activée";
                modifsTexte.Foreground = Brushes.Green;
                Valider.IsEnabled = true;
                Annuler.IsEnabled = true;
            }
            else
            {
                modifsTexte.Content = "Modification désactivée";
                modifsTexte.Foreground = Brushes.Red;
                this.Supprimer.IsEnabled = false;
                DGAnimaux.IsReadOnly = true;
                Valider.IsEnabled = false;
                Annuler.IsEnabled = false;
            }
        }

        void ActiveDataChange(int index)
        {
            Animal current = (Animal)DGAnimaux.Items[index];
            TypeAnimal espece = ApplicationData.listeTypeAnimal.Find(x => x.IdType == current.TypeAnimal);
            activeData.Text = $"N° unique : {current.IdAnimal}" +
                $"\nEspèce : {espece.LibelleType}" +
                $"\nNom : {current.NomAnimal}" +
                $"\nTaille : {current.TailleAnimal} cm" +
                $"\nPoids : {current.PoidsAnimal} kg";
        }
    }
}
