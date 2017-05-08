using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Core.Data
{
    /// <summary>
    /// ES: Clase para guardar perfiles de usuario y sus configuraciones.
    /// </summary>
    public class ProfileManager
    {
        /// <summary>
        /// ES: Estructura para almacenar datos de un perfil.
        /// </summary>
        [Serializable]
        public class Profile
        {
            private string _name;
            private string _information;
            private SensorSet _sensors;

            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            public string Information
            {
                get { return _information; }
                set { _information = value; }
            }

            public SensorSet Sensors
            {
                get { return _sensors; }
                set { _sensors = value; }
            }

            public Profile(string name = "New Profile", string information = "This is a new profile.")
            {
                _name = name;
                _information = information;
                _sensors = new SensorSet("New SensorSet");
            }
        }

        // ES: Lista de todos los perfiles existentes.
        private List<Profile> _profiles;
        // ES: DataSerializer para persistencia de datos de los perfiles
        private DataSerializer<Profile> _dataManagerProfiles;

        /// <summary>
        /// ES: Devuelve todos los perfiles existentes en una lista.
        /// NOTA: Hacer set de 'ProfileList' es una operación costosa, requiere reescribir todos los datos existentes en el archivo binario.
        /// </summary>
        public List<Profile> ProfileList
        {
            get { return _profiles; }
            set
            {
                _dataManagerProfiles.ClearBinary();
                _dataManagerProfiles.SaveData(value);
                _profiles = value;
            }
        }

        /// <summary>
        /// ES: Constructor.
        /// </summary>
        public ProfileManager()
        {
            try
            {
                _dataManagerProfiles = new DataSerializer<Profile>();
                _profiles = _dataManagerProfiles.RecoverData();
            }
            catch (Exception ex) { RuntimeLogs.SendLog(ex.Message, typeof(ProfileManager).DeclaringMethod + ".ProfileManager()"); }
        }

        /// <summary>
        /// ES: Guarda un nuevo perfil.
        /// </summary>
        /// <param name="profileData">ES: Datos del nuevo perfil a añadir.</param>
        public void NewProfile(Profile profileData)
        {
            try { 
                _profiles.Add(profileData);
                _dataManagerProfiles.SaveData(profileData);
            }
            catch (Exception ex) { RuntimeLogs.SendLog(ex.Message, typeof(ProfileManager).DeclaringMethod + ".NewProfile(Profile)"); }
        }

        /// <summary>
        /// ES: Actualiza un perfil ya guardado.
        /// </summary>
        /// <param name="Id">ES: Identificador del perfil que se desea editar.</param>
        /// <param name="profileData">ES: Nuevos datos del perfil a editar.</param>
        public void UpdateProfile(int IdProfile, Profile profileData)
        {
            try
            {
                _dataManagerProfiles.UpdateData(IdProfile, profileData);
            }
            catch (Exception ex) { RuntimeLogs.SendLog(ex.Message, typeof(ProfileManager).DeclaringMethod + ".UpdateProfile(int, Profile)"); }
        }

        /// <summary>
        /// ES: Elimina permanentemente un perfil.
        /// </summary>
        /// <param name="Id">ES: Identificador del perfil que se desea eliminar.</param>
        public void DeleteProfile(int IdProfile)
        {
            try
            {
                _dataManagerProfiles.DeleteRegister(IdProfile);
            }
            catch (Exception ex) { RuntimeLogs.SendLog(ex.Message, typeof(ProfileManager).DeclaringMethod + ".DeleteProfile(int)"); }
            
        }
    }
}
