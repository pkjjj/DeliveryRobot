using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryRobot.Implementations
{
    public static class GetPath
    {
        public static void PrintPath(Robot robot)
        {
            Console.WriteLine(robot.ResultPath);
        }

        public static string ReturnPath(Robot robot)
        {
            return robot.ResultPath;
        }
    }
}
