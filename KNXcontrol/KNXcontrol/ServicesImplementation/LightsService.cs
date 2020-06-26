using Flurl.Http;
using KNXcontrol.Configuration;
using KNXcontrol.Models;
using KNXcontrol.Services;
using System;
using System.Threading.Tasks;

namespace KNXcontrol.ServicesImplementation
{
    /// <summary>
    /// Lights control implementation methods
    /// </summary>
    public class LightsService : ILightsService
    {
        /// <summary>
        /// Sets the brightness of the selected dimmable KNX object 
        /// </summary>
        /// <param name="knxObject"></param>
        /// <returns></returns>
        public async Task Dim(KnxObject knxObject)
        {
            try
            {
                var response = await(Config.ServiceBase + "dim").PostJsonAsync(new { data = knxObject });
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// Switches the state of selected KNX object - on/off
        /// </summary>
        /// <param name="knxObject"></param>
        /// <returns></returns>
        public async Task Switch(KnxObject knxObject)
        {
            try
            {
                var response = await(Config.ServiceBase + "switch").PostJsonAsync(new { data = knxObject });
            }
            catch (Exception ex)
            {
            }
        }
    }
}
