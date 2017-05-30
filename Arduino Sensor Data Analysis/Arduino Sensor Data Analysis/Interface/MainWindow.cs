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
        private SDA_Program.View.Home HomeWindow = new SDA_Program.View.Home();

        // Color seleccionado   : 98, 174, 178
        // Color deseleccionado : 100, 151, 153
        public void HomeClicked(Button B_Home, Button B_Profiles, Button B_Communication, Frame F_Page)
        {
            B_Home.Background = new SolidColorBrush(Color.FromRgb(98, 174, 178));
            B_Profiles.Background = new SolidColorBrush(Color.FromRgb(100, 151, 153));
            B_Communication.Background = new SolidColorBrush(Color.FromRgb(100, 151, 153));
            F_Page.Content = HomeWindow;
        }

        public void ProfilesClicked(Button B_Home, Button B_Profiles, Button B_Communication, Frame F_Frame)
        {

            B_Home.Background = new SolidColorBrush(Color.FromRgb(100, 151, 153));
            B_Profiles.Background = new SolidColorBrush(Color.FromRgb(98, 174, 178));
            B_Communication.Background = new SolidColorBrush(Color.FromRgb(100, 151, 153));
        }

        public void CommunicationClicked(Button B_Home, Button B_Profiles, Button B_Communication, Frame F_Frame)
        {
            B_Home.Background = new SolidColorBrush(Color.FromRgb(100, 151, 153));
            B_Profiles.Background = new SolidColorBrush(Color.FromRgb(100, 151, 153));
            B_Communication.Background = new SolidColorBrush(Color.FromRgb(98, 174, 178));
        }
    }
}
