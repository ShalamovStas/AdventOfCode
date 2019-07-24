using System;
using System.IO;

namespace Day15
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = InitBoard();
            PrintBoard(board);



        }

        private static void PrintBoard(Board board)
        {
            int lineIndex = 0;
            foreach (var unit in board.Units)
            {
                if (unit.Y > lineIndex)
                {
                    Console.WriteLine();
                    lineIndex++;
                }
                if (unit.Name == 'G')
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(unit.Name);
                    Console.ForegroundColor = ConsoleColor.White;
                }else
                if (unit.Name == 'E')
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(unit.Name);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.Write(unit.Name);
                }
            }
        }

        private static Board InitBoard()
        {
            string[] input = File.ReadAllLines("input.txt");
            Board board = new Board();


            for (int y = 0; y < input.Length; y++)
            {
                var line = input[y];
                for (int x = 0; x < line.Length; x++)
                    board.Units.Add(new Unit() { X = x, Y = y, Name = line[x] });
            }
            return board;
        }



    }
}
