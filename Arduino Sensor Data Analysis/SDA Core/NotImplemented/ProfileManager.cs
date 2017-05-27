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
        // ES: Lista de todos los perfiles existentes.
        private List<Profiles> _profiles;
        // ES: DataSerializer para persistencia de datos de los perfiles
        private DataSerializer<Profiles> _dataManagerProfiles;

        /// <summary>
        /// ES: Devuelve todos los perfiles existentes en una lista.
        /// NOTA: Hacer set de 'ProfileList' es una operación relativamente costosa, requiere reescribir todos los datos existentes en el archivo binario.
        /// </summary>
        public List<Profiles> Profiles
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
        /// ES: Constructor de la clase ProfileManager.
        /// </summary>
        public ProfileManager()
        {
            try
            {
                _profiles = _dataManagerProfiles.RecoverData();
            }
            catch (Exception ex) { RuntimeLogs.SendLog(ex.Message, typeof(ProfileManager).Name + ".ProfileManager()"); }
        }

        /// <summary>
        /// ES: Guarda un nuevo perfil.
        /// </summary>
        /// <param name="profileData">ES: Datos del nuevo perfil a añadir.</param>
        public void NewProfile(Profiles profileData)
        {
            try { 
                _profiles.Add(profileData);
                _dataManagerProfiles.SaveData(profileData);
            }
            catch (Exception ex) { RuntimeLogs.SendLog(ex.Message, typeof(ProfileManager).Name + ".NewProfile(Profile)"); }
        }

        /// <summary>
        /// ES: Actualiza un perfil ya guardado.
        /// </summary>
        /// <param name="IdProfile">ES: Identificador del perfil que se desea editar.</param>
        /// <param name="profileData">ES: Nuevos datos del perfil a editar.</param>
        public void UpdateProfile(int IdProfile, Profiles profileData)
        {
            try
            {
                _dataManagerProfiles.UpdateData(IdProfile, profileData);
            }
            catch (Exception ex) { RuntimeLogs.SendLog(ex.Message, typeof(ProfileManager).Name + ".UpdateProfile(int, Profile)"); }
        }

        /// <summary>
        /// ES: Elimina permanentemente un perfil.
        /// </summary>
        /// <param name="IdProfile">ES: Identificador del perfil que se desea eliminar.</param>
        public void DeleteProfile(int IdProfile)
        {
            try
            {
                _dataManagerProfiles.DeleteRegister(IdProfile);
            }
            catch (Exception ex) { RuntimeLogs.SendLog(ex.Message, typeof(ProfileManager).Name + ".DeleteProfile(int)"); }
            
        }
    }
}
