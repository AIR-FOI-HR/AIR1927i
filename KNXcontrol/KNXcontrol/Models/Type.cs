using System;

namespace KNXcontrol.Models
{
    public class Type
    {
        public Guid _id { get; set; }
        public string Name { get; set; }
        public string DefaultValue { get; set; }
        public string Description { get; set; }
    }
}
