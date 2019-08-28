using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Program
    {
        private static List<char[]> pairs = new List<char[]>();
        private static List<Node> nodes = new List<Node>();

        static void Main()
        {
            LoadData();
            InitPoints();
            InitConnections();
            PrintNodes();

            Node firstNode = FindFirstNode();
            List<Node> args = new List<Node>();
            Processing(firstNode, args);

            Console.ReadKey();
        }

        private static Node FindFirstNode()
        {
            Node firstNode = nodes.First();
            foreach (var node in nodes)
            {
                if (node.Value < firstNode.Value)
                    firstNode = node;
            }
            return firstNode;
        }
        private static void PrintNodes()
        {
            foreach (var item in nodes)
            {
                Console.WriteLine("=====");
                foreach (var innerNodes in item.Previous)
                    Console.Write(" " + innerNodes.Value);

                Console.WriteLine("\n");
                Console.WriteLine("\t\tNODE " + item.Value);
                foreach (var innerNodes in item.Next)
                    Console.Write(" " + innerNodes.Value);

                Console.WriteLine("\n=====");

            }
        }

        private static void Processing(Node node, List<Node> args)
        {
            Console.Write(node.Value);
            if (node.Next.Count == 0)
                return;

            args = args.Concat(node.Next).ToList();
            Node primaryNode = FindMaxValue(args);

            args.Remove(primaryNode);
            Processing(primaryNode, args);
        }

        private static Node FindMaxValue(List<Node> nodes)
        {
            Node primaryNode = nodes.First();
            foreach (var node in nodes)
            {
                CheckNode(out bool flag, node.Value, primaryNode);
                if (node.Value < primaryNode.Value && !(primaryNode.Next.Contains(node)) && flag)
                    primaryNode = node;
            }

            return primaryNode;
        }

        private static void CheckNode(out bool flag, char value, Node node)
        {
            flag = true;
            foreach (var item in node.Next)
            {
                if (item.Value.Equals(value))
                {
                    flag = false;
                    return;
                }
                CheckNode(out flag, value, item);
            }
        }

        private static void InitPoints()
        {
            foreach (var pair in pairs)
            {
                bool nodeAlreadyExists = false;
                foreach (var node in nodes)
                {
                    if (node.Value.Equals(pair[0]))
                    {
                        nodeAlreadyExists = true;
                    }
                }

                if (!nodeAlreadyExists)
                {
                    Node point = new Node();
                    point.Value = pair[0];
                    nodes.Add(point);
                }

                nodeAlreadyExists = false;
                foreach (var node in nodes)
                {
                    if (node.Value.Equals(pair[1]))
                    {
                        nodeAlreadyExists = true;
                    }
                }

                if (!nodeAlreadyExists)
                {
                    Node point = new Node();
                    point.Value = pair[1];
                    nodes.Add(point);
                }

            }
        }

        private static void LoadData()
        {
            string path = @"..\in2.txt";
            List<string> inputData = new List<string>();

            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    inputData.Add(line);
                }
            }

            foreach (var line in inputData)
            {
                char[] pair = new char[2];
                pair[0] = line.Skip(5).Take(1).FirstOrDefault();
                pair[1] = line.Skip(36).Take(1).FirstOrDefault();
                pairs.Add(pair);
            }
        }

        private static void InitConnections()
        {

            foreach (var pair in pairs)
            {
                Node node = nodes.Where(n => n.Value.Equals(pair[0])).First();
                Node nextNode = nodes.Where(n => n.Value.Equals(pair[1])).First();
                node.Next.Add(nextNode);
                nextNode.Previous.Add(node);
            }
        }
    }


    class Node
    {
        public Node()
        {
            Previous = new List<Node>();
            Next = new List<Node>();
        }

        public char Value { get; set; }

        public List<Node> Previous { get; set; }

        public List<Node> Next { get; set; }
    }

}
