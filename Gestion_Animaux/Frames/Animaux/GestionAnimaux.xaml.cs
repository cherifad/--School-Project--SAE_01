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
        public ObservableCollection<TypeAnimal> ListeTypeAnimal { get; set; }
        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text


        private TypeAnimal typeAnimal;
        public TypeAnimal TypeAnimal
        {
            get { return typeAnimal; }
            set { typeAnimal = value; }
        }
        public GestionAnimaux()
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

            addEspeceIn.ItemsSource = ApplicationData.listeTypeAnimal;

            this.DataContext = this;

        }

        private void DGAnimaux_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ActiveDataChange(DGAnimaux.SelectedIndex);

            if ((DGAnimaux.SelectedIndex == -1) || (!modifs.IsOn))
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

            if(result == MessageBoxResult.Yes)
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
            if (this.modifs.IsOn)
            {
                DGAnimaux.IsReadOnly = false;
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
                DGAnimaux.IsReadOnly = true;
                Valider.IsEnabled = false;
                Annuler.IsEnabled = false;
            }
        }

        void ActiveDataChange(int index)
        {
            if(index != -1)
            {
                Animal current = (Animal)DGAnimaux.Items[index];
                TypeAnimal espece = ApplicationData.listeTypeAnimal.Find(x => x.IdType == current.TypeAnimal);
                activeData.Text = $"N° unique : {current.IdAnimal}" +
                    $"\nEspèce : {espece.LibelleType}" +
                    $"\nNom : {current.NomAnimal}" +
                    $"\nTaille : {current.TailleAnimal} cm" +
                    $"\nPoids : {current.PoidsAnimal} kg";
            }
            else
            {
                activeData.Text = "Cliquez sur un animal pour voir ses informations";
            }
            
        }

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            int index = DGAnimaux.SelectedIndex;
            if (index != -1)
            {
                Animal current = (Animal)DGAnimaux.Items[index];
                TypeAnimal espece = ApplicationData.listeTypeAnimal.Find(x => x.IdType == current.TypeAnimal);
                string infos = $"N° unique : {current.IdAnimal}" +
                    $"\nEspèce : {espece.LibelleType}" +
                    $"\nNom : {current.NomAnimal}" +
                    $"\nTaille : {current.TailleAnimal} cm" +
                    $"\nPoids : {current.PoidsAnimal} kg";

                string message = $"Vous êtes sur le point de supprimé : \n{infos} .\nVoulez-vous continuer ?";
                string title = "Validation";
                var result = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (result == MessageBoxResult.Yes)
                {
                    load.IsActive = true;
                    current.Delete();
                    ListeAnimaux.RemoveAt(index);
                    DGAnimaux.Items.Refresh();
                    load.IsActive = false;
                }
            } else
            {
                string message = $"Aucun animal selectioné !";
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
            Animal newAnimal = new Animal();
            newAnimal.TypeAnimal = ApplicationData.listeTypeAnimal[addEspeceIn.SelectedIndex].IdType;
            newAnimal.PoidsAnimal = double.Parse(addPoidsIn.Text);
            newAnimal.TailleAnimal = int.Parse(addTailleIn.Text);
            newAnimal.NomAnimal = addNomIn.Text;

            string infos = $"\nEspèce : {ApplicationData.listeTypeAnimal[addEspeceIn.SelectedIndex].LibelleType}" +
                    $"\nNom : {newAnimal.NomAnimal}" +
                    $"\nTaille : {newAnimal.TailleAnimal} cm" +
                    $"\nPoids : {newAnimal.PoidsAnimal} kg";

            string message = $"Vous êtes sur le point d'ajouté : \n{infos} .\nVoulez-vous continuer ?";
            string title = "Validation";
            var result = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (result == MessageBoxResult.Yes)
            {
                load.IsActive = true;
                newAnimal.Create();
                ListeAnimaux.Add(newAnimal);
                DGAnimaux.Items.Refresh();
                Switch();
                load.IsActive = false;
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

        private void addPoidsIn_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void addTailleIn_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void Switch()
        {
            if (form.Visibility == Visibility.Visible)
            {
                Ajouter.Content = "Ajouter un animal";
                DGAnimaux.Visibility = Visibility.Visible;
                form.Visibility = Visibility.Hidden;
            }
            else if (form.Visibility == Visibility.Hidden)
            {
                Ajouter.Content = "Retour";
                DGAnimaux.Visibility = Visibility.Hidden;
                form.Visibility = Visibility.Visible;
            }
        }
    }
}
