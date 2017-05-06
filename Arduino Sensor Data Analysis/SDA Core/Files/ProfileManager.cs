using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Core.Files
{
    /// <summary>
    /// 
    /// </summary>
    public class ProfileManager
    {
        [Serializable]
        private struct ProfileConfigurations
        {
            string Name;
            string Information;
            int SensorsQuantities;
        }

        [Serializable]
        private struct Profile
        {
            ProfileConfigurations Configurations;
            List<Tuple<string, double>> Sensor;
        }

        private string _profileFileName;
        private DataSerializer <Profile> _profilesData;

        private DataSerializer<Profile> ProfilesData
        {
            get { return _profilesData; }
        }

        public ProfileManager()
        {
            _profileFileName = "Profiles";
            _profilesData = new DataSerializer<Profile>(_profileFileName);
        }
    }
}
