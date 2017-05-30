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

namespace Arduino_Sensor_Data_Analysis
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SDA_Program.Interface.MainWindow IO;

        public MainWindow()
        {
            InitializeComponent();

            IO = new SDA_Program.Interface.MainWindow();

            IO.HomeClicked(B_Home, B_Profiles, B_Communication, FrameWindow);
        }

        private void B_Home_Click(object sender, RoutedEventArgs e)
        {
            IO.HomeClicked(B_Home, B_Profiles, B_Communication, FrameWindow);
        }

        private void B_Profiles_Click(object sender, RoutedEventArgs e)
        {

            IO.ProfilesClicked(B_Home, B_Profiles, B_Communication, FrameWindow);
        }

        private void B_Communication_Click(object sender, RoutedEventArgs e)
        {
            IO.CommunicationClicked(B_Home, B_Profiles, B_Communication, FrameWindow);
        }

       
    }
}
