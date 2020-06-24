using System.Collections.Generic;

namespace KNXcontrol.Models
{
    public class DPT
    {
        public List<string> DPTs { get; set; }

        public DPT()
        {
            DPTs = new List<string>() { "DPT 1", "DPT 2", "DPT 3", "DPT 4", "DPT 5", "DPT 6", "DPT 7", "DPT 8", "DPT 9", "DPT 10" };
        }
    }
}
