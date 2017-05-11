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
    public class ProfileManager : DataSerializer<Profile>
    {
        /// <summary>
        /// ES: Estructura para almacenar datos de un perfil.
        /// </summary>
        

        // ES: Lista de todos los perfiles existentes.
        private List<Profile> _profiles;
        // ES: DataSerializer para persistencia de datos de los perfiles
        //private DataSerializer<Profile> _dataManagerProfiles;

        /// <summary>
        /// ES: Devuelve todos los perfiles existentes en una lista.
        /// NOTA: Hacer set de 'ProfileList' es una operación costosa, requiere reescribir todos los datos existentes en el archivo binario.
        /// </summary>
        public List<Profile> ProfileList
        {
            get { return _profiles; }
            set
            {
                ClearBinary();
                SaveData(value);
                _profiles = value;
            }
        }

        /// <summary>
        /// ES: Constructor de la clase ProfileManager.
        /// </summary>
        public ProfileManager()
        {
            try
            {
                _profiles = RecoverData();
                RuntimeLogs.SendLog("SE HA RECUPERADO LOS DATOS " + _profiles.Count().ToString(),   "-+-.ProfileManager()");
            }
            catch (Exception ex) { RuntimeLogs.SendLog(ex.Message, typeof(ProfileManager).Name + ".ProfileManager()"); }
        }

        /// <summary>
        /// ES: Guarda un nuevo perfil.
        /// </summary>
        /// <param name="profileData">ES: Datos del nuevo perfil a añadir.</param>
        public void NewProfile(Profile profileData)
        {
            try { 
                _profiles.Add(profileData);
                SaveData(profileData);
            }
            catch (Exception ex) { RuntimeLogs.SendLog(ex.Message, typeof(ProfileManager).Name + ".NewProfile(Profile)"); }
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
                UpdateData(IdProfile, profileData);
            }
            catch (Exception ex) { RuntimeLogs.SendLog(ex.Message, typeof(ProfileManager).Name + ".UpdateProfile(int, Profile)"); }
        }

        /// <summary>
        /// ES: Elimina permanentemente un perfil.
        /// </summary>
        /// <param name="Id">ES: Identificador del perfil que se desea eliminar.</param>
        public void DeleteProfile(int IdProfile)
        {
            try
            {
                DeleteRegister(IdProfile);
            }
            catch (Exception ex) { RuntimeLogs.SendLog(ex.Message, typeof(ProfileManager).Name + ".DeleteProfile(int)"); }
            
        }
    }
}
