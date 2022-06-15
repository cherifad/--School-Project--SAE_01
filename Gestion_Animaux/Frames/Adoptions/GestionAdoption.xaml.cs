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


namespace Gestion_Animaux.Frames.Adoptions
{
    /// <summary>
    /// Logique d'interaction pour EspeceFrame.xaml
    /// </summary>
    public partial class GestionAdoption : Page
    {
        public ObservableCollection<Adoption> ListeAdoption { get; set; }
        List<Adoption> modifsListe;
        public GestionAdoption()
        {
            InitializeComponent();
            ListeAdoption = new ObservableCollection<Adoption>();

            foreach (var item in ApplicationData.listeAdoptions)
            {
                ListeAdoption.Add(item);
            }

            modifsListe = new List<Adoption>();

            this.DataContext = this;
        }
        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            DGAdoption.Visibility = Visibility.Hidden;
            Label_Ajouter.Visibility = Visibility.Visible;
            Entrer_Espece.Visibility = Visibility.Visible;
            Button_Ajouter_Valider.Visibility = Visibility.Visible;
        }
        public void Update()
        {
            ApplicationData.UpdateAdoption();
            foreach (var item in ApplicationData.listeAdoptions)
            {
                ListeAdoption.Add(item);
            }
            DGAdoption.ItemsSource = ListeAdoption;
            this.DataContext = this;
        }

        private void Button_Ajouter_Valider_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(Entrer_Espece.Text))
            {
                Entrer_Espece.BorderBrush = Brushes.Red;
            }
            else
            {
                Adoption animal = new Adoption(Entrer_Espece.Text);
                animal.Create();

                DGAdoption.Visibility = Visibility.Visible;
                Label_Ajouter.Visibility = Visibility.Hidden;
                Entrer_Espece.Visibility = Visibility.Hidden;
                Button_Ajouter_Valider.Visibility = Visibility.Hidden;
                Entrer_Espece.Text = "";
                Entrer_Espece.BorderBrush = Brushes.Gray;
                this.Update();
            }
        }

        private void Page_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void DGAdoption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGAdoption.SelectedIndex == -1)
                this.Supprimer.IsEnabled = false;
            else
                this.Supprimer.IsEnabled = true;
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.modifs.IsChecked == true)
                DGAdoption.IsReadOnly = false;
            else
            {
                this.Supprimer.IsEnabled = false;
                DGAdoption.IsReadOnly = true;
            }
        }

        private void DGAdoption_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            modifsListe.Clear();

            foreach (var item in DGAdoption.Items)
            {
                modifsListe.Add((Adoption)item);
            }
        }

        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            List<Adoption> diff = new List<Adoption>();

            modifsListe.Sort(Adoption.CompareById);
            ApplicationData.listeAdoptions.Sort(Adoption.CompareById);

            List<int> index = new List<int>();


            for (int i = 0; i < modifsListe.Count; i++)
            {
                if (modifsListe[i] != ApplicationData.listeAdoptions[i])
                    index.Add(i);
            }



            //this.select.Content = modifsListe[0].IdAnimal.ToString();

        }

        private void Annuler_Modification_Click(object sender, RoutedEventArgs e)
        {
            this.Update();
        }
    }
}
