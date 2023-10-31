using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reduceGlare.Configuration
{
    public class settings
    {
        public static settings Instance = new settings();
        public bool modEnable = true;
        public bool playersPlace = true;
        public bool reduceGlare = true;
    }
}
