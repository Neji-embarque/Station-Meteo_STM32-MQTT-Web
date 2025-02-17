namespace Neji_Meteo.Models
{
    public class WeatherData
    {
        public float Temperature { get; set; } // Température en °C
        public float Pressure { get; set; }   // Pression en hPa
        public DateTime Timestamp { get; set; } // Date et heure de la mesure
    }
}