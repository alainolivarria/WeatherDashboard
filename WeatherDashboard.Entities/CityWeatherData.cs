using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherDashboard.Entities
{
    public class CityWeatherData
    {
        private string _max_temp = string.Empty;

        public string max_temp
        {
            get { return _max_temp; }
            set { _max_temp = value; }
        }

        private string _min_temp = string.Empty;

        public string min_temp
        {
            get { return _min_temp; }
            set { _min_temp = value; }
        }

        private string _datetime = string.Empty;

        public string datetime
        {
            get { return _datetime; }
            set { _datetime = value; }
        }

        private string _temp = string.Empty;

        public string temp
        {
            get { return _temp; }
            set { _temp = value; }
        }
    }
}
