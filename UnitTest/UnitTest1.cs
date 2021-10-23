using System;
using Xunit;
using DeliveryRobot;
using System.Collections.Generic;
using DeliveryRobot.Interfaces;
using DeliveryRobot.Implementations;
using DeliveryRobot.Extensions;
using System.Collections;

namespace UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void TestResultDeliver()
        {
            IField field = new Field();
            field.Parse("5x5 (0, 0) (1, 3) (4,4) (4, 2) (4, 2) (0, 1) (3, 2) (2, 3) (4, 1) (4,0) (3, 0) (3, 1)");
            DataModel model = new(field.MakeMatrix());
            Robot robot = new();
            List<int> OptimalPath = Algorithm.FindOptimalWay(model);
            robot.Delivery(field, OptimalPath);
            Assert.Equal("NDEEEDSDEDNDNDDWDWNDWDEEEND", robot.ResultPath);

            field.Parse("4x4 (1, 3) (4, 4)");
            model = new(field.MakeMatrix());
            OptimalPath = Algorithm.FindOptimalWay(model);
            robot.Delivery(field, OptimalPath);
            Assert.Equal("ENNNDEEEND", robot.ResultPath);

            field.Parse("5x5 (5, 5) (4, 4)(2,1)  (1,5) (0,1)");
            model = new(field.MakeMatrix());
            OptimalPath = Algorithm.FindOptimalWay(model);
            robot.Delivery(field, OptimalPath);
            
            Assert.Equal("NDEEDWNNNNDEEESDEND", robot.ResultPath);
        }
        [Fact]
        public void TestParsing()
        {
            IField field = new Field();
            field.Parse("5x5 (0, 0) (1, 3) (4,4) (4, 2) (4, 2) (0, 1) (3, 2) (2, 3) (4, 1) (4,0) (3, 0) (3, 1)");
            Assert.Equal("(0, 0)", field.Points[0].ToString());
            Assert.Equal("(3, 1)", field.Points[11].ToString());
            Assert.Equal("(3, 2)", field.Points[6].ToString());
        }

        [Fact]
        public void TestListOfCoordinates()
        {
            IField field = new Field();
            field.Parse("5x5 (5, 5) (4, 4)(2,1)  (1,5) (0,1)");
            field.MakeMatrix();
            var result = new List<Tuple<int, int>>
            {
                Tuple.Create( 0, 0 ),
                Tuple.Create( 5, 5 ),
                Tuple.Create( 4, 4 ),
                Tuple.Create( 2, 1 ),
                Tuple.Create( 1, 5 ),
                Tuple.Create( 0, 1 ),
            };
            Assert.Equal(result, field.Points);

            field.Parse("4x4 (1, 3) (4, 4)");
            field.MakeMatrix();
            result = new List<Tuple<int, int>>
            {
                Tuple.Create( 0, 0 ),
                Tuple.Create( 1, 3 ),
                Tuple.Create( 4, 4 ),
            };
            Assert.Equal(result, field.Points);

            field.Parse("3x3 (0,0 )(1, 3) (2, 2)");
            field.MakeMatrix();
            result = new List<Tuple<int, int>>
            {
                Tuple.Create( 0, 0 ),
                Tuple.Create( 1, 3 ),
                Tuple.Create( 2, 2 ),
            };
            Assert.Equal(result, field.Points);

        }
    }
}
