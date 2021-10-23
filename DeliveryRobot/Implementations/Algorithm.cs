using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.OrTools.ConstraintSolver;

namespace DeliveryRobot.Implementations
{
    //Finding the optimal path going through points by Algorithm for solving Travling Salesperson Problem using Google OR-Tolls
    public class Algorithm
    {
        //Return optimal(minimal) path
        public static List<int> ReturnPath(RoutingModel routing, RoutingIndexManager manager, Assignment solution, DataModel model)
        {
            var arrayofOptimalpoints = new List<int>();
            long routeDistance = 0;
            var index = routing.Start(0);

            while (routing.IsEnd(index) == false)
            {
                arrayofOptimalpoints.Add(manager.IndexToNode((int)index));
                var previousIndex = index;
                index = solution.Value(routing.NextVar(index));
                routeDistance += routing.GetArcCostForVehicle(previousIndex, index, 0);
            }
            return arrayofOptimalpoints;
        }
        //Algorithm solve TSP using Google OR-Tools
        public static List<int> FindOptimalWay(DataModel model)
        {
            DataModel data = model;

            RoutingIndexManager manager = new RoutingIndexManager(data.DistanceMatrix.GetLength(0), data.VehicleNumber, data.Starts, data.Ends);

            RoutingModel routing = new RoutingModel(manager);

            int transitCallbackIndex = routing.RegisterTransitCallback((long fromIndex, long toIndex) =>
            {
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
