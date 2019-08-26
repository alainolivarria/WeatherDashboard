using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherDashboard.Entities
{
    public class City
    {
        private string _name = string.Empty;

        public string name
        {
            get { return _name; }
            set { _name = value; }
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

        public City(string name_, string lat_, string lon_)
        {
            this.name = name_;
            this.lat = lat_;
            this.lon = lon_;
        }
    }
}
