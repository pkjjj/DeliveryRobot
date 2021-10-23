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
            var robot = new Robot();
            var OptimalPath = Algorithm.FindOptimalWay(model);
            robot.Delivery(field, OptimalPath);
            GetPath.PrintPath(robot);
        }
    }
}

