using System;
using System.Collections.Generic;
using System.Text;

namespace Day15
{
    class Board
    {
        public List<Node> Nodes { get; set; }
        public Board()
        {
            Nodes = new List<Node>();
        }
    }

    class Unit
    {
        public char Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Symbol { get; set; }
        public Unit Unit { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Node Top { get; set; }
        public Node Bottom { get; set; }
        public Node[] Sides
        {
            get
            {
                return new Node[] { Left, Right, Top, Bottom };
            }
        }
    }

    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    class Branch
    {
        public List<Point> Way { get; set; }
        public Branch()
        {
            Way = new List<Point>();
        }
    }

}
