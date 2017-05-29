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
using System.Threading.Tasks;

namespace Arduino_Sensor_Data_Analysis
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SDA_Core.Data.SensorDataArray sda;
        DataTable procesado;
        SDA_Core.Communication.USB usb;
        SDA_Core.Data.Processing pro;
        public MainWindow()
        {
            InitializeComponent();
            FrameWindow.Content = new SDA_Program.View.Home();

            procesado = new DataTable();
            procesado.Columns.Add("Tiempo", typeof(double));
            procesado.Columns.Add("Datos", typeof(double));

            sda = new SDA_Core.Data.SensorDataArray();
            pro = new SDA_Core.Data.Processing();
            usb = new SDA_Core.Communication.USB("COM4", 9600);


           // dg1.DataContext = procesado;

            usb.Open();
            mostrar();
        }

        private async void mostrar()
        {
            int lastProcesadoRow = 0;
            while (usb.Available())
            {
                //label.Content = lastProcesadoRow.ToString();
                for (int i = lastProcesadoRow; i < usb.MessagesHistory.RowCount(); ++i)
                {
                    DataRow temp = procesado.NewRow();
                    temp[0] = usb.MessagesHistory.Row(i).Values(0);
                    temp[1] = usb.MessagesHistory.Row(i).Values(1);
                  //  label.Content = temp[0].ToString() + " - " + temp[1].ToString();
                    procesado.Rows.Add(temp);
                    lastProcesadoRow = i + 1;
                }
               // dg1.DataContext = procesado;
                await Task.Delay(100);
            }
        }

        // 98, 174, 178
        // 100, 151, 153
        private void B_Home_Click(object sender, RoutedEventArgs e)
        {
            B_Home.Background = new SolidColorBrush(Color.FromRgb(98, 174, 178));
            B_Profiles.Background = new SolidColorBrush(Color.FromRgb(100, 151, 153));
            B_Communication.Background = new SolidColorBrush(Color.FromRgb(100, 151, 153));
        }

        private void B_Profiles_Click(object sender, RoutedEventArgs e)
        {
            B_Home.Background = new SolidColorBrush(Color.FromRgb(100, 151, 153));
            B_Profiles.Background = new SolidColorBrush(Color.FromRgb(98, 174, 178));
            B_Communication.Background = new SolidColorBrush(Color.FromRgb(100, 151, 153));
        }

        private void B_Communication_Click(object sender, RoutedEventArgs e)
        {
            B_Home.Background = new SolidColorBrush(Color.FromRgb(100, 151, 153));
            B_Profiles.Background = new SolidColorBrush(Color.FromRgb(100, 151, 153));
            B_Communication.Background = new SolidColorBrush(Color.FromRgb(98, 174, 178));
        }

       
    }
}
