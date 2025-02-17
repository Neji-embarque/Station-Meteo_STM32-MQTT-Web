using Microsoft.AspNetCore.SignalR;
using Neji_Meteo.Controllers;
using System.Threading.Tasks;

namespace Neji_Meteo.Hubs
{
    public class WeatherHub : Hub
    {
        public async Task SendWeatherData()
        {
            var data = WeatherController.GetData();
            await Clients.All.SendAsync("ReceiveWeatherData", data);
        }
    }
}
