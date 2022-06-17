using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Logique d'interaction pour AdoptionFrame.xaml
    /// </summary>
    public partial class GestionAdoption : Page
    {
        public ObservableCollection<Adoption> ListeAdoption { get; set; }
        List<Adoption> modifsListe;
        List<int> indexMofifs;
        public ObservableCollection<Adoptant> ListeAdoptant { get; set; }
        public ObservableCollection<Animal> ListeAnimal { get; set; }
        private static readonly Regex _regex = new Regex("[^0-9.-]+");

        public GestionAdoption()
        {
            InitializeComponent();

            Toggle();

            ListeAdoption = new ObservableCollection<Adoption>();

            foreach (var item in ApplicationData.listeAdoptions)
            {
                ListeAdoption.Add(item);
            }

            modifsListe = new List<Adoption>();

            indexMofifs = new List<int>();

            addAdoptantIn.ItemsSource = ApplicationData.listeAdoptants;

            addAnimalIn.ItemsSource = ApplicationData.listeAnimaux;

            this.DataContext = this;
        }
        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            Switch();
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
        }


        private void DGAdoption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ActiveDataChange(DGAdoption.SelectedIndex);

            if (DGAdoption.SelectedIndex == -1 || (!modifs.IsOn))
                this.Supprimer.IsEnabled = false;
            else
                this.Supprimer.IsEnabled = true;
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            Toggle();
        }
        void Toggle()
        {
            if (this.modifs.IsOn)
            {
                DGAdoption.IsReadOnly = false;
                modifs.OnContent = "Modification activée";
                modifs.Foreground = Brushes.Green;
                Valider.IsEnabled = true;
                Annuler.IsEnabled = true;
            }
            else
            {
                modifs.OffContent = "Modification désactivée";
                modifs.Foreground = Brushes.Red;
                this.Supprimer.IsEnabled = false;
                DGAdoption.IsReadOnly = true;
                Valider.IsEnabled = false;
                Annuler.IsEnabled = false;
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
            Adoption newAdoption, oldAdoption;

            foreach (var item in index)
            {
                newAdoption = (Adoption)DGAdoption.Items[item];
                oldAdoption = ApplicationData.listeAdoptions.Find(x => x.IdAnimal == newAdoption.IdAnimal);

                if (oldAdoption != null /*&& oldAdoption != newAdoption*/)
                {
                    modifsListe.Add(newAdoption);
                }
            }
        }

        private void Annuler_Modification_Click(object sender, RoutedEventArgs e)
        {
            this.Update();
        }
        void ActiveDataChange(int index)
        {
            if (index != -1)
            {
                Adoption current = (Adoption)DGAdoption.Items[index];
                Adoptant adoptant = ApplicationData.listeAdoptants.Find(x => x.IdAdoptant == current.IdAdoptant);
                Animal animal = ApplicationData.listeAnimaux.Find(x => x.IdAnimal == current.IdAnimal);
                activeData.Text = $"Nom Adoptant : {adoptant.NomAdoptant}" +
                    $"\nNom Animal : {animal.NomAnimal}" +
                    $"\nDate : {current.DateAdoption}" +
                    $"\nCommentaire : {current.CommentaireAdoption}";
            }
            else
            {
                activeData.Text = "Cliquez sur une adoption pour voir ses informations";
            }

        }

        private void DGAdoption_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            indexMofifs.Add(e.Row.GetIndex());

            e.Row.Background = Brushes.Orange;
        }

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            int index = DGAdoption.SelectedIndex;
            if (index != -1)
            {
                Adoption current = (Adoption)DGAdoption.Items[index];
                Adoptant adoptant = ApplicationData.listeAdoptants.Find(x => x.IdAdoptant == current.IdAdoptant);
                Animal animal = ApplicationData.listeAnimaux.Find(x => x.IdAnimal == current.IdAnimal);
                string infos = $"Nom Adoptant : {adoptant.NomAdoptant}" +
                    $"\nNom Animal : {animal.NomAnimal}" +
                    $"\nDate : {current.DateAdoption}" +
                    $"\nCommentaire : {current.CommentaireAdoption}";

                string message = $"Vous êtes sur le point de supprimé : \n{infos} .\nVoulez-vous continuer ?";
                string title = "Validation";
                var result = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (result == MessageBoxResult.Yes)
                {
                    load.IsActive = true;
                    current.Delete();
                    ListeAdoption.RemoveAt(index);
                    DGAdoption.Items.Refresh();
                    load.IsActive = false;
                }
            }
            else
            {
                string message = $"Aucune adoption selectionée !";
                string title = "Validation";
                var result = MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void Switch()
        {
            if (form.Visibility == Visibility.Visible)
            {
                Ajouter.Content = "Ajouter une adoption";
                DGAdoption.Visibility = Visibility.Visible;
                form.Visibility = Visibility.Hidden;
            }
            else if (form.Visibility == Visibility.Hidden)
            {
                Ajouter.Content = "Retour";
                DGAdoption.Visibility = Visibility.Hidden;
                form.Visibility = Visibility.Visible;
            }
        }
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            Adoption newAdoption = new Adoption();
            newAdoption.IdAdoptant = ApplicationData.listeAdoptants[addAdoptantIn.SelectedIndex].IdAdoptant;
            newAdoption.IdAnimal = ApplicationData.listeAnimaux[addAnimalIn.SelectedIndex].IdAnimal;
            newAdoption.DateAdoption = DateTime.Parse(addDateIn.Text);
            newAdoption.CommentaireAdoption = addCommentaireIn.Text;

            string infos = $"\nAdoptant : {ApplicationData.listeAdoptants[addAdoptantIn.SelectedIndex].IdAdoptant}" +
                    $"\nAnimal : {ApplicationData.listeAnimaux[addAnimalIn.SelectedIndex].IdAnimal}" +
                    $"\nDate : {newAdoption.DateAdoption}" +
                    $"\nCommentaire : {newAdoption.CommentaireAdoption}";

            string message = $"Vous êtes sur le point d'ajouté : \n{infos} .\nVoulez-vous continuer ?";
            string title = "Validation";
            var result = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (result == MessageBoxResult.Yes)
            {
                load.IsActive = true;
                newAdoption.Create();
                ListeAdoption.Add(newAdoption);
                DGAdoption.Items.Refresh();
                Switch();
                load.IsActive = false;
            }


        }
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
    }
}