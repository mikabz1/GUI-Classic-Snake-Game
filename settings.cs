using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Snake_Game
{
    internal class settings
    {
        public static int width { get; set; }
        public static int hight { get; set; }
        public static string direction { get; set; }
        public settings()
        {
            width = 16;
            hight = 16;
            direction = "left";
        }


    }
}
