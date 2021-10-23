using System;
using System.Collections.Generic;
using Google.OrTools.ConstraintSolver;

namespace DeliveryRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            IField field = new Field();
            field.Parsing("5x5(5, 5)(4, 4)(2, 1)(1, 5)(0, 1)");
            DataModel model = new(field.MakeMatrix());
            Robot robot = new();
            List<int> OptimalPath = Algorithm.FindOptimalWay(model);
            robot.Delivery(field, OptimalPath);
        }
    }

    public interface IField
    {
        int Height { get;  }
        int Width { get; }
        List<Tuple<int, int>> Points { get;  }
        void Parsing(string data);
    }

    public class Field : IField
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Tuple<int, int>> Points { get; set; }

        //Parsing input string in List (X,Y)
        public void Parsing (string data)
        {
            Points = new();
            //Delete last ) and whitespaces for good Split
            string subdata = data.Replace(" ", "").Trim( ')' );
            string[] array = subdata.Split(new string[] { ")(", ",", "(", ")","x"}, StringSplitOptions.None);
            int[] arrPoints = Array.ConvertAll(array, int.Parse);
            Width = arrPoints[0];
            Height = arrPoints[1];
            //First item1 - x point, item2 - y point
            for(int i = 2; i < arrPoints.Length; i=i+2)
            {
                //If first element not (0,0) so add it
                if (i == 2 && arrPoints[i] != 0 && arrPoints[i + 1] != 0)
                {
                    Points.Add(Tuple.Create(0, 0));
                }
                    Points.Add(Tuple.Create(arrPoints[i], arrPoints[i + 1]));
            }
        }
    }

    public class Robot
    {
        public IField Field { get; set; }
        public List<int> OptimalPath;
        // Delivery to all points
        public void Delivery(IField field, List<int> optimalpath)
        {
            Field = field;
            OptimalPath = optimalpath;
            List<Tuple<int, int>> Points = Field.Points;
            int x = 0;
            int y = 0;
            for (int i = 0; i < OptimalPath.Count - 1; i++)
            {
                x = Points[OptimalPath[i + 1] - 1].Item1 - Points[OptimalPath[i] - 1].Item1;
                y = Points[OptimalPath[i + 1] - 1].Item2 - Points[OptimalPath[i] - 1].Item2;
                if (x > 0)
                    MoveRight(x);
                else if (x < 0)
                    MoveLeft(x);
                if (y > 0)
                    MoveTop(y);
                else if (y < 0)
                    MoveBot(y);
                Console.Write("D");
            }

        }

        private void MoveTop(int y)
        {
            for (int i = 0; i < y; i++)
                Console.Write("N");
        }

        private void MoveBot(int y)
        {
            for (int i = 0; i < Math.Abs(y); i++)
                Console.Write("S");
        }

        private void MoveRight(int x)
        {
            for (int i = 0; i < x; i++)
                Console.Write("E");
        }

        private void MoveLeft(int x)
        {
            for (int i = 0; i < Math.Abs(x); i++)
                Console.Write("W");
        }
    }

    //Make matrix with distances between points(X,Y) from List(coordinates X Y) for next calculations
    public static class CoordsExtension
    {
        private static List<Tuple<int, int>> Points;
        private static int[,] Matrix;

        public static int[,] MakeMatrix(this IField field)
        {
            Points = field.Points;
            Matrix = new int[Points.Count+1, Points.Count+1];
            // Make matrix with first row and column of zeros for finding optimal path
            for (int i = 0; i < Points.Count + 1; i++)
            {
                for (int j = 0; j < Points.Count + 1; j++)
                {
                    Matrix[0,j] = 0;
                    Matrix[i,0] = 0;
                }
            }
            //Compute count of steps(X,Y) from one point to others and make Matrix
            for (int i = 1; i < Points.Count + 1; i++)
            {
                for (int j = 1; j < Points.Count + 1; j++)
                {
                        
                        Matrix[i, j] = Math.Abs(Points[j-1].Item1 - Points[i-1].Item1) + Math.Abs(Points[j-1].Item2 - Points[i-1].Item2);
                }
            }

            return Matrix;
        }
    }

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

    //Finding the optimal path going through points by Algorithm for solving Travling Salesperson Problem using Google OR-Tolls
    public class Algorithm
    {
        //Return optimal(minimal) path
        static List<int> ReturnPath(in RoutingModel routing, in RoutingIndexManager manager, in Assignment solution, in DataModel model)
        {
            List<int> arrayofOptimalpoints = new();
            long routeDistance = 0;
            var index = routing.Start(0);

            while (routing.IsEnd(index) == false)
            {
                arrayofOptimalpoints.Add(manager.IndexToNode((int)index));
                var previousIndex = index;
                index = solution.Value(routing.NextVar(index));
                routeDistance += routing.GetArcCostForVehicle(previousIndex, index, 0);
            }
            //Check optimal path bellow
            foreach (var elem in arrayofOptimalpoints)
                Console.WriteLine(elem);
            return arrayofOptimalpoints;
        }
        //Algorithm solve TSP using Google OR-Tools
        public static List<int> FindOptimalWay(DataModel model)
        {
            DataModel data = model;

            RoutingIndexManager manager = new RoutingIndexManager(data.DistanceMatrix.GetLength(0), data.VehicleNumber, data.Starts, data.Ends);

            RoutingModel routing = new RoutingModel(manager);

            int transitCallbackIndex = routing.RegisterTransitCallback((long fromIndex, long toIndex) => {
                //Convert from routing variable Index to distance matrix NodeIndex.
                var fromNode = manager.IndexToNode(fromIndex);
                var toNode = manager.IndexToNode(toIndex);
                return data.DistanceMatrix[fromNode, toNode];
            });

            //Define cost 
            routing.SetArcCostEvaluatorOfAllVehicles(transitCallbackIndex);

            RoutingSearchParameters searchParameters =
                operations_research_constraint_solver.DefaultRoutingSearchParameters();
            searchParameters.FirstSolutionStrategy = FirstSolutionStrategy.Types.Value.PathCheapestArc;

            Assignment solution = routing.SolveWithParameters(searchParameters);
            //Return array with optimal path 
            return ReturnPath(routing, manager, solution, data);
        }
    }

}
