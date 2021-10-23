using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryRobot.Implementations
{
    public class DataModel
    {
        public int[,] DistanceMatrix;
        public int VehicleNumber = 1;
        public int[] Starts = { 1 };
        public int[] Ends = { 0 };

        public DataModel(int[,] Matrix)
        {
            DistanceMatrix = Matrix;
        }
    }
}
