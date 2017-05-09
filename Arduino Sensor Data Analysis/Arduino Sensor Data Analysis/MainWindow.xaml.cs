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

namespace Arduino_Sensor_Data_Analysis
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int actual = 0;
        bool encendido = true;
        SDA_Core.Data.ProfileManager pmanager = new SDA_Core.Data.ProfileManager();
        SDA_Core.Data.ProfileManager.Profile p = new SDA_Core.Data.ProfileManager.Profile("Campos electromagneticos", "Variaciones de campos electromagneticos.");
        SDA_Core.Communication.Serial ser = new SDA_Core.Communication.Serial("COM4", 9600);
        public MainWindow()
        {
            InitializeComponent();
            //pmanager.NewProfile(p);
            //pmanager.DeleteProfile(2);
            //pmanager = new SDA_Core.Data.ProfileManager();
            label.Content = "None.";
            label1.Content = "None.";
            label2.Content = "None.";

            
            ser.Open();
            ser.Write("b");
            escuchar();
            
        }

        private async void escuchar()
        {
            while (true)
            {
                textBlock.Text = ser.CommunicationHistory;
                await Task.Delay(100);
            }
        }

        private void change()
        {
            if (actual < pmanager.ProfileList.Count())
            {
                label.Content = pmanager.ProfileList[actual].Name;
                label1.Content = pmanager.ProfileList[actual].Information;
                label2.Content = actual.ToString();
                dataGrid.DataContext = pmanager.ProfileList[actual].Sensors;
                actual++;
            }
            else
            {
                actual = 0;
                change();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            change();
            if (encendido) ser.Write("b");
            else ser.Write("a");
            encendido = !encendido;
        }
    }
}
