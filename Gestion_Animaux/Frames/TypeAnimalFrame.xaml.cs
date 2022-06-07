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
    /// Logique d'interaction pour TypeAnimal.xaml
    /// </summary>
    public partial class TypeAnimalFrame : Page
    {
        public TypeAnimalFrame()
        {
            
            InitializeComponent();
            listViewTypeAnimal.ItemsSource = ApplicationData.listeTypeAnimal;

        }
    }
}
