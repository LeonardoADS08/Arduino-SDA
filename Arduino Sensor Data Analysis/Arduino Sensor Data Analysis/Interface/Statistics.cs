using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Diagnostics;
using SDA_Core.Data.Containers;
namespace SDA_Program.Interface
{
    class Statistics
    {

        public Statistics()
        {

        }

        public void LoadColumns(ComboBox CB_Columns)
        {
            List<Measurement> format = SDA_Core.Program.GetSensorsFormat();
            foreach (Measurement value in format)
            {
                CB_Columns.Items.Add(value.Name);
            }
        }

        public DataTable CalculateProm(ComboBox CB_Columns)
        {
            if (CB_Columns.SelectedItem == null) return null;
            DataTable result = new DataTable();
            List<Measurement> format = SDA_Core.Program.GetSensorsFormat();
            SensorData temporal = SDA_Core.Program.ReceivedData;
            for (int i = 0; i < format.Count; ++i)
            {
                int selected = CB_Columns.SelectedIndex;
                if (selected == i) continue;
                double prom = 0;
                

            }
            return result;
        }
    }
}
