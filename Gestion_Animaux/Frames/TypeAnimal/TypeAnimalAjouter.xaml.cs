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

namespace Gestion_Animaux.Frames.TypeAnimal
{
    /// <summary>
    /// Logique d'interaction pour TypeAnimalAjouter.xaml
    /// </summary>
    public partial class TypeAnimalAjouter : Window
    {
        public TypeAnimalAjouter()
        {
            InitializeComponent();
        }

        private void Button_Ajouter_Click(object sender, RoutedEventArgs e)
        {
            Gestion_Animaux.TypeAnimal animal = new Gestion_Animaux.TypeAnimal(Entrer_Espece.Text);
            animal.Create();
            
            this.Close();

        }

        
    }
}
