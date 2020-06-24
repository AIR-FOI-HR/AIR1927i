using System;

namespace KNXcontrol.Models
{
    public class KnxObject
    {
        public Guid _id { get; set; }
        public string Address { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public string DPT { get; set; }
        public Room Room { get; set; }
        public Type Type { get; set; }
    }
}
