using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryRobot.Services
{
    public static class MovingService
    {
        public static void MoveTop(int y, ref string res)
        {
            for (var i = 0; i < y; i++)
                res += Constants.Top;
        }

        public static void MoveBot(int y, ref string res)
        {
            for (var i = 0; i < Math.Abs(y); i++)
                res += Constants.Bot;
        }

        public static void MoveRight(int x, ref string res)
        {
            for (var i = 0; i < x; i++)
                res += Constants.Right;
        }

        public static void MoveLeft(int x, ref string res)
        {
            for (var i = 0; i < Math.Abs(x); i++)
                res += Constants.Left;
        }

        public static void Delivered(ref string res)
        {
            res += Constants.Delivered;
        }
    }
}
