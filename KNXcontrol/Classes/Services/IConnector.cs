﻿using Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Model.Services
{
    /// <summary>
    /// Interface that defines methods that should be implemented and used to communicate with Raspberry Pi server
    /// </summary>
    public interface IConnector
    {
        Task Move(KnxObject knxObject);
        Task Rotate(KnxObject knxObject);
        Task<bool> AddKnxObject(KnxObject room);
        Task<List<KnxObject>> KnxObjectsOverview(); 
        Task<List<KnxObject>> KnxObjectsByRoom(string roomId);
        Task<bool> UpdateKnxObject(KnxObject room);
        Task<bool> DeleteKnxObject(string id);
        Task Dim(KnxObject knxObject);
        Task Switch(KnxObject knxObject);
        Task<bool> AddRoom(Room room);
        Task<List<Room>> RoomsOverview();
        Task<bool> UpdateRoom(Room room);
        Task<bool> DeleteRoom(string id);
        Task<bool> AddType(Type Type);
        Task<List<Type>> TypesOverview();
        Task<bool> UpdateType(Type Type);
        Task<bool> DeleteType(string id);
    }
}
