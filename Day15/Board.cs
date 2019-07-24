using System;
using System.Collections.Generic;
using System.Text;

namespace Day15
{
    class Board
    {
        public List<Unit> Units { get; set; }
        public Board()
        {
            Units = new List<Unit>();
        }

    }

    class Unit
    {
        public char Name { get; set; }
        public int X{ get; set; }
        public int Y{ get; set; }
    }


    class Point
    {
        public int X{ get; set; }
        public int Y{ get; set; }

        public string MyProperty { get; set; }
    }

}
