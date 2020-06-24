using KNXcontrol.Models;
using System.Threading.Tasks;

namespace KNXcontrol.Services
{
    public interface ILightsService
    {
        Task Dim(KnxObject knxObject);
        Task Switch(KnxObject knxObject);
    }
}
