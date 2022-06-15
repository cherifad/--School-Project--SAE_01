using System;
using System.Collections.Generic;
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

namespace Gestion_Animaux
{
    /// <summary>
    /// Logique d'interaction pour AdoptionFrame.xaml
    /// </summary>
    public partial class AdoptionFrame : Page
    {
        public AdoptionFrame()
        {
            InitializeComponent();
            listeAdopt.ItemsSource = ApplicationData.listeAdoptions;
            this.DataContext = this;
            
        }

        private void suppr_Click(object sender, RoutedEventArgs e)
        {
            ApplicationData.listeAdoptions.Remove((Adoption)listeAdopt.SelectedItem);
            listeAdopt.Items.Refresh();
            //Gestion_Animaux.Adoption adoption = new Adoption(listeAdopt.SelectedValue);
            
        }
    }
}
