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
        [Serializable]
        struct Profile
        {
            string _name;
            string _information;
        }

        DataSerializer<Profile> _dataManagerProfiles;

        public ProfileManager()
        {
            _dataManagerProfiles = new DataSerializer<Profile>();

        }

    }
}
