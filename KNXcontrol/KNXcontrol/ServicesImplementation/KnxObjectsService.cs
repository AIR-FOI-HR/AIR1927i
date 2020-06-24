using Flurl.Http;
using KNXcontrol.Configuration;
using KNXcontrol.Models;
using KNXcontrol.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KNXcontrol.ServicesImplementation
{
    public class KnxObjectsService : IKnxObjectsService
    {
        public async Task<bool> AddKnxObject(KnxObject room)
        {
            try
            {
                room._id = Guid.NewGuid();
                var response = await(Config.ServiceBase + "add-knx-object").PostJsonAsync(new { data = room });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> DeleteKnxObject(string id)
        {
            try
            {
                var result = await(Config.ServiceBase + "delete-knx-object").PostJsonAsync(new { id = id });
                return result.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<KnxObject>> KnxObjectsOverview()
        {
            try
            {
                return await (Config.ServiceBase + "get-knx-objects").GetJsonAsync<List<KnxObject>>();
            }
            catch (Exception ex)
            {
                return new List<KnxObject>();
            }
        }

        public async Task<bool> UpdateKnxObject(KnxObject room)
        {
            try
            {
                var response = await (Config.ServiceBase + "update-knx-object").PostJsonAsync(new { data = room });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}
