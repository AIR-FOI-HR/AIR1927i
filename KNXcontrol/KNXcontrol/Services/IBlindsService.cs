using KNXcontrol.Models;
using System.Threading.Tasks;

namespace KNXcontrol.Services
{
    public interface IBlindsService
    {
        Task Move(KnxObject knxObject);
        Task Rotate(KnxObject knxObject);
    }
}
