using KNXcontrol.Models;
using KNXcontrol.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KNXcontrol.ServicesImplementation
{
    public class KnxObjectsService : IKnxObjectsService
    {
        public Task<bool> AddKnxObject(KnxObject room)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteKnxObject(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<KnxObject>> KnxObjectsOverview()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateKnxObject(KnxObject room)
        {
            throw new System.NotImplementedException();
        }


    }
}
