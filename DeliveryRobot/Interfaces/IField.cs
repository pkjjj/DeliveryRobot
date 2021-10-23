using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryRobot.Interfaces
{
    public interface IField
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public List<Tuple<int, int>> Points { get; set; }
        public void Parse(string data);
    }
}
