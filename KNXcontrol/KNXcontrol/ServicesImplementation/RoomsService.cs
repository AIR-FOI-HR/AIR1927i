using Flurl.Http;
using KNXcontrol.Configuration;
using KNXcontrol.Models;
using KNXcontrol.Services;
using System.Threading.Tasks;

namespace KNXcontrol.ServicesImplementation
{
    public class RoomsService : IRoomsService
    {
        public async Task<bool> AddRoom(Room room)
        {
            try
            {
                var response = await (Config.ServiceBase + "add_room").PostJsonAsync(new { data = room });
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
