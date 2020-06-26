using Flurl.Http;
using KNXcontrol.Configuration;
using KNXcontrol.Models;
using KNXcontrol.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KNXcontrol.ServicesImplementation
{
    /// <summary>
    /// KNX objects CRUD implementation
    /// </summary>
    public class KnxObjectsService : IKnxObjectsService
    {
        /// <summary>
        /// Adds a new KNX object to the database
        /// </summary>
        /// <param name="knxObject"></param>
        /// <returns></returns>
        public async Task<bool> AddKnxObject(KnxObject knxObject)
        {
            try
            {
                knxObject._id = Guid.NewGuid();
                var response = await(Config.ServiceBase + "add-knx-object").PostJsonAsync(new { data = knxObject });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes KNX object with selected id from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Method for retrieving all KNX objects from database
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Returns all KNX objects that are linked with the selected room
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public async Task<List<KnxObject>> KnxObjectsByRoom(string roomId)
        {
            try
            {
                return await (Config.ServiceBase + $"get-knx-objects/{roomId}").GetJsonAsync<List<KnxObject>>();
            }
            catch (Exception ex)
            {
                return new List<KnxObject>();
            }
        }
        /// <summary>
        /// Method for updating KNX object data
        /// </summary>
        /// <param name="knxObject"></param>
        /// <returns></returns>
        public async Task<bool> UpdateKnxObject(KnxObject knxObject)
        {
            try
            {
                var response = await (Config.ServiceBase + "update-knx-object").PostJsonAsync(new { data = knxObject });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}
