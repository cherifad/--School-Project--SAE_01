
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
            //listviewTypeAnimal.ItemsSource = ApplicationData.listeTypeAnimal;
            //listviewAdoptant.ItemsSource = ApplicationData.listeAdoptants;

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

        private void fermer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowMain.WindowState = WindowState.Minimized;
        }

        private void statusBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowMain.DragMove();
        }

        private void exit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void maximize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowMain.WindowState == WindowState.Maximized)
                this.WindowMain.WindowState = WindowState.Normal;
            else
                this.WindowMain.WindowState = WindowState.Maximized;
        }

        private void minimize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowMain.WindowState = WindowState.Minimized;
        }

        private void OnDragMoveWindow(object sender, MouseButtonEventArgs e)
        {
            Application.Current.MainWindow.DragMove();
        }
    }

}
