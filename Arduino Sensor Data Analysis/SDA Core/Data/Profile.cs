using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Core.Data
{
    /// <summary>
    /// ES: Clase contenedora para perfiles.
    /// </summary>
    [Serializable]
    public class Profile
    {
        private string _name;
        private string _information;
        private SensorSet _sensors;

        /// <summary>
        /// ES: Nombre del perfil.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// ES: Información breve sobre el perfil.
        /// </summary>
        public string Information
        {
            get { return _information; }
            set { _information = value; }
        }

        /// <summary>
        /// ES: Conjunto de sensores registrados para el perfil.
        /// </summary>
        public SensorSet Sensors
        {
            get { return _sensors; }
            set { _sensors = value; }
        }

        /// <summary>
        /// ES: Constructor de la clase Profile.
        /// </summary>
        /// <param name="name">ES: Nombre del perfil.</param>
        /// <param name="information">ES: Informacion breve del perfil.</param>
        /// <param name="sensorSetName">ES: Nombre del conjunto de sensores.</param>
        public Profile(string name = "New Profile", string information = "This is a new profile.", string sensorSetName = "SensorSet")
        {
            _name = name;
            _information = information;
            _sensors = new SensorSet(sensorSetName);
        }
    }
}
