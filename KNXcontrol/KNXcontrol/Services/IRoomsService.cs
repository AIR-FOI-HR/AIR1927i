using KNXcontrol.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KNXcontrol.Services
{
    public interface IRoomsService
    {
        Task<bool> AddRoom(Room room);
        Task<List<Room>> RoomsOverview();
        Task<bool> UpdateRoom(Room room);
        Task<bool> DeleteRoom(string id);
    }
}
