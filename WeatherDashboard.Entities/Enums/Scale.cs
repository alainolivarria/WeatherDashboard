using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherDashboard.Entities.Enums
{
    public enum Scale
    {
        [Description("Celsius")]
        Celsius = 1,
        [Description("Fahrenheit")]
        Fahrenheit = 2,
    }
}
