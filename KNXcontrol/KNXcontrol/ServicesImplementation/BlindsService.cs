using Flurl.Http;
using KNXcontrol.Configuration;
using KNXcontrol.Models;
using KNXcontrol.Services;
using System;
using System.Threading.Tasks;

namespace KNXcontrol.ServicesImplementation
{
    /// <summary>
    /// Blinds control implementation methods
    /// </summary>
    public class BlindsService : IBlindsService
    {
        /// <summary>
        /// Method that is used to move the blinds up/down
        /// </summary>
        /// <param name="knxObject"></param>
        /// <returns></returns>
        public async Task Move(KnxObject knxObject)
        {
            try
            {
                var response = await(Config.ServiceBase + "move").PostJsonAsync(new { data = knxObject });
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// Method that is used to rotate the blinds 
        /// </summary>
        /// <param name="knxObject"></param>
        /// <returns></returns>
        public async Task Rotate(KnxObject knxObject)
        {
            try
            {
                var response = await(Config.ServiceBase + "rotate").PostJsonAsync(new { data = knxObject });
            }
            catch (Exception ex)
            {
            }
        }
    }
}
