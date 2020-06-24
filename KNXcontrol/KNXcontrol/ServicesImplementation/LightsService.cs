using Flurl.Http;
using KNXcontrol.Configuration;
using KNXcontrol.Models;
using KNXcontrol.Services;
using System;
using System.Threading.Tasks;

namespace KNXcontrol.ServicesImplementation
{
    public class LightsService : ILightsService
    {
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
