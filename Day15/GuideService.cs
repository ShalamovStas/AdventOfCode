using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Day15
{
    public class GuideService
    {
        public int MinWayLength { get; private set; }
        public Board Board { get; set; }

        //startNodes - точки из которых нужно проложить путь
        //targetNodes - в которые нужно проложить путь
        //unitsList - список юнитов - через эти точки путь прокладывать нельзя
        public List<Branch> FindWaysFromCurrentUnit(Node startNode, Node[] targetNodes, Node[] unitsList)
        {
            List<Point> branchWay = new List<Point>();
            List<Branch> successBranchList = new List<Branch>();
            Point[] avoidThisPoint = unitsList.Select(p => p.Point).ToArray();

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
            PrintWay(successBranchList);

            return successBranchList;
        }



        private void ExploreSide(Node node, Node target, List<Branch> successPathList, List<Point> branchWay, Point[] avoidThisPoint)
        {
            //if (MinWayLength != 0)
            //    if (branchWay.Count() > MinWayLength)
            //        return;

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
            //Print ways
            foreach (var branch in successBranchList)
            {
                Console.WriteLine("Branch");
                foreach (var point in branch.Way)
                {
                    Console.WriteLine($"[{point.X}, {point.Y}]");
                }
            }

        }
    }
}
