using System;
using System.Collections.Generic;
using System.Text;

namespace Day15
{
    class PrinterService
    {
        public void PrintNode(Node node)
        {
            Console.SetCursorPosition(node.Point.X, node.Point.Y);
            if (node.Unit == null)
            {
                Console.Write(".");
                return;
            }

            if (node.Unit.Label == UnitLabel.G)
                Console.ForegroundColor = ConsoleColor.DarkRed;
            else
                Console.ForegroundColor = ConsoleColor.DarkGreen;

            Console.Write(node.Unit.Label.ToString());
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void PrintBoard(Board board)
        {
            foreach (var node in board.Nodes)
            {
                PrintNode(node);
            }
        }
    }
}
