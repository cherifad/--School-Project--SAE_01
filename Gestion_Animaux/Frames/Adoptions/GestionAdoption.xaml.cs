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
        List<int> indexModifs;
        public ObservableCollection<Adoptant> ListeAdoptant { get; set; }
        public ObservableCollection<Animal> ListeAnimal { get; set; }
        private static readonly Regex _regex = new Regex("[^0-9.-]+");
        /// <summary>
        /// Initialise la fenêtre et les différents objets et variables nécessaires à son fonctionnement.
        /// </summary>
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

            indexModifs = new List<int>();

            addAdoptantIn.ItemsSource = ApplicationData.listeAdoptants;

            addAnimalIn.ItemsSource = ApplicationData.listeAnimaux;

            this.DataContext = this;
        }
        /// <summary>
        /// Evenement correspondant au bouton d'ajout d'une adoption.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            Switch();
        }
        /// <summary>
        /// Permet de mettre à jour le tableau de données.
        /// </summary>
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

        /// <summary>
        /// Evenement lors de la sélection d'une ligne du tableau. Cela permet d'activer et désactiver le bouton supprimer
        /// en fonction de l'activation des modifications et de la sélection ou non d'un élement du tableau.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGAdoption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ActiveDataChange(DGAdoption.SelectedIndex);

            if (DGAdoption.SelectedIndex == -1 || (!modifs.IsOn))
                this.Supprimer.IsEnabled = false;
            else
                this.Supprimer.IsEnabled = true;
        }
        /// <summary>
        /// Evenement du bouton des modifications permettant de les activer ou non.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            Toggle();
        }
        /// <summary>
        /// Permet d'activer et désactiver les modifications du tableau.
        /// </summary>
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
        /// <summary>
        /// Evenement lors de la modification d'une cellule du tableau. Permet d'ajouter à la liste des modifications les modifications effectuées.
        /// Cela permet de pouvoir annuler ou valider les modifications.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGAdoption_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            modifsListe.Clear();

            foreach (var item in DGAdoption.Items)
            {
                modifsListe.Add((Adoption)item);
            }
        }
        /// <summary>
        /// Evenement lors du clique du bouton de validation des modifications. Permet de demander confirmation des modifications, et
        /// mettre à jour la base de données.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            string message = $"Vous êtes sur le point de valider {indexModifs.Count} modification(s).\nVoulez-vous continuer ?";
            string title = "Validation";
            var result = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (result == MessageBoxResult.Yes)
            {
                UpdateModifList(indexModifs);

                load.IsActive = true;
                foreach (var item in modifsListe)
                {
                    item.Update();
                }
                load.IsActive = false;

            }
        }
        /// <summary>
        /// Mets à jour la liste des modifications en ne gardant que les éléments réelement modifiés et modifiables.
        /// </summary>
        /// <param name="index">Liste contenant l'index des lignes modifiées.</param>
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
        
        /// <summary>
        /// Permet de modifier le texte sous le bouton d'activation des modifications. Le texte correspond aux informations
        /// concernant la ligne selectionnées.
        /// </summary>
        /// <param name="index">Index de la ligne selectionnée. Ne devant pas être nul ou négatif.</param>
        void ActiveDataChange(int index)
        {
            if (index != -1)
            {
                Adoption current = (Adoption)DGAdoption.Items[index];
                Adoptant adoptant = ApplicationData.listeAdoptants.Find(x => x.IdAdoptant == current.IdAdoptant);
                Animal animal = ApplicationData.listeAnimaux.Find(x => x.IdAnimal == current.IdAnimal);
                string nomAdoptant, nomAnimal;

                if (adoptant != null)
                    nomAdoptant = adoptant.NomAdoptant;
                else
                    nomAdoptant = "Adoptant inconnu";

                if (animal != null)
                    nomAnimal = animal.NomAnimal;
                else
                    nomAnimal = "Animal inconnu";


                activeData.Text = $"Nom Adoptant : {nomAdoptant}" +
                    $"\nNom Animal : {nomAnimal}" +
                    $"\nDate : {current.DateAdoption}" +
                    $"\nCommentaire : {current.CommentaireAdoption}";
            }
            else
            {
                activeData.Text = "Cliquez sur une adoption pour voir ses informations";
            }

        }
        /// <summary>
        /// Evenement correspondant à la fin de l'édition d'une ligne du tableau. Ajoute à la liste des modifications une ligne si
        /// elle a été modifiée.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGAdoption_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            indexModifs.Add(e.Row.GetIndex());

            e.Row.Background = Brushes.Orange;
        }
        /// <summary>
        /// Evenement du bouton supprimer une ligne. Permet de supprimer la ligne selectionnée dans le tableau de la base de données.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Permet de modifier l'apparence de la fenetre en passant du menu de consultation des données avec le tableau,
        /// au menu d'ajout de données avec le formulaire.
        /// </summary>
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
        /// <summary>
        /// Evenement de clique du bouton de validation du formulaire d'ajout. Permet de valider l'entree de données si le formulaire est complet,
        /// et ajouter à la base la nouvelle ligne.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

    }
}