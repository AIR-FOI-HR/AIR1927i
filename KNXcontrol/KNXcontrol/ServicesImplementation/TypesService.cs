using Flurl.Http;
using KNXcontrol.Configuration;
using KNXcontrol.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Type = KNXcontrol.Models.Type;

namespace KNXcontrol.ServicesImplementation
{
    public class TypesService : ITypesService
    {
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
