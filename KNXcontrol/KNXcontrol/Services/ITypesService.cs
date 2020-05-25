using KNXcontrol.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KNXcontrol.Services
{
    public interface ITypesService
    {

        Task<bool> AddType(Type Type);
        Task<List<Type>> TypesOverview();
        Task<bool> UpdateType(Type Type);
        Task<bool> DeleteType(string id);
    }
}
