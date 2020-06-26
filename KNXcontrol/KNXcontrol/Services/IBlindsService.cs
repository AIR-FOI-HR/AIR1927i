using KNXcontrol.Models;
using System.Threading.Tasks;

namespace KNXcontrol.Services
{
    /// <summary>
    /// Interface used to define required methods for blinds control
    /// </summary>
    public interface IBlindsService
    {
        Task Move(KnxObject knxObject);
        Task Rotate(KnxObject knxObject);
    }
}
