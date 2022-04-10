using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
   public class StartMovingRequest
    {
     
        public int AreaX { get; set; }
        public int AreaY { get; set; }
        public string Command { get; set; }
    }
}
