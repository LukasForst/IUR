using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUR_p07
{
    /// <summary>
    /// Class representing simplified (without notification) view model 
    /// </summary>
    public class WeatherCardViewModel
    {
        public string Location { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public string Conditions { get; set; }
        public string IconPath { get; set; }
    }
}
