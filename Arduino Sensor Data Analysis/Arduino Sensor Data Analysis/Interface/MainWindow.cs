using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Threading;

namespace SDA_Program.Interface
{
    class MainWindow
    {
        private SDA_Program.View.Home HomeWindow;
        private SDA_Program.View.Statistics StatisticsWindow;

        public MainWindow()
        {
            HomeWindow = new View.Home();
            StatisticsWindow = new View.Statistics();
        }

        // Color seleccionado   : 98, 174, 178
        // Color deseleccionado : 100, 151, 153
        public void HomeClicked(Button B_Home, Button B_Statistics, Frame F_Page)
        {
            B_Home.Background = new SolidColorBrush(Color.FromRgb(98, 174, 178));
            B_Statistics.Background = new SolidColorBrush(Color.FromRgb(100, 151, 153));
            F_Page.Content = HomeWindow;
        }

        public void StatisticsClicked(Button B_Home, Button B_Statistics, Frame F_Page)
        {
            B_Home.Background = new SolidColorBrush(Color.FromRgb(100, 151, 153));
            B_Statistics.Background = new SolidColorBrush(Color.FromRgb(98, 174, 178));
            F_Page.Content = new SDA_Program.View.Statistics();
        }
    }
}
