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


            for (int i = 0; i < 3; i++)
            {
                nodesWithUnits = board.Nodes.Where(p => p.Unit != null).ToArray();
                RunRound();
            }

            Console.ReadKey();
        }

        private void RunRound()
        {
            foreach (var node in nodesWithUnits)
            {
                var oppositeUnitLabel = GetOppositeUnitLabel(node.Unit.Label);
                Node[] targetNodes = boardExplorerService.GetTargetNodes(board, oppositeUnitLabel);
                if (UnitReachTargetNode(node, targetNodes))
                    continue;
                var avaliableWays = guideService.FindWaysFromCurrentUnit(node, targetNodes, nodesWithUnits);
                var moveToThisPoint = guideService.GetNextNodeToMove(node.Point, avaliableWays);
                guideService.MoveUnit(board, node, moveToThisPoint);

                printerService.PrintBoard(board);
               Console.ReadKey();
            }


        }

        private bool UnitReachTargetNode(Node cuurentNode, Node[] targetNodes)
        {
            var currentPoint = cuurentNode.Point;
            
            Point[] nextPoints = guideService.GetOrderReadingForPoint(currentPoint);
            foreach (var node in nodesWithUnits)
            {
                if (node.Point.Equals(nextPoints[0]) || node.Point.Equals(nextPoints[1])
                    || node.Point.Equals(nextPoints[2]) || node.Point.Equals(nextPoints[3]))
                    return true;
            }
 
            return false;
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
