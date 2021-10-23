using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryRobot.Implementations
{
    public class DataModel
    {
        public int[,] DistanceMatrix { get; set; }
        public int VehicleNumber { get; set; } = 1;
        public int[] Starts { get; set; } = { 1 };
        public int[] Ends { get; set; } = { 0 };

        public DataModel(int[,] Matrix)
        {
            DistanceMatrix = Matrix;
        }
    }
}
