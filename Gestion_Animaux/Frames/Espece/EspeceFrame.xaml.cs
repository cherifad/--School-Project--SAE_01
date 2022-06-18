﻿using System;
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


namespace Gestion_Animaux.Frames.Espece
{
    /// <summary>
    /// Logique d'interaction pour EspeceFrame.xaml
    /// </summary>
    public partial class EspeceFrame : Page
    {
        public ObservableCollection<TypeAnimal> ListeTypeAnimal { get; set; }
        List<TypeAnimal> modifsListe;
        List<int> indexMofifs;
        private static readonly Regex _regex = new Regex("[^0-9.-]+");
        public EspeceFrame()
        {
            InitializeComponent();

            Toggle();

            ListeTypeAnimal = new ObservableCollection<TypeAnimal>();

            foreach (var item in ApplicationData.listeTypeAnimal)
            {
                ListeTypeAnimal.Add(item);
            }

            modifsListe = new List<TypeAnimal>();

            indexMofifs = new List<int>();

            this.DataContext = this;
        }
        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            Switch();
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

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            TypeAnimal newTypeAnimal = new TypeAnimal();
            newTypeAnimal.LibelleType = addEspeceIn.Text;

            string infos = $"\nNom : {newTypeAnimal.LibelleType}";

            string message = $"Vous êtes sur le point d'ajouté : \n{infos} .\nVoulez-vous continuer ?";
            string title = "Validation";
            var result = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (result == MessageBoxResult.Yes)
            {
                load.IsActive = true;
                newTypeAnimal.Create();
                ListeTypeAnimal.Add(newTypeAnimal);
                DGTypeAnimal.Items.Refresh();
                Switch();
                load.IsActive = false;
            }
        }
        

        private void DGTypeAnimal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ActiveDataChange(DGTypeAnimal.SelectedIndex);

            if (DGTypeAnimal.SelectedIndex == -1 || (!modifs.IsOn))
                this.Supprimer.IsEnabled = false;
            else
                this.Supprimer.IsEnabled = true;
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            Toggle();
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
            TypeAnimal newTypeAnimal, oldTypeAnimal;

            foreach (var item in index)
            {
                newTypeAnimal = (TypeAnimal)DGTypeAnimal.Items[item];
                oldTypeAnimal = ApplicationData.listeTypeAnimal.Find(x => x.IdType == newTypeAnimal.IdType);

                if (oldTypeAnimal != null /*&& oldAnimal != newAnimal*/)
                {
                    modifsListe.Add(newTypeAnimal);
                }
            }
        }
        void ActiveDataChange(int index)
        {
            if (index != -1)
            {
                TypeAnimal current = (TypeAnimal)DGTypeAnimal.Items[index];
                activeData.Text = $"N° unique : {current.IdType}" +
                    $"\nNom : {current.LibelleType}";
            }
            else
            {
                activeData.Text = "Cliquez sur un type d'animal pour voir ses informations";
            }

        }

        private void Annuler_Modification_Click(object sender, RoutedEventArgs e)
        {
            this.Update();
        }

        void Toggle()
        {
            if (this.modifs.IsOn)
            {
                DGTypeAnimal.IsReadOnly = false;
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
                DGTypeAnimal.IsReadOnly = true;
                Valider.IsEnabled = false;
                Annuler.IsEnabled = false;
            }
        }
        private void Switch()
        {
            if (form.Visibility == Visibility.Visible)
            {
                Ajouter.Content = "Ajouter une espèce";
                DGTypeAnimal.Visibility = Visibility.Visible;
                form.Visibility = Visibility.Hidden;
            }
            else if (form.Visibility == Visibility.Hidden)
            {
                Ajouter.Content = "Retour";
                DGTypeAnimal.Visibility = Visibility.Hidden;
                form.Visibility = Visibility.Visible;
            }
        }

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            int index = DGTypeAnimal.SelectedIndex;
            if (index != -1)
            {
                TypeAnimal current = (TypeAnimal)DGTypeAnimal.Items[index];
                string infos = $"N° unique : {current.IdType}" +
                    $"\nNom : {current.LibelleType}";

                string message = $"Vous êtes sur le point de supprimé : \n{infos} .\nVoulez-vous continuer ?";
                string title = "Validation";
                var result = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (result == MessageBoxResult.Yes)
                {
                    load.IsActive = true;
                    current.Delete();
                    ListeTypeAnimal.RemoveAt(index);
                    DGTypeAnimal.Items.Refresh();
                    load.IsActive = false;
                }
            }
            else
            {
                string message = $"Aucun type d'animal selectioné !";
                string title = "Validation";
                var result = MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DGTypeAnimal_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            indexMofifs.Add(e.Row.GetIndex());

            e.Row.Background = Brushes.Orange;
        }
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void addEspeceIn_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsTextAllowed(e.Text);
        }
    }
}
