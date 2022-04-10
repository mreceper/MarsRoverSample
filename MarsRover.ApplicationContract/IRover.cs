using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
   public interface IRover
    {
        void StartMoving(StartMovingRequest startMovingRequest);
        void StartPosition(StartPositionRequest startPositionRequest);
        void Output();
        string CurrentRoverPositon();
    }
}
