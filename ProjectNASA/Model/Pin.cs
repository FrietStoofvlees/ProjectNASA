using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectNASA.Model
{
    public class Pin
    {
        public string Address { get; set; }
        public string Description { get; set; }
        public Location Location { get; set; }
    }
}
