using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day15
{
    class Engene
    {
        private BoardExplorerService boardExplorerService;
        private GuideService guideService;
        private PrinterService printerService;

        private Node[] nodesWithUnits;
        private Board board;

        public void Run()
        {
            boardExplorerService = new BoardExplorerService();
            board = boardExplorerService.InitBoard();

            guideService = new GuideService() { Board = board };
            printerService = new PrinterService();

            printerService.PrintBoard(board);

            nodesWithUnits = board.Nodes.Where(p => p.Unit != null).ToArray();

            RunRound();
           
            Console.ReadKey();
        }

        private void RunRound()
        {
            foreach (var node in nodesWithUnits)
            {
                var oppositeUnitLabel = GetOppositeUnitLabel(node.Unit.Label);
                Node[] targetNodes = boardExplorerService.GetTargetNodes(board, oppositeUnitLabel);
                var avaliableWays = guideService.FindWaysFromCurrentUnit(node, targetNodes, nodesWithUnits);
                var moveToThisPoint = guideService.GetNextNodeToMove(node.Point, avaliableWays);
                guideService.MoveUnit(board, node, moveToThisPoint);

                printerService.PrintBoard(board);
                Console.ReadKey();
            }


        }

        private UnitLabel GetOppositeUnitLabel(UnitLabel currentUnitLabel)
        {
            if (currentUnitLabel == UnitLabel.E)
                return UnitLabel.G;
            else
                return UnitLabel.E;
        }
    }
}
