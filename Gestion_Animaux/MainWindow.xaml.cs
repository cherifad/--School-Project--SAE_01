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
            MainFrame.Navigate(new System.Uri("/Frames/Adoptions/GestionAdoption.xaml",
             UriKind.RelativeOrAbsolute));
        }

        private void MinimizeWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowMain.WindowState = WindowState.Minimized;
        }

        private void Espece_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new System.Uri("/Frames/Espece/EspeceFrame.xaml",
             UriKind.RelativeOrAbsolute));
        }

        private void fermer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowMain.WindowState = WindowState.Minimized;
        }

        private void exit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void maximize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MaxMin();
        }

        private void minimize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowMain.WindowState = WindowState.Minimized;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            if (WindowStyle != WindowStyle.None)
            {
                Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, (DispatcherOperationCallback)delegate (object unused)
                {
                    WindowStyle = WindowStyle.None;
                    return null;
                }
                , null);
            }
        }

        private void Animaux_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new System.Uri("/Frames/Animaux/GestionAnimaux.xaml",
             UriKind.RelativeOrAbsolute));
        }

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

        private void statusBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            /* 如何在Window.ResizeMode属性为CanResize的时候，阻止窗口拖动到屏幕边缘自动最大化。
               (When the Window.ResizeMode property is CanResize, 
               when the window is dragged to the edge of the screen, 
               it prevents the window from automatically maximizing.)*/
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

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("cmd", "/c start https://github.com/Nelda74/SAE_01");
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            Page page = e.Content as Page;    

            if (page != null)   
                title.Content = page.Title.ToUpper();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new System.Uri("/Frames/Main/Main.xaml",
             UriKind.RelativeOrAbsolute));
        }
    }
    

}
