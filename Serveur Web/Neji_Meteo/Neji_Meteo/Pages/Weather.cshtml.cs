using Microsoft.AspNetCore.Mvc.RazorPages;
using Neji_Meteo.Controllers;
using Neji_Meteo.Models;
using System.Collections.Generic;

namespace Neji_Meteo.Pages
{
    public class WeatherModel : PageModel
    {
        // D�clarer la propri�t� WeatherDataList
        public List<WeatherData> WeatherDataList { get; set; } = new List<WeatherData>();

        // M�thode appel�e lorsque la page est charg�e
        public void OnGet()
        {
            // R�cup�rer les donn�es depuis le contr�leur
            WeatherDataList = WeatherController.GetData();
        }
    }
}