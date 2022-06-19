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

namespace Gestion_Animaux.Frames.Main
{
    /// <summary>
    /// Logique d'interaction pour Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();

            nbAdoptants.Content = ApplicationData.listeAdoptants.Count.ToString();  
            nbAdoptions.Content = ApplicationData.listeAdoptions.Count.ToString();  
            nbAnimaux.Content = ApplicationData.listeAnimaux.Count.ToString();  
            nbEspeces.Content = ApplicationData.listeTypeAnimal.Count.ToString();

        }
    }
}
