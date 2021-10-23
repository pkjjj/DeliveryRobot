using System;
using Xunit;
using DeliveryRobot;
using System.Collections.Generic;

namespace UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            IField field = new Field();
            field.Parsing("5x5 (0, 0) (1, 3) (4,4) (4, 2) (4, 2) (0, 1) (3, 2) (2, 3) (4, 1) (4,0) (3, 0) (3, 1)");
            DataModel model = new(field.MakeMatrix());
            Robot robot = new();
            List<int> OptimalPath = Algorithm.FindOptimalWay(model);
            robot.Delivery(field, OptimalPath);
            string result = Console.ReadLine();
            Assert.Equal("NDEEEDSDEDNDNDDWDWNDWDEEEND", result);

            field.Parsing("5x5 (1, 3) (4, 4)");
            model = new(field.MakeMatrix());
            OptimalPath = Algorithm.FindOptimalWay(model);
            robot.Delivery(field, OptimalPath);
            result = Console.ReadLine();
            Assert.Equal("ENNNDEEEND", result);

            field.Parsing("5x5 (5, 5) (4, 4)(2,1)  (1,5) (0,1)");
            model = new(field.MakeMatrix());
            OptimalPath = Algorithm.FindOptimalWay(model);
            robot.Delivery(field, OptimalPath);
            result = Console.ReadLine();
            Assert.Equal("NDEEDWNNNNDEEESDEND", result);

            field.Parsing("5x5 (0, 0) (1, 3) (4,4) (4, 2) (4, 2) (0, 1) (3, 2) (2, 3) (4, 1) (4,0) (3, 0) (3, 1)");
            Assert.Equal("(0,0)", field.Points[0].ToString());
            Assert.Equal("(3,1)", field.Points[11].ToString());
            Assert.Equal("(3,2)", field.Points[6].ToString());


        }
    }
}
