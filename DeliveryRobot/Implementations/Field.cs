using DeliveryRobot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryRobot.Implementations
{
    public class Field : IField
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Tuple<int, int>> Points { get; set; }

        //Parse input string in List (X,Y)
        public void Parse(string data)
        {
            Points = new();
            //Delete last ) and whitespaces for good Split
            var subdata = data.Replace(" ", "").Trim(')');
            var array = subdata.Split(new string[] { ")(", ",", "(", ")", "x" }, StringSplitOptions.None);
            var arrPoints = Array.ConvertAll(array, int.Parse);
            Width = arrPoints[0];
            Height = arrPoints[1];
            //First item1 - x point, item2 - y point
            for (var i = 2; i < arrPoints.Length; i = i + 2)
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
}
