using ConnectorModule;
using Flurl.Http;
using Model.Configuration;
using Model.Models;
using Model.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Type = Model.Models.Type;

[assembly: Xamarin.Forms.Dependency(typeof(Connector))]
namespace ConnectorModule
{
    public class Connector : IConnector
    {
        public Connector()
        {

        }
        /// <summary>
        /// Adds new room to the database
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public async Task<bool> AddRoom(Room room)
        {
            try
            {
                room._id = Guid.NewGuid();
                var response = await (Config.ServiceBase + "add-room").PostJsonAsync(new { data = room });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// Returns all rooms from the database
        /// </summary>
        /// <returns></returns>
        public async Task<List<Room>> RoomsOverview()
        {
            try
            {
                return await (Config.ServiceBase + "get-rooms").GetJsonAsync<List<Room>>();
            }
            catch (Exception ex)
            {
                return new List<Room>();
            }
        }
        /// <summary>
        /// Deletes the room with selected id from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Updates selected room data
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public async Task<bool> UpdateRoom(Room room)
        {
            try
            {
                var response = await (Config.ServiceBase + "update-room").PostJsonAsync(new { data = room });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Method that is used to move the blinds up/down
        /// </summary>
        /// <param name="knxObject"></param>
        /// <returns></returns>
        public async Task Move(KnxObject knxObject)
        {
            try
            {
                var response = await (Config.ServiceBase + "move").PostJsonAsync(new { data = knxObject });
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
                var response = await (Config.ServiceBase + "rotate").PostJsonAsync(new { data = knxObject });
            }
            catch (Exception ex)
            {
            }
        }

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
                var response = await (Config.ServiceBase + "add-knx-object").PostJsonAsync(new { data = knxObject });
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
                var result = await (Config.ServiceBase + "delete-knx-object").PostJsonAsync(new { id = id });
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

        /// <summary>
        /// Sets the brightness of the selected dimmable KNX object 
        /// </summary>
        /// <param name="knxObject"></param>
        /// <returns></returns>
        public async Task Dim(KnxObject knxObject)
        {
            try
            {
                var response = await (Config.ServiceBase + "dim").PostJsonAsync(new { data = knxObject });
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
                var response = await (Config.ServiceBase + "switch").PostJsonAsync(new { data = knxObject });
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Adds new object type to the database
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<bool> AddType(Type type)
        {
            try
            {
                type._id = Guid.NewGuid();
                var response = await (Config.ServiceBase + "add-type").PostJsonAsync(new { data = type });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// Returns all object types from the database
        /// </summary>
        /// <returns></returns>
        public async Task<List<Type>> TypesOverview()
        {
            try
            {
                return await (Config.ServiceBase + "get-types").GetJsonAsync<List<Type>>();
            }
            catch (Exception ex)
            {
                return new List<Type>();
            }
        }
        /// <summary>
        /// Delete the type with selected id from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteType(string id)
        {
            try
            {
                var result = await (Config.ServiceBase + "delete-type").PostJsonAsync(new { id = id });
                return result.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// Updates the data of the selected type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<bool> UpdateType(Type type)
        {
            try
            {
                var response = await (Config.ServiceBase + "update-type").PostJsonAsync(new { data = type });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
