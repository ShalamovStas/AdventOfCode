using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day15
{
    public class BoardExplorerService
    {
        
        // Summary:
        //     Find points next to the units: 
        //
        //      #######
        //      #E....#
        //      #..?#?#
        //      #.?G#G#
        //      #######
        //
        public  Node[] GetTargetNodes(Board board, char unitLabel)
        {
            List<Node> targetNodes = new List<Node>();

            var nodes = board.Nodes
                .Where(n => n.Unit != null)
                .Where(n => n.Unit.Name == unitLabel).ToList();

            foreach (var node in nodes)
            {
                if (node.Top != null)
                    targetNodes.Add(node.Top);
                if (node.Bottom != null)
                    targetNodes.Add(node.Bottom);
                if (node.Right != null)
                    targetNodes.Add(node.Right);
                if (node.Left != null)
                    targetNodes.Add(node.Left);
            }
            return targetNodes.ToArray();
        }

        public Board InitBoard()
        {
            //string[] input = File.ReadAllLines("input.txt");
            string[] input = File.ReadAllLines("../../../in2.txt");
            Board board = new Board();

            for (int y = 0; y < input.Length; y++)
            {
                var line = input[y];
                for (int x = 0; x < line.Length; x++)
                {
                    if (line[x] == '#')
                        continue;

                    Node node = new Node() { Point = new Point(x, y), Symbol = '.' };

                    if (line[x] == 'G' || line[x] == 'E')
                        node.Unit = new Unit() { Point = new Point(x, y), Name = line[x] };

                    board.Nodes.Add(node);
                }
            }

            foreach (var node in board.Nodes)
            {
                node.Top = board.Nodes
                    .Where(n => n.Point.X == node.Point.X && n.Point.Y == node.Point.Y - 1 && n.Symbol != '#')
                    .FirstOrDefault();

                node.Bottom = board.Nodes
                    .Where(n => n.Point.X == node.Point.X && n.Point.Y == node.Point.Y + 1 && n.Symbol != '#')
                    .FirstOrDefault();

                node.Left = board.Nodes
                    .Where(n => n.Point.X == node.Point.X + 1 && n.Point.Y == node.Point.Y && n.Symbol != '#')
                    .FirstOrDefault();

                node.Right = board.Nodes
                    .Where(n => n.Point.X == node.Point.X - 1 && n.Point.Y == node.Point.Y && n.Symbol != '#')
                    .FirstOrDefault();
            }

            return board;
        }

        public static void PrintBoard(Board board)
        {
            int lineIndex = 0;
            foreach (var unit in board.Nodes)
            {
                if (unit.Point.Y > lineIndex)
                {
                    Console.WriteLine();
                    lineIndex++;
                }
                if (unit.Unit != null || unit.Unit.Name == 'G')
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(unit.Symbol);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                if (unit.Unit != null || unit.Unit.Name == 'E')
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(unit.Symbol);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.Write(unit.Symbol);
                }
            }
        }
    }
}
