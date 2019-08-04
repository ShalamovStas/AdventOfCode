using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace Day15
{
    public class GuideService
    {
        private int MinWayLength;
        public Board Board { get; set; }

        //startNodes - точки из которых нужно проложить путь
        //targetNodes - в которые нужно проложить путь
        //unitsList - список юнитов - через эти точки путь прокладывать нельзя
        public List<Branch> FindWaysFromCurrentUnit(Node startNode, Node[] targetNodes, Node[] nodesWithUnits)
        {
            MinWayLength = 0;
            List<Point> branchWay = new List<Point>();
            List<Branch> successBranchList = new List<Branch>();
            Point[] avoidThisPoint = nodesWithUnits.Select(p => p.Point).ToArray();

            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();

            foreach (var targetNode in targetNodes)
            {
                foreach (var topLeftRightButtomNode in startNode.Sides)
                {
                    if (topLeftRightButtomNode != null)
                    {
                        List<Point> nextWay = branchWay.ToList();
                        ExploreSide(topLeftRightButtomNode, targetNode, successBranchList, nextWay, avoidThisPoint);
                    }
                }
            }
           // stopwatch.Stop();
           // Console.WriteLine($"ElapsedTime: {stopwatch.Elapsed}");
           // PrintWay(successBranchList);

            return successBranchList;
        }

        internal void MoveUnit(Board board, Node currentNode, Point moveToThisPoint)
        {
            Unit unit = currentNode.Unit;
            currentNode.Unit = null;
            var nextNode = board.Nodes.Where(n => n.Point.Equals(moveToThisPoint)).First();
            nextNode.Unit = unit;
        }

        public Point GetNextNodeToMove(Point currentPosition, List<Branch> avaliableWays)
        {

            //MoveDestination = GetMoveDestination();
            var theShortestWaysList = avaliableWays
                .GroupBy(i => i.Way.Length)
                .OrderBy(i => i.Key)
                .First()
                .ToArray();


            var orderReading = GetOrderReadingForPoint(currentPosition);

            foreach (var orderReadingPoint in orderReading)
            {
                foreach (var currentWay in theShortestWaysList)
                {
                    var item = currentWay.Way.Where(p => p.Equals(orderReadingPoint)).Count();
                    if (item >= 1)
                        return orderReadingPoint;
                }
            }

            throw new Exception("Reading order exception") ;
        }

        public Point[] GetOrderReadingForPoint(Point current)
        {
            Point[] points = new Point[4] 
            {
                new Point(current.X, current.Y-1),
                new Point(current.X-1, current.Y),
                new Point(current.X+1, current.Y),
                new Point(current.X, current.Y+1),
            };

                return points;
        }

        private void ExploreSide(Node node, Node target, List<Branch> successPathList, List<Point> branchWay, Point[] avoidThisPoint)
        {
            if (MinWayLength != 0)
                if (branchWay.Count() > MinWayLength)
                    return;

            if (node.Point.Equals(target.Point))
            {
                branchWay.Add(node.Point);
                var branch = new Branch() { Way = branchWay.ToArray() };
                successPathList.Add(branch);
                MinWayLength = branchWay.Count();
                return;
            }

            if (branchWay.Where(p => p.Equals(node.Point)).Count() != 0)
                return;

            if (avoidThisPoint.Where(p => p.Equals(node.Point)).Count() != 0)
                return;

            branchWay.Add(node.Point);

            foreach (var side in node.Sides)
            {
                if (side != null)
                {
                    List<Point> nextWay = branchWay.ToList();
                    ExploreSide(side, target, successPathList, nextWay, avoidThisPoint);
                }
            }
        }

        private void PrintWay(List<Branch> successBranchList)
        {
            Console.BufferHeight = successBranchList.Count() + 50;
            //Print ways
            foreach (var branch in successBranchList)
            {
                Console.WriteLine("Branch");
                foreach (var point in branch.Way)
                {
                    Console.WriteLine($"[{point.X}, {point.Y}]");
                }
            }

            Console.WriteLine($"Count: {successBranchList.Count()}");
        }
    }
}
