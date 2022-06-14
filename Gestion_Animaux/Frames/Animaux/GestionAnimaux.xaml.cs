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

namespace Gestion_Animaux.Frames.Animaux
{
    /// <summary>
    /// Logique d'interaction pour GestionAnimaux.xaml
    /// </summary>
    public partial class GestionAnimaux : Page
    {
        public ObservableCollection<Animal> ListeAnimaux { get; set; }

        public GestionAnimaux()
        {
            InitializeComponent();

            ListeAnimaux = new ObservableCollection<Animal>();

            foreach (var item in ApplicationData.listeAnimaux)
            {
                ListeAnimaux.Add(item);
            }

            this.DataContext = this;

        }

        private void DGAnimaux_CurrentCellChanged(object sender, EventArgs e)
        {
            DGAnimaux.Focus();
            this.select.Content = DGAnimaux.SelectedItems.Count.ToString();

            if (DGAnimaux.SelectedCells.Count < 0)
                this.Supprimer.IsEnabled = true;
        }

        private void Page_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }
    }
}
