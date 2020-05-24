using Flurl.Http;
using KNXcontrol.Configuration;
using KNXcontrol.Models;
using KNXcontrol.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KNXcontrol.ServicesImplementation
{
    public class RoomsService : IRoomsService
    {
        public async Task<bool> AddRoom(Room room)
        {
            try
            {
                room._id = Guid.NewGuid().ToString();
                var response = await (Config.ServiceBase + "add-room").PostJsonAsync(new { data = room });
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Room>> RoomsOverview()
        {
            try
            {
                return await (Config.ServiceBase + "get-rooms").GetJsonAsync<List<Room>>();
            }
            catch(Exception ex)
            {
                return new List<Room>();
            }
        }

        public async Task<bool> DeleteRoom(string id)
        {
            try
            {
                var result = await (Config.ServiceBase + "delete-room").PostJsonAsync(new { id = id });
                return result.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateRoom(Room room)
        {
            try
            {
                var response = await(Config.ServiceBase + "update-room").PostJsonAsync(new { data = room });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
