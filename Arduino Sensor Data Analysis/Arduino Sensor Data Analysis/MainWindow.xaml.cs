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

        bool encendido = true;
        int actual = 0;
        SDA_Core.Communication.Serial ser = new SDA_Core.Communication.Serial("COM4", 9600);
        SDA_Core.Data.ProfileManager test = new SDA_Core.Data.ProfileManager();
        SDA_Core.Data.Profile p = new SDA_Core.Data.Profile("Sensor de temperatura.", "Temperatura de ambiente.");
        public MainWindow()
        {
            InitializeComponent();
            ser.Open();
            ser.Write("a");
            test.NewProfile(p);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (encendido) ser.Write("b");
            else ser.Write("a");
            encendido = !encendido;
            label_Copy.Content = actual.ToString();
            if (actual != test.ProfileList.Count())
            {
                label.Content = test.ProfileList[actual].Name;
                actual++;
            }
            else
            {
                actual = 0;
                label.Content = test.ProfileList[actual].Name;
            }
        }
    }
}
