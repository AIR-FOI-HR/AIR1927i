using System;

namespace Model.Models
{
    /// <summary>
    /// Class for storing data about rooms
    /// </summary>
    public class Room
    {
        public Guid _id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
