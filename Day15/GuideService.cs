using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day15
{
    public class GuideService
    {
        public List<Branch> FindWays(Node[] startNodes, Node[] targetNodes, Node[] unitsList)
        {
            Node first = targetNodes.First();
            List<Point> branchWay = new List<Point>();
            List<Branch> successBranchList = new List<Branch>();
            Point[] avoidThisPoint = unitsList.Select(p => p.Point).ToArray();

            foreach (var startNode in startNodes)
            {
                foreach (var topLeftRightButtomNode in startNode.Sides)
                {
                    if (topLeftRightButtomNode != null)
                        ExploreSide(topLeftRightButtomNode, first, successBranchList, branchWay, avoidThisPoint);
                }
            }

            return successBranchList;
        }

        private static void ExploreSide(Node node, Node target, List<Branch> successPathList, List<Point> branchWay, Point[] avoidThisPoint)
        {
            if (node.Point.Equals(target.Point))
            {
                var branch = new Branch() { Way = branchWay };
                branch.Way.Add(node.Point);
                successPathList.Add(branch);
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


    }
}
