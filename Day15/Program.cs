﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day15
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = InitBoard();

            List<Node> targetNodes = FindPoints(board);



        }

        private static List<Node> FindPoints(Board board)
        {
            List<Node> targetNodes = new List<Node>();

            var nodes = board.Nodes
                .Where(n => n.Unit != null)
                .Where(n => n.Unit.Name == 'G').ToList();

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

            return targetNodes;
        }

        private static void PrintBoard(Board board)
        {
            int lineIndex = 0;
            foreach (var unit in board.Nodes)
            {
                if (unit.Y > lineIndex)
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

        private static Board InitBoard()
        {
            //string[] input = File.ReadAllLines("input.txt");
            string[] input = File.ReadAllLines("in1.txt");
            Board board = new Board();

            for (int y = 0; y < input.Length; y++)
            {
                var line = input[y];
                for (int x = 0; x < line.Length; x++)
                {
                    if (line[x] == '#')
                        continue;

                    Node node = new Node() { X = x, Y = y, Symbol = '.' };

                    if (line[x] == 'G' || line[x] == 'E')
                        node.Unit = new Unit() { X = x, Y = y, Name = line[x] };

                    board.Nodes.Add(node);
                }
            }

            foreach (var node in board.Nodes)
            {
                node.Top = board.Nodes
                    .Where(n => n.X == node.X && n.Y == node.Y - 1 && n.Symbol != '#')
                    .FirstOrDefault();

                node.Bottom = board.Nodes
                    .Where(n => n.X == node.X && n.Y == node.Y + 1 && n.Symbol != '#')
                    .FirstOrDefault();

                node.Left = board.Nodes
                    .Where(n => n.X == node.X + 1 && n.Y == node.Y && n.Symbol != '#')
                    .FirstOrDefault();

                node.Right = board.Nodes
                    .Where(n => n.X == node.X - 1 && n.Y == node.Y && n.Symbol != '#')
                    .FirstOrDefault();
            }

            return board;
        }



    }
}
