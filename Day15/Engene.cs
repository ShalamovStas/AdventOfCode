using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day15
{
    class Engene
    {
        private BoardExplorerService boardExplorerService = new BoardExplorerService();
        private GuideService guideService = new GuideService();

        public void Run()
        {
            Board board = boardExplorerService.InitBoard();
            Node[] targetNodes = boardExplorerService.GetTargetNodes(board, 'G');

            Node[] unitsList = board.Nodes.Where(p => p.Unit != null).ToArray();

            Node startNode = board.Nodes
                .Where(n => n.Unit != null)
                .Where(n => n.Unit.Name == 'E').First();

            guideService.Board = board;
            var avaliableWays = guideService.FindWaysFromCurrentUnit(startNode, targetNodes, unitsList);

        }
    }
}
