using System;

namespace KNXcontrol.Models
{
    /// <summary>
    /// Class for storing data abount KNX object types
    /// </summary>
    public class Type
    {
        public Guid _id { get; set; }
        public string Name { get; set; }
        public string DefaultValue { get; set; }
        public string Description { get; set; }
    }
}
