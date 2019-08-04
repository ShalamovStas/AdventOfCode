using System;
using System.Collections.Generic;
using System.Text;

namespace Day15
{
   public class Board
    {
        public List<Node> Nodes { get; set; }
        public Board()
        {
            Nodes = new List<Node>();
        }
    }

    public class Unit
    {
        public UnitLabel Label { get; set; }
        public Point Point { get; set; }
    }

    public class Node
    {
        public Point Point { get; set; }
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

    public struct Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class Branch
    {
        public Point[] Way { get; set; }
    }

    public enum UnitLabel
    {
        G = 0,
        E
    }
}
