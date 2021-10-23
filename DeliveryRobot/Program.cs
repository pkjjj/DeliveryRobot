using System;
using System.Collections.Generic;
using DeliveryRobot.Extensions;
using DeliveryRobot.Implementations;
using DeliveryRobot.Interfaces;
using DeliveryRobot.Services;
using Google.OrTools.ConstraintSolver;

namespace DeliveryRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            IField field = new Field();
            field.Parse("5x5(5, 5)(4, 4)(2, 1)(1, 5)(0, 1)");
            var model = new DataModel(field.MakeMatrix());
            //check elements
            foreach (var elem in field.Points)
            {
                Console.Write(elem);
            }
            Console.WriteLine();
            var robot = new Robot();
            var OptimalPath = Algorithm.FindOptimalWay(model);
            //Check optimal path bellow
            Console.WriteLine("Optimal path:");
            foreach (var elem in OptimalPath)
            {
                if (elem != OptimalPath[OptimalPath.Count - 1])
                    Console.Write(elem + "->");
                else
                    Console.Write(elem);
            }
            Console.WriteLine();
            robot.Delivery(field, OptimalPath);
            Console.WriteLine(GetPath.ReturnPath(robot));
        }
    }
    
}

