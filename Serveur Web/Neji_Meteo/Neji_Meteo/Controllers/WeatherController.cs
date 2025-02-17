using Microsoft.AspNetCore.Mvc;
using Neji_Meteo.Models;
using System.Collections.Generic;

namespace Neji_Meteo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        // Liste statique pour stocker les données météo
        private static List<WeatherData> _data = new List<WeatherData>();

        // POST: api/weather
        [HttpPost]
        public IActionResult Post([FromBody] WeatherData data)
        {
            if (data == null)
            {
                return BadRequest("Données invalides.");
            }

            data.Timestamp = DateTime.Now; // Ajouter un timestamp
            _data.Add(data);
            return Ok();
        }

        // GET: api/weather
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_data);
        }

        // Méthode statique pour récupérer les données
        public static List<WeatherData> GetData()
        {
            return _data;
        }
    }
}









//using Microsoft.AspNetCore.Mvc;
//using Neji_Meteo.Models;
//using System.Collections.Generic;

//namespace Neji_Meteo.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class WeatherController : ControllerBase
//    {
//        private static List<WeatherData> _data = new List<WeatherData>();

//        // POST: api/weather
//        [HttpPost]
//        public IActionResult Post([FromBody] WeatherData data)
//        {
//            data.Timestamp = DateTime.Now; // Ajouter un timestamp
//            _data.Add(data);
//            return Ok();
//        }

//        // GET: api/weather
//        [HttpGet]
//        public IActionResult Get()
//        {
//            return Ok(_data);
//        }
//    }


//}







//using Microsoft.AspNetCore.Mvc;
//using Neji_Meteo.Models;
//using System.Collections.Generic;

//namespace Neji_Meteo.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController] //Important pour la validation automatique du modèle
//    public class WeatherController : ControllerBase
//    {
//        private static List<WeatherData> _data = new List<WeatherData>();

//        // POST: api/weather
//        [HttpPost]
//        public IActionResult Post([FromBody] WeatherData data)
//        {
//            // Validation du modèle (grace à l'attribut [ApiController])
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState); // Retourne une erreur 400 avec les erreurs de validation
//            }

//            if (data == null)
//            {
//                return BadRequest("Les données météorologiques sont nulles."); // Retourne une erreur 400 si data est null
//            }

//            data.Timestamp = DateTime.Now; // Ajouter un timestamp
//            _data.Add(data);

//            // Retourne un code 201 (Created) et l'objet créé
//            return CreatedAtAction(nameof(Get), new { }, data); // Assurez vous que "Get" est bien le nom de votre action Get
//            // Alternative si vous ne voulez pas renvoyer l'objet créé (code 200 OK et un message):
//            // return Ok(new { message = "Données météorologiques enregistrées avec succès." });
//        }

//        // GET: api/weather
//        [HttpGet]
//        public IActionResult Get()
//        {
//            return Ok(_data);
//        }
//    }
//}