﻿using System;
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
            listeAdoption.ItemsSource = ApplicationData.listeAdoptions;
            listeAdoption.SelectedIndex = 0;
            
        }

        
    }
}
