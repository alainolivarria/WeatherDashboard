using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WeatherDashboard.Data;
using WeatherDashboard.Entities;

namespace WeatherDashboard.Business
{
    public class Weather
    {
        private const string URL = "https://api.weatherbit.io/v1.0/history/daily";


        public static CityWeather GetWheather(DateTime start, DateTime end, string city_name)
        {
            ImportData(start, end, city_name);

            var item = WeatherData.myDataCityWeather.Where(a => a.city_name.ToUpper() == city_name.ToUpper() && a.data.Where(b => Convert.ToDateTime(b.datetime) >= start && Convert.ToDateTime(b.datetime) <= end).FirstOrDefault() != null).FirstOrDefault();

            return item;
        }

        public static List<City> GetCities()
        {
            return WeatherData.GetAllCities();
        }

        private static void ImportData(DateTime start, DateTime end, string city_name)
        {
            //this method simulate import data to the database, we should keep the informatión in on local.

            string urlParameters = string.Empty;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);


            City city = WeatherData.GetCity(city_name);

            if (start <= end && city != null)
            {
                while (start < end) //the licence only grant one call per date
                {

                    var existObj = WeatherData.myDataCityWeather.Where(a => a.city_name.ToUpper() == city_name.ToUpper()).FirstOrDefault();
                    //ANOTHER KEY: 37fd611ca7e6466a8feda4489191a8f9
                    urlParameters = string.Format("?lat={0}&lon={1}&start_date={2:yyyy-MM-dd}&end_date={3:yyyy-MM-dd}&key=acfc6084caae440883937ebd4cf90140", city.lat, city.lon, start, start.AddDays(1));
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                    if (response.IsSuccessStatusCode)
                    {
                        string json = response.Content.ReadAsStringAsync().Result;

                        CityWeather result = JsonConvert.DeserializeObject<CityWeather>(json);

                        if (existObj == null)
                            WeatherData.myDataCityWeather.Add(result);
                        else
                        {
                           var notContain = result.data.Where(a => !existObj.data.Select(b => Convert.ToDateTime(b.datetime)).Contains(Convert.ToDateTime(a.datetime))).ToList();
                           if(notContain.Count > 0)
                               existObj.data.AddRange(notContain);
                        }
                            
                    }

                    start = start.AddDays(1);
                }
            }

            client.Dispose();


        }
    }
}
