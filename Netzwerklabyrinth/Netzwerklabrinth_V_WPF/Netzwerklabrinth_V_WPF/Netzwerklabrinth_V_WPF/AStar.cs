using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netzwerklabrinth_V_WPF
{
   
    class AStar
    {
        private Dictionary<Key, Node> openList = new Dictionary<Key, Node>();
        private Dictionary<Key, Node> closedList = new Dictionary<Key, Node>();

        public Stack<Direction> directions = new Stack<Direction>();
        private byte[,] map = new byte[1024, 1024];
        private Node current;

        public AStar(byte[,] map, int playerX, int playerY, Mission mission)
        {
            this.map = map;
            if (mission == Mission.FromPlayer)
            {
                current = new Node(playerY, playerX, null, Direction.Null);
                openList.Add(new Key(playerY, playerX), current);
            }
            else if (mission == Mission.FromStart)
            {
                this.map[playerY, playerX] = 1;
                current = new Node(512, 512, null, Direction.Null);
                openList.Add(new Key(512, 512), current);
            }
            
            while (openList.Count > 0)
            {
                current = getSmallest();

                if (this.map[current.Y, current.X] == 4)
                {
                    if (mission == Mission.FromPlayer)
                    {
                        SetDirections(current.Parent);
                        return;
                    }
                    else
                    {
                        MarkTheWay(current.Parent);
                        return;
                    }
                }

                openList.Remove(new Key(current.Y, current.X));
                int result = findNeigbors(mission);

                // Result = Neighbors found
                if (result == 1)
                {
                    if (!closedList.ContainsKey(new Key(current.Y, current.X)))
                        closedList.Add(new Key(current.Y, current.X), current);
                }

                // Result = No Neighbors
                if (result == 2)
                {
                    closedList.Remove(new Key(current.Y, current.X));
                }

                // Result = Null
                if (result == 3)
                {
                    SetDirections(current);
                    return;
                }
            }
        }

        public int Current => map[current.Y, current.X];

        public byte[,] GetMap => map;
        private int findNeigbors(Mission mission)
        {
            if (mission == Mission.FromPlayer)
            {
                if (map[current.Y - 1, current.X] == 0 || map[current.Y + 1, current.X] == 0 ||
                map[current.Y, current.X - 1] == 0 || map[current.Y, current.X + 1] == 0)
                    return 3;
            }

            int counter = 0;

            if ((map[current.Y - 1, current.X] == 1 || map[current.Y - 1, current.X] == 4) &&
                !closedList.ContainsKey(new Key(current.Y - 1, current.X)) &&
                !openList.ContainsKey(new Key(current.Y - 1, current.X)))
            {
                SetNeigborAndAddToOpenList(current.Y - 1, current.X, current, Direction.Up);
                counter++;
            }

            if ((map[current.Y + 1, current.X] == 1 || map[current.Y + 1, current.X] == 4) &&
                !closedList.ContainsKey(new Key(current.Y + 1, current.X)) &&
                !openList.ContainsKey(new Key(current.Y + 1, current.X)))
            {
                SetNeigborAndAddToOpenList(current.Y + 1, current.X, current, Direction.Down);
                counter++;
            }

            if ((map[current.Y, current.X - 1] == 1 || map[current.Y, current.X - 1] == 4) &&
                !closedList.ContainsKey(new Key(current.Y, current.X - 1)) &&
                !openList.ContainsKey(new Key(current.Y, current.X - 1)))
            {
                SetNeigborAndAddToOpenList(current.Y, current.X - 1, current, Direction.Left);
                counter++;
            }

            if ((map[current.Y, current.X + 1] == 1 || map[current.Y, current.X + 1] == 4) &&
                !closedList.ContainsKey(new Key(current.Y, current.X + 1)) &&
                !openList.ContainsKey(new Key(current.Y, current.X + 1)))
            {
                SetNeigborAndAddToOpenList(current.Y, current.X + 1, current, Direction.Right);
                counter++;
            }

            if (counter == 0)
                return 2;

            else return 1;
        }

        private Node getSmallest()
        {
            Node smallest = openList.First().Value;

            foreach (KeyValuePair<Key, Node> item in openList)
            {
                if (item.Value.f < smallest.f)
                    smallest = item.Value;

                else if (item.Value.f == smallest.f && (item.Key.Y != smallest.Key.Y || item.Key.X != smallest.Key.X))
                {
                    if (item.Value.X != smallest.X && item.Value.Y == smallest.Y)
                    {
                        if (item.Value.X > smallest.X)
                            smallest = item.Value;
                    }
                }
            }
            return smallest;
        }

        private void SetNeigborAndAddToOpenList(int NeigborY, int NeigborX, Node parent, Direction direction)
        {
            Node newNode = new Node(NeigborY, NeigborX, parent, direction);
            newNode.g = SetGvalue(NeigborY, NeigborX);
            newNode.h = SetHvalue(NeigborY + 1, NeigborX);
            newNode.f = newNode.g + newNode.h;
            openList.Add(new Key(newNode.Y, newNode.X), newNode);
        }
        private int SetGvalue(int y, int x)
        {
            return Math.Abs(y - 512) + Math.Abs(x - 512);
        }

        private int SetHvalue(int y, int x)
        {
            return StreetCost(y, x) * (Math.Abs(y - 1023) + Math.Abs(x - 1023));
        }

        private int StreetCost(int y, int x)
        {
            return Math.Abs(y - 1023) + Math.Abs(x - 1023);
        }

        private void SetDirections(Node current)
        {
            while (current.Parent != null)
            {
                directions.Push(current.From);
                current = current.Parent;
            }
        }

        public void MarkTheWay(Node current)
        {
            Node _current = current;
            Key k = new Key(512, 512);
            while (_current.Y != k.Y || _current.X != k.X)
            {
                map[_current.Y, _current.X] = 6;
                _current = _current.Parent;
            }
        }
    }
}
