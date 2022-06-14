using Gestion_Animaux.Frames.TypeAnimal;
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

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            listViewTypeAnimal.Visibility = Visibility.Hidden;
            Label_Ajouter.Visibility = Visibility.Visible;
            Entrer_Espece.Visibility = Visibility.Visible;
            Button_Ajouter_Valider.Visibility = Visibility.Visible;
        }
        public void Update()
        {
            ApplicationData.UpdateTypeAnimal();
            listViewTypeAnimal.ItemsSource = ApplicationData.listeTypeAnimal;
        }

        private void Button_Ajouter_Valider_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(Entrer_Espece.Text))
            {
                Entrer_Espece.BorderBrush = Brushes.Red;
            }
            else
            {
                TypeAnimal animal = new TypeAnimal(Entrer_Espece.Text);
                animal.Create();

                listViewTypeAnimal.Visibility = Visibility.Visible;
                Label_Ajouter.Visibility = Visibility.Hidden;
                Entrer_Espece.Visibility = Visibility.Hidden;
                Button_Ajouter_Valider.Visibility = Visibility.Hidden;
                Entrer_Espece.Text = "";
                Entrer_Espece.BorderBrush = Brushes.Gray;
                this.Update();
            }
        }
    }
}
