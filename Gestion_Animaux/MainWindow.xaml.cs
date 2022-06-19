using System;
using System.Windows.Threading;
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
        bool mRestoreIfMove = false;    
        public MainWindow()
        {
            InitializeComponent();

            MainFrame.Navigate(new System.Uri("/Frames/Main/Main.xaml",
             UriKind.RelativeOrAbsolute));

        }
        
        /// <summary>
        /// Modifie la fenetre à l'appuie du bouton Gérer Adoption et envoi sur la fenetre correspondante.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Adoption_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new System.Uri("/Frames/Adoptions/GestionAdoption.xaml",
             UriKind.RelativeOrAbsolute));
        }
        
        /// <summary>
        /// Modifie la fenetre à l'appuie du bouton Gérer Espece et envoi sur la fenetre correspondante.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Espece_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new System.Uri("/Frames/Espece/EspeceFrame.xaml",
             UriKind.RelativeOrAbsolute));
        }

        
        /// <summary>
        /// Permet de fermer la fenetre.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
        /// <summary>
        /// Permet d'agrandir ou rendre flottante la fenetre.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void maximize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MaxMin();
        }
        /// <summary>
        /// Permet de réduire la fenêtre.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void minimize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowMain.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Modifie la fenetre à l'appuie du bouton Gérer Animaux et envoi sur la fenetre correspondante.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Animaux_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new System.Uri("/Frames/Animaux/GestionAnimaux.xaml",
             UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        /// Permet de rendre la barre du haut de fentre clicquable. Permet de déplacer la fenetre,
        /// la rendre flottante/l'agrandir en cas de double clique.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void statusBar_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if ((ResizeMode == ResizeMode.CanResize) || (ResizeMode == ResizeMode.CanResizeWithGrip))
                {
                    MaxMin();
                }

                return;
            }

            else if (WindowState == WindowState.Maximized)
            {
                mRestoreIfMove = true;
                return;
            }

            DragMove();
        }
        /// <summary>
        /// Renvoi si la fentre prend l'ecran entier ou non.
        /// </summary>
        /// <returns></returns>
        private bool IsMaximized()
        {
            bool value = false;
            if (
                WindowMain.Left == SystemParameters.WorkArea.Left &&
                WindowMain.Top == SystemParameters.WorkArea.Top &&
                WindowMain.Height == SystemParameters.WorkArea.Height &&
                WindowMain.Width == SystemParameters.WorkArea.Width)
                value = true;

            return value;
        }
        /// <summary>
        /// Permet d'agrandir la fenetre si elle est flottante et inversement, la rendre flottante si elle est agrandie au max.
        /// Avec un style de fenêtre à 'None', appeler 'WindowsState.Maximized' reviendrait à mettre la fenêtre en plein écran, 
        /// cette fonction contourne ce problème.
        /// </summary>
        private void MaxMin()
        {
            if (IsMaximized())
            {
                this.WindowMain.Height = this.WindowMain.MinHeight;
                this.WindowMain.Width = this.WindowMain.MinWidth;
            }
            else if (this.WindowMain.WindowState == WindowState.Maximized)
                this.WindowMain.WindowState = WindowState.Normal;
            else
            {
                WindowMain.Left = SystemParameters.WorkArea.Left;
                WindowMain.Top = SystemParameters.WorkArea.Top;
                WindowMain.Height = SystemParameters.WorkArea.Height;
                WindowMain.Width = SystemParameters.WorkArea.Width;
            }
        }
        /// <summary>
        /// Permet de rendre la barre du haut de fentre interagissable.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void statusBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (Mouse.LeftButton == MouseButtonState.Pressed)
                {
                    var windowMode = this.ResizeMode;
                    if (this.WindowMain.ResizeMode != ResizeMode.NoResize)
                    {
                        this.WindowMain.ResizeMode = ResizeMode.NoResize;
                    }

                    this.UpdateLayout();


                    DragMove();


                    if (this.WindowMain.ResizeMode != windowMode)
                    {
                        this.WindowMain.ResizeMode = windowMode;
                    }

                    this.UpdateLayout();
                }
            }
        }

        /// <summary>
        /// Envoi sur GitHub en cas d'appui sur voir le code source.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("cmd", "/c start https://github.com/Nelda74/SAE_01");
        }

        /// <summary>
        /// Permet de modifier le nom de la fenetre en fonction la localisation dans l'application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            Page page = e.Content as Page;    

            if (page != null)   
                title.Content = page.Title.ToUpper();
        }

        /// <summary>
        /// Envoi sur l'accueil en cas de clique sur l'image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new System.Uri("/Frames/Main/Main.xaml",
             UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        /// Modifie la fenetre à l'appuie du bouton Gérer Adoptant et envoi sur la fenetre correspondante.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Adoptant_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new System.Uri("/Frames/Adoptants/GestionAdoptant.xaml",
             UriKind.RelativeOrAbsolute));
        }
    }
    

}
