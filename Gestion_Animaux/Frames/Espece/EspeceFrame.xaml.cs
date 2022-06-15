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


namespace Gestion_Animaux.Frames.Espece
{
    /// <summary>
    /// Logique d'interaction pour EspeceFrame.xaml
    /// </summary>
    public partial class EspeceFrame : Page
    {
        public ObservableCollection<TypeAnimal> ListeTypeAnimal { get; set; }
        List<TypeAnimal> modifsListe;
        public EspeceFrame()
        {
            InitializeComponent();
            ListeTypeAnimal = new ObservableCollection<TypeAnimal>();

            foreach (var item in ApplicationData.listeTypeAnimal)
            {
                ListeTypeAnimal.Add(item);
            }

            modifsListe = new List<TypeAnimal>();

            this.DataContext = this;
        }
        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            DGTypeAnimal.Visibility = Visibility.Hidden;
            Label_Ajouter.Visibility = Visibility.Visible;
            Entrer_Espece.Visibility = Visibility.Visible;
            Button_Ajouter_Valider.Visibility = Visibility.Visible;
        }
        public void Update()
        {
            ApplicationData.UpdateTypeAnimal();
            foreach (var item in ApplicationData.listeTypeAnimal)
            {
                ListeTypeAnimal.Add(item);
            }
            DGTypeAnimal.ItemsSource = ListeTypeAnimal;
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
                TypeAnimal animal = new TypeAnimal(Entrer_Espece.Text);
                animal.Create();
                
                DGTypeAnimal.Visibility = Visibility.Visible;
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

        private void DGTypeAnimal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGTypeAnimal.SelectedIndex == -1)
                this.Supprimer.IsEnabled = false;
            else
                this.Supprimer.IsEnabled = true;
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.modifs.IsChecked == true)
                DGTypeAnimal.IsReadOnly = false;
            else
            {
                this.Supprimer.IsEnabled = false;
                DGTypeAnimal.IsReadOnly = true;
            }
        }

        private void DGTypeAnimal_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            modifsListe.Clear();

            foreach (var item in DGTypeAnimal.Items)
            {
                modifsListe.Add((TypeAnimal)item);
            }
        }

        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            List<TypeAnimal> diff = new List<TypeAnimal>();

            modifsListe.Sort(TypeAnimal.CompareById);
            ApplicationData.listeTypeAnimal.Sort(TypeAnimal.CompareById);

            List<int> index = new List<int>();


            for (int i = 0; i < modifsListe.Count; i++)
            {
                if (modifsListe[i] != ApplicationData.listeTypeAnimal[i])
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
