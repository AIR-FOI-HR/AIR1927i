using KNXcontrol.Models;
using System.Threading.Tasks;

namespace KNXcontrol.Services
{
    public interface IRoomsService
    {
        Task<bool> AddRoom(Room room);
    }
}
