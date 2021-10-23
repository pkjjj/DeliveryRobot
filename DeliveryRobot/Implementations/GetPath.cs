using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryRobot.Implementations
{
    public static class GetPath
    {
        public static string ReturnPath(Robot robot)
        {
            return robot.ResultPath;
        }
    }
}
