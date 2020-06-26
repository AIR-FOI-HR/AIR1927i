using KNXcontrol.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KNXcontrol.Services
{
    /// <summary>
    /// Interface used to define required methods for KNX objects CRUD
    /// </summary>
    public interface IKnxObjectsService
    {

        Task<bool> AddKnxObject(KnxObject room);
        Task<List<KnxObject>> KnxObjectsOverview();
        Task<bool> UpdateKnxObject(KnxObject room);
        Task<bool> DeleteKnxObject(string id);
    }
}
