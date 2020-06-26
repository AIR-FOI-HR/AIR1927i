using KNXcontrol.Models;
using System.Threading.Tasks;

namespace KNXcontrol.Services
{
    /// <summary>
    /// Interface used to define required methods for lights control
    /// </summary>
    public interface ILightsService
    {
        Task Dim(KnxObject knxObject);
        Task Switch(KnxObject knxObject);
    }
}
