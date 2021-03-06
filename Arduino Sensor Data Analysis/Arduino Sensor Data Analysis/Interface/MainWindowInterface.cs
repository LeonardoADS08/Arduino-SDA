﻿using SDA_Core;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace SDA_Program.Interface
{
    internal class MainWindowInterface
    {
        // Views
        private SDA_Program.View.Home homeFrame;

        private SDA_Program.View.Statistics statisticsFrame;
        private SDA_Program.View.Configurations configurationsFrame;

        /// <summary>
        /// ES: Constructor de la clase MainWindowInterface
        /// </summary>
        public MainWindowInterface()
        {
            homeFrame = new View.Home();
            statisticsFrame = new View.Statistics();
            configurationsFrame = new View.Configurations();
        }

        // Color seleccionado   : 98, 174, 178
        // Color deseleccionado : 100, 151, 153

        // Todos los siguientes metodos son para cambiar de ventana, y cambiar el color del botón cuando es seleccionado.

        public void HomeClicked(Button B_Home, Button B_Statistics, Button B_Configurations, Frame F_Page)
        {
            B_Home.Background = new SolidColorBrush(Color.FromRgb(98, 174, 178));
            B_Statistics.Background = new SolidColorBrush(Color.FromRgb(100, 151, 153));
            B_Configurations.Background = new SolidColorBrush(Color.FromRgb(100, 151, 153));
            F_Page.Content = homeFrame;
        }

        public void StatisticsClicked(Button B_Home, Button B_Statistics, Button B_Configurations, Frame F_Page)
        {
            B_Home.Background = new SolidColorBrush(Color.FromRgb(100, 151, 153));
            B_Statistics.Background = new SolidColorBrush(Color.FromRgb(98, 174, 178));
            B_Configurations.Background = new SolidColorBrush(Color.FromRgb(100, 151, 153));

            statisticsFrame.LoadData(homeFrame.IO.GetSerialMonitorData());
            F_Page.Content = statisticsFrame;
        }

        public void ConfigurationClicked(Button B_Home, Button B_Statistics, Button B_Configurations, Frame F_Page)
        {
            B_Home.Background = new SolidColorBrush(Color.FromRgb(100, 151, 153));
            B_Statistics.Background = new SolidColorBrush(Color.FromRgb(100, 151, 153));
            B_Configurations.Background = new SolidColorBrush(Color.FromRgb(98, 174, 178));
            F_Page.Content = configurationsFrame;
        }
    }
}