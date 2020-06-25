using Flurl.Http;
using KNXcontrol.Configuration;
using KNXcontrol.Models;
using KNXcontrol.Services;
using System;
using System.Threading.Tasks;

namespace KNXcontrol.ServicesImplementation
{
    public class BlindsService : IBlindsService
    {
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
