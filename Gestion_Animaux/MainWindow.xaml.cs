
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Switch_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Adoption_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new System.Uri("/Frames/AdoptionFrame.xaml",
             UriKind.RelativeOrAbsolute));
        }

        private void MinimizeWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowMain.WindowState = WindowState.Minimized;
        }

        private void Espece_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new System.Uri("/Frames/TypeAnimalFrame.xaml",
             UriKind.RelativeOrAbsolute));
        }
    }

}
