using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherDashboard.Entities
{
    public class CityWeather
    {

        private string _timezone = string.Empty;

        public string timezone
        {
            get { return _timezone; }
            set { _timezone = value; }
        }

        private string _state_code = string.Empty;

        public string state_code
        {
            get { return _state_code; }
            set { _state_code = value; }
        }

        private string _country_code = string.Empty;

        public string sountry_code
        {
            get { return _country_code; }
            set { _country_code = value; }
        }

        private string _lat = string.Empty;

        public string lat
        {
            get { return _lat; }
            set { _lat = value; }
        }

        private string _lon = string.Empty;

        public string lon
        {
            get { return _lon; }
            set { _lon = value; }
        }

        private string _city_name = string.Empty;

        public string city_name
        {
            get { return _city_name; }
            set { _city_name = value; }
        }

        private string _station_id = string.Empty;

        public string ctation_id
        {
            get { return _station_id; }
            set { _station_id = value; }
        }

        private List<CityWeatherData> _data = new List<CityWeatherData>();

        public List<CityWeatherData> data
        {
            get { return _data; }
            set { _data = value; }
        }

        private string[] _sources = new string[] { };

        public string[] sources
        {
            get { return _sources; }
            set { _sources = value; }
        }

        private string _city_id = string.Empty;

        public string city_id
        {
            get { return _city_id; }
            set { _city_id = value; }
        }

    }
}
