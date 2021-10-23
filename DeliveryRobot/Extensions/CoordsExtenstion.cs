using DeliveryRobot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryRobot.Extensions
{
    //Make matrix with distances between points(X,Y) from List(coordinates X Y) for next calculations
    public static class CoordsExtenstion
    {
        public static int[,] MakeMatrix(this IField field)
        {
            var Points = field.Points;
            var Matrix = new int[Points.Count + 1, Points.Count + 1];
            // Make matrix with first row and column of zeros for finding optimal path using Algotrithm that solver Travelling Salesman Problem
            for (var i = 0; i < Points.Count + 1; i++)
            {
                for (var j = 0; j < Points.Count + 1; j++)
                {
                    Matrix[0, j] = 0;
                    Matrix[i, 0] = 0;
                }
            }
            //Compute count of steps(X,Y) from one point to others and make Matrix
            for (var i = 1; i < Points.Count + 1; i++)
            {
                for (var j = 1; j < Points.Count + 1; j++)
                {
                    Matrix[i, j] = Math.Abs(Points[j - 1].Item1 - Points[i - 1].Item1) + Math.Abs(Points[j - 1].Item2 - Points[i - 1].Item2);
                }
            }
            return Matrix;
        }
    }
}
