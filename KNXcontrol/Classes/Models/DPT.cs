using System.Collections.Generic;

namespace Model.Models
{
    /// <summary>
    /// Helper class with a list of available Data Point Types
    /// </summary>
    public class DPT
    {
        public List<string> DPTs { get; set; }

        public DPT()
        {
            DPTs = new List<string>() { "DPT1", "DPT2", "DPT3", "DPT4", "DPT5", "DPT6", "DPT7", "DPT8", "DPT9", "DPT10" };
        }
    }
}
