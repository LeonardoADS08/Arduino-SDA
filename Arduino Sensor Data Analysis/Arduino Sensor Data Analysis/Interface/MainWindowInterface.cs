﻿using System;
using System.Collections.Generic;
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
using System.Data;
using SDA_Core;

namespace SDA_Program.Interface
{
    class MainWindowInterface
    {
        private SDA_Program.View.Home homeFrame;
        private SDA_Program.View.Statistics statisticsFrame;
        private SDA_Program.View.Configurations configurationsFrame;

        public MainWindowInterface()
        {
            homeFrame = new View.Home();
            configurationsFrame = new View.Configurations();
        }

        // Color seleccionado   : 98, 174, 178
        // Color deseleccionado : 100, 151, 153
        public void HomeClicked(Button B_Home, Button B_Statistics, Button B_Configurations ,Frame F_Page)
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

            F_Page.Content = new SDA_Program.View.Statistics(homeFrame.IO.GetSerialMonitorData());
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