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

namespace SDA_Program.View
{
    /// <summary>
    /// Lógica de interacción para Statistics.xaml
    /// </summary>
    public partial class Statistics : Page
    {
        Interface.Statistics IO;
        public Statistics()
        {
            InitializeComponent();

            IO = new Interface.Statistics();

            IO.LoadColumns(CB_General_Variables);
            IO.LoadColumns(CB_Charts_Variables);
        }


    }
}
