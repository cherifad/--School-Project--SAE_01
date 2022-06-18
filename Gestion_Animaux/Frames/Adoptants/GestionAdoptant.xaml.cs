using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace Gestion_Animaux.Frames.Adoptants
{
    /// <summary>
    /// Logique d'interaction pour GestionAnimaux.xaml
    /// </summary>
    public partial class GestionAdoptant : Page
    {
        public ObservableCollection<Adoptant> ListeAdoptant { get; set; }
        List<Adoptant> modifsListe;
        List<int> indexMofifs;
        public ObservableCollection<TypeAnimal> ListeTypeAnimal { get; set; }
        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text


        private TypeAnimal typeAnimal;
        public TypeAnimal TypeAnimal
        {
            get { return typeAnimal; }
            set { typeAnimal = value; }
        }
        public GestionAdoptant()
        {
            InitializeComponent();

            Toggle();

            ListeAdoptant = new ObservableCollection<Adoptant>();

            foreach (var item in ApplicationData.listeAdoptants)
            {
                ListeAdoptant.Add(item);
            }

            modifsListe = new List<Adoptant>();

            indexMofifs = new List<int>();

            this.DataContext = this;

        }

        private void DGAdoptant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ActiveDataChange(DGAdoptant.SelectedIndex);

            if ((DGAdoptant.SelectedIndex == -1) || (!modifs.IsOn))
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
            Adoptant newAdoptant, oldAdoptant;

            foreach (var item in index)
            {
                newAdoptant = (Adoptant)DGAdoptant.Items[item];
                oldAdoptant = ApplicationData.listeAdoptants.Find(x => x.IdAdoptant == newAdoptant.IdAdoptant);

                if (oldAdoptant != null /*&& oldAnimal != newAnimal*/)
                {
                    modifsListe.Add(newAdoptant);
                }
            }
        }

        private void DGAdoptant_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            indexMofifs.Add(e.Row.GetIndex());

            e.Row.Background = Brushes.Orange;
        }

        void Toggle()
        {
            if (this.modifs.IsOn)
            {
                DGAdoptant.IsReadOnly = false;
                modifs.OnContent = "Modifications activées";
                modifs.Foreground = Brushes.Green;
                Valider.IsEnabled = true;
                Annuler.IsEnabled = true;
            }
            else
            {
                modifs.OffContent = "Modifications désactivées";
                modifs.Foreground = Brushes.Red;
                this.Supprimer.IsEnabled = false;
                DGAdoptant.IsReadOnly = true;
                Valider.IsEnabled = false;
                Annuler.IsEnabled = false;
            }
        }

        void ActiveDataChange(int index)
        {
            if (index != -1)
            {
                Adoptant current = (Adoptant)DGAdoptant.Items[index];
                activeData.Text = $"N° unique : {current.IdAdoptant}" +
                    $"\nNom : {current.NomAdoptant}" +
                    $"\nPrénom : {current.PrenomAdoptant}" +
                    $"\nTéléphone : {current.TelAdoptant}" +
                    $"\nEmail : {current.MailAdoptant}";
            }
            else
            {
                activeData.Text = "Cliquez sur un adoptant pour voir ses informations";
            }

        }

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            int index = DGAdoptant.SelectedIndex;
            if (index != -1)
            {
                Adoptant current = (Adoptant)DGAdoptant.Items[index];
                string infos = $"N° unique : {current.IdAdoptant}" +
                    $"\nNom : {current.NomAdoptant}" +
                    $"\nPrénom : {current.PrenomAdoptant}" +
                    $"\nTéléphone : {current.TelAdoptant}" +
                    $"\nEmail : {current.MailAdoptant}";

                string message = $"Vous êtes sur le point de supprimer : \n{infos} .\nVoulez-vous continuer ?";
                string title = "Validation";
                var result = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (result == MessageBoxResult.Yes)
                {
                    load.IsActive = true;
                    current.Delete();
                    ListeAdoptant.RemoveAt(index);
                    DGAdoptant.Items.Refresh();
                    load.IsActive = false;
                }
            }
            else
            {
                string message = $"Aucun animal selectionné !";
                string title = "Validation";
                var result = MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            Switch();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            addNomIn.BorderBrush = Brushes.Gray;
            addPrenomIn.BorderBrush = Brushes.Gray;
            addTelIn.BorderBrush = Brushes.Gray;
            addMailIn.BorderBrush = Brushes.Gray;

            if (String.IsNullOrEmpty(addNomIn.Text)||String.IsNullOrEmpty(addPrenomIn.Text)||String.IsNullOrEmpty(addTelIn.Text)||String.IsNullOrEmpty(addMailIn.Text))
            {
                if (String.IsNullOrEmpty(addNomIn.Text))
                    addNomIn.BorderBrush = Brushes.Red;

                if (String.IsNullOrEmpty(addPrenomIn.Text))
                    addPrenomIn.BorderBrush = Brushes.Red;

                if (String.IsNullOrEmpty(addTelIn.Text))
                    addTelIn.BorderBrush = Brushes.Red;

                if (String.IsNullOrEmpty(addMailIn.Text))
                    addMailIn.BorderBrush = Brushes.Red;
            }
            else
            {
                Adoptant newAdoptant = new Adoptant();
                newAdoptant.NomAdoptant = addNomIn.Text;
                newAdoptant.PrenomAdoptant = addPrenomIn.Text;
                newAdoptant.TelAdoptant = addTelIn.Text;
                newAdoptant.MailAdoptant = addMailIn.Text;

                string infos = $"\nNom : {newAdoptant.NomAdoptant}" +
                        $"\nPrénom : {newAdoptant.PrenomAdoptant}" +
                        $"\nTéléphone : {newAdoptant.TelAdoptant}" +
                        $"\nEmail : {newAdoptant.MailAdoptant}";

                string message = $"Vous êtes sur le point d'ajouter : \n{infos} .\nVoulez-vous continuer ?";
                string title = "Validation";
                var result = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (result == MessageBoxResult.Yes)
                {
                    load.IsActive = true;
                    newAdoptant.Create();
                    ListeAdoptant.Add(newAdoptant);
                    DGAdoptant.Items.Refresh();
                    Switch();
                    load.IsActive = false;
                }

            }
        }
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void addNomIn_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsTextAllowed(e.Text);
        }

        private void addPrenomIn_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsTextAllowed(e.Text);
        }

        private void addTelIn_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void addMailIn_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void Switch()
        {
            if (form.Visibility == Visibility.Visible)
            {
                Ajouter.Content = "Ajouter un adoptant";
                DGAdoptant.Visibility = Visibility.Visible;
                form.Visibility = Visibility.Hidden;
            }
            else if (form.Visibility == Visibility.Hidden)
            {
                Ajouter.Content = "Retour";
                DGAdoptant.Visibility = Visibility.Hidden;
                form.Visibility = Visibility.Visible;
            }
        }

    }
}
