using DeliveryRobot.Interfaces;
using DeliveryRobot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryRobot.Implementations
{
    public class Robot
    {
        public string ResultPath { get; set; }
        // Delivery to all points
        public void Delivery(IField Field, List<int> Optimalpath)
        {
            var result = String.Empty;
            var Points = Field.Points;
            var x = 0;
            var y = 0;
            for (var i = 0; i < Optimalpath.Count - 1; i++)
            {
                x = Points[Optimalpath[i + 1] - 1].Item1 - Points[Optimalpath[i] - 1].Item1;
                y = Points[Optimalpath[i + 1] - 1].Item2 - Points[Optimalpath[i] - 1].Item2;
                if (x > 0)
                    MovingService.MoveRight(x , ref result);
                else if (x < 0)
                    MovingService.MoveLeft(x , ref result);
                if (y > 0)
                    MovingService.MoveTop(y, ref result);
                else if (y < 0)
                    MovingService.MoveBot(y, ref result);
                MovingService.Delivered(ref result);
            }
            ResultPath = result;
        }
    }
}
