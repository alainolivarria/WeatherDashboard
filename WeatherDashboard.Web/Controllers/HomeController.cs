using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherDashboard.Business;
using WeatherDashboard.Entities;
using WeatherDashboard.Entities.Enums;

namespace WeatherDashboard.Web.Controllers
{
    public class HomeController : Controller
    {
        IDictionary<string, object> RespuestaObject = new Dictionary<string, object>();

        public ActionResult Index()
        {
            ViewBag.Title="Weather dashboard Challenge";
            return View();
        }

        /// <summary>
        /// Return list of temperatures and dates.
        /// </summary>
        /// <param name="endDate"></param> this param mark the end of the 15 days range.
        /// <param name="cityName"></param> name of the city.
        /// <param name="scale"></param> scale used.
        /// <returns></returns>
        public ActionResult GetWeather(DateTime endDate, string cityName, int scale)
        {
            try
            {
                DateTime end = Convert.ToDateTime(endDate.AddDays(1).ToShortDateString());
                DateTime start = end.AddDays(-15);

                var weather = Weather.GetWheather(start, end, cityName);

                if (weather != null)
                {
                    var lstDates = weather.data.Select(a => Convert.ToDateTime(a.datetime)).OrderBy(b => b).ToList();

                    var lstStrDates = lstDates.Select(a => string.Format("{0:dd/MM}", a)).ToList();
                    var lstStrDatesFull = lstDates.Select(a => string.Format("{0:dd/MM/yyyy}", a)).ToList();

                    List<decimal> lstTemps = new List<decimal>();

                    switch ((Scale)scale)
                    {
                        case Scale.Celsius:
                            lstTemps = weather.data.Select(b => decimal.Parse(b.temp)).ToList();
                            break;
                        case Scale.Fahrenheit:
                            lstTemps = weather.data.Select(b => (decimal.Parse(b.temp) * 9 / 5) + (32)).ToList();
                            break;
                    }



                    RespuestaObject.Add("success", true);
                    RespuestaObject.Add("temps", lstTemps);
                    RespuestaObject.Add("categories", lstStrDates);
                    RespuestaObject.Add("datesfull", lstStrDatesFull);
                }
                else
                {
                    RespuestaObject.Add("success", false);
                    RespuestaObject.Add("Msg", "Hubo un problema al realizar la operación.");
                }
            }
            catch
            {
                RespuestaObject.Clear();
                RespuestaObject.Add("success", false);
                RespuestaObject.Add("Msg", "Hubo un problema al realizar la operación.");
            }

            return this.Content(JsonConvert.SerializeObject(RespuestaObject));

        }

        public ActionResult GetCities()
        {
            try
            {
                RespuestaObject.Add("success", true);
                RespuestaObject.Add("cities", Weather.GetCities());
            }
            catch
            {
                RespuestaObject.Clear();
                RespuestaObject.Add("success", false);
                RespuestaObject.Add("Msg", "Hubo un problema al realizar la operación.");
            }

            return this.Content(JsonConvert.SerializeObject(RespuestaObject));

        }

        public ActionResult GetScales()
        {
            try
            {
                Dictionary<int, string> dicScales = new Dictionary<int, string>();
                dicScales.Add((int)Scale.Celsius, Scale.Celsius.ToString());
                dicScales.Add((int)Scale.Fahrenheit, Scale.Fahrenheit.ToString());

                RespuestaObject.Add("success", true);
                RespuestaObject.Add("scales", dicScales);
            }
            catch
            {
                RespuestaObject.Clear();
                RespuestaObject.Add("success", false);
                RespuestaObject.Add("Msg", "Hubo un problema al realizar la operación.");
            }

            return this.Content(JsonConvert.SerializeObject(RespuestaObject));

        }

    }
}