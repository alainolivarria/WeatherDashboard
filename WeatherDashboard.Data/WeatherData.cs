using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherDashboard.Entities;

namespace WeatherDashboard.Data
{
    public class WeatherData
    {

        public static List<CityWeather> myDataCityWeather = new List<CityWeather>();

        public static List<City> GetAllCities()
        {
            List<City> lst = new List<City>();

            lst.Add(new City("Ciudad Obregón", "27.4833", "-109.9333"));
            lst.Add(new City("Navojoa", "27.0715", "-109.444"));
            lst.Add(new City("Hermosillo", "29.0892", "-110.961"));
            lst.Add(new City("Guaymas", "27.9192", "-110.8975"));
            lst.Add(new City("Nogales", "31.3086", "-110.9421"));


            return lst;
        }

        public static City GetCity(string city_name)
        {
            return GetAllCities().Where(a => a.name.ToUpper() == city_name.ToUpper()).FirstOrDefault();
        }

        public static List<CityWeather> GetWeatherByDates(DateTime start, DateTime end, string city_name)
        {

            var lst = myDataCityWeather.Where(a => a.city_name.ToUpper() == city_name.ToUpper() && a.data.Where(b => Convert.ToDateTime(b.datetime) >= start && Convert.ToDateTime(b.datetime) <= end).FirstOrDefault() == null).ToList();
            return lst;
        }


    }
}
