using Microsoft.AspNetCore.Mvc.RazorPages;
using Neji_Meteo.Controllers;
using Neji_Meteo.Models;
using System.Collections.Generic;

namespace Neji_Meteo.Pages
{
    public class WeatherModel : PageModel
    {
        // Déclarer la propriété WeatherDataList
        public List<WeatherData> WeatherDataList { get; set; } = new List<WeatherData>();

        // Méthode appelée lorsque la page est chargée
        public void OnGet()
        {
            // Récupérer les données depuis le contrôleur
            WeatherDataList = WeatherController.GetData();
        }
    }
}