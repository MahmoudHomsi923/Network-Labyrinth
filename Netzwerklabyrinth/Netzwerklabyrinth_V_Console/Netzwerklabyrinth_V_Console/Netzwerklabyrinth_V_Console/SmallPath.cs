using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netzwerklabyrinth_V_Console
{
    class SmallPath
    {
        public SortedDictionary<int, SortedDictionary<int, node>> Map = null;
        Dictionary<string, node> openList = new Dictionary<string, node>();
        Dictionary<string, node> closedList = new Dictionary<string, node>();
        node current = null;
        node target = null;

        public Stack<string> ReturnEndPath(SortedDictionary<int, SortedDictionary<int, node>> sMap, node current, node target)
        {
            this.Map = sMap;
            this.current = current;
            this.target = target;

            foreach (KeyValuePair<int, SortedDictionary<int, node>> kvp in Map)
            {
                foreach (KeyValuePair<int, node> item in kvp.Value)
                {
                    Map[kvp.Key][item.Key].parent = null;
                }
            }

            node endNode = Astar(current, target);

            return Returnbefehele(endNode);
        }

        public node Astar(node current, node target)
        {
            openList.Add(current.key, current);

            while (true)
            {
                node _current = TheSmallest();

                if (_current.key == target.key)
                    return _current;

                openList.Remove(_current.key);

                if (!closedList.ContainsKey(_current.key))
                    closedList.Add(_current.key, _current);

                findNeighbors(_current);
            }
        }

        public node AstarEndPath(SortedDictionary<int, SortedDictionary<int, node>> sMap, node current, node target)
        {

            this.Map = sMap;
            this.current = current;
            this.target = target;

            foreach (KeyValuePair<int, SortedDictionary<int, node>> kvp in Map)
            {
                foreach (KeyValuePair<int, node> item in kvp.Value)
                {
                    Map[kvp.Key][item.Key].parent = null;
                }
            }

            openList.Add(current.key, current);
            node _current = null;
            while (openList.Count > 0)
            {
                _current = TheSmallest();

                if (_current.value == 'Z')
                    break;

                openList.Remove(_current.key);

                if (!closedList.ContainsKey(_current.key))
                    closedList.Add(_current.key, _current);

                findNeighbors(_current);
            }
            return _current;
        }
        private node TheSmallest()
        {
            node TheSmallest = openList.First().Value;

            foreach (KeyValuePair<string, node> item in openList)
            {
                if (item.Value.f < TheSmallest.f)
                    TheSmallest = item.Value;
            }
            return TheSmallest;
        }


        private void findNeighbors(node current)
        {
            // Down
            if (Map.ContainsKey(current.y - 1) && Map[current.y - 1].ContainsKey(current.x))
            {
                if ((Map[current.y - 1][current.x].value == '.' || Map[current.y - 1][current.x].value == 'Z')
                        && !openList.ContainsKey(Map[current.y - 1][current.x].key)
                        && !closedList.ContainsKey(Map[current.y - 1][current.x].key))
                {
                    if (Map[current.y - 1][current.x].parent != null)
                    {
                        if (Map[current.y - 1][current.x].parent.key != Map[current.y][current.x].key)
                        {
                            Map[current.y - 1][current.x].g = Math.Abs(Map[current.y - 1][current.x].x - current.x) + Math.Abs(Map[current.y - 1][current.x].y - current.y);
                            Map[current.y - 1][current.x].h = Math.Abs(Map[current.y - 1][current.x].x - target.x) + Math.Abs(Map[current.y - 1][current.x].y - target.y);
                            Map[current.y - 1][current.x].f = Map[current.y - 1][current.x].g + Map[current.y - 1][current.x].h;
                            Map[current.y - 1][current.x].parent = Map[current.y][current.x];
                            openList.Add(Map[current.y - 1][current.x].key, Map[current.y - 1][current.x]);
                        }
                    }
                    else
                    {
                        Map[current.y - 1][current.x].g = Math.Abs(Map[current.y - 1][current.x].x - current.x) + Math.Abs(Map[current.y - 1][current.x].y - current.y);
                        Map[current.y - 1][current.x].h = Math.Abs(Map[current.y - 1][current.x].x - target.x) + Math.Abs(Map[current.y - 1][current.x].y - target.y);
                        Map[current.y - 1][current.x].f = Map[current.y - 1][current.x].g + Map[current.y - 1][current.x].h;
                        Map[current.y - 1][current.x].parent = Map[current.y][current.x];
                        openList.Add(Map[current.y - 1][current.x].key, Map[current.y - 1][current.x]);
                    }
                }
            }

            // Right 
            if (Map.ContainsKey(current.y) && Map[current.y].ContainsKey(current.x + 1))
            {
                if ((Map[current.y][current.x + 1].value == '.' || Map[current.y][current.x + 1].value == 'Z')
            && !openList.ContainsKey(Map[current.y][current.x + 1].key)
            && !closedList.ContainsKey(Map[current.y][current.x + 1].key))
                {
                    if (Map[current.y][current.x + 1].parent != null)
                    {
                        if (Map[current.y][current.x + 1].parent.key != Map[current.y][current.x].key)
                        {
                            Map[current.y][current.x + 1].g = Math.Abs(Map[current.y][current.x + 1].x - current.x) + Math.Abs(Map[current.y][current.x + 1].y - current.y);
                            Map[current.y][current.x + 1].h = Math.Abs(Map[current.y][current.x + 1].x - target.x) + Math.Abs(Map[current.y][current.x + 1].y - target.y);
                            Map[current.y][current.x + 1].f = Map[current.y][current.x + 1].g + Map[current.y][current.x + 1].h;
                            Map[current.y][current.x + 1].parent = Map[current.y][current.x];
                            openList.Add(Map[current.y][current.x + 1].key, Map[current.y][current.x + 1]);
                        }
                    }
                    else
                    {
                        Map[current.y][current.x + 1].g = Math.Abs(Map[current.y][current.x + 1].x - current.x) + Math.Abs(Map[current.y][current.x + 1].y - current.y);
                        Map[current.y][current.x + 1].h = Math.Abs(Map[current.y][current.x + 1].x - target.x) + Math.Abs(Map[current.y][current.x + 1].y - target.y);
                        Map[current.y][current.x + 1].f = Map[current.y][current.x + 1].g + Map[current.y][current.x + 1].h;
                        Map[current.y][current.x + 1].parent = Map[current.y][current.x];
                        openList.Add(Map[current.y][current.x + 1].key, Map[current.y][current.x + 1]);
                    }
                }
            }

            // Up
            if (Map.ContainsKey(current.y + 1) && Map[current.y + 1].ContainsKey(current.x))
            {
                if ((Map[current.y + 1][current.x].value == '.' || Map[current.y + 1][current.x].value == 'Z')
            && !openList.ContainsKey(Map[current.y + 1][current.x].key)
            && !closedList.ContainsKey(Map[current.y + 1][current.x].key))
                {
                    if (Map[current.y + 1][current.x].parent != null)
                    {
                        if (Map[current.y + 1][current.x].parent.key != Map[current.y][current.x].key)
                        {
                            Map[current.y + 1][current.x].g = Math.Abs(Map[current.y + 1][current.x].x - current.x) + Math.Abs(Map[current.y + 1][current.x].y - current.y);
                            Map[current.y + 1][current.x].h = Math.Abs(Map[current.y + 1][current.x].x - target.x) + Math.Abs(Map[current.y + 1][current.x].y - target.y);
                            Map[current.y + 1][current.x].f = Map[current.y + 1][current.x].g + Map[current.y + 1][current.x].h;
                            Map[current.y + 1][current.x].parent = Map[current.y][current.x];
                            openList.Add(Map[current.y + 1][current.x].key, Map[current.y + 1][current.x]);
                        }
                    }
                    else
                    {
                        Map[current.y + 1][current.x].g = Math.Abs(Map[current.y + 1][current.x].x - current.x) + Math.Abs(Map[current.y + 1][current.x].y - current.y);
                        Map[current.y + 1][current.x].h = Math.Abs(Map[current.y + 1][current.x].x - target.x) + Math.Abs(Map[current.y + 1][current.x].y - target.y);
                        Map[current.y + 1][current.x].f = Map[current.y + 1][current.x].g + Map[current.y + 1][current.x].h;
                        Map[current.y + 1][current.x].parent = Map[current.y][current.x];
                        openList.Add(Map[current.y + 1][current.x].key, Map[current.y + 1][current.x]);
                    }
                }
            }

            // Left
            if (Map.ContainsKey(current.y) && Map[current.y].ContainsKey(current.x - 1))
            {
                if ((Map[current.y][current.x - 1].value == '.' || Map[current.y][current.x - 1].value == 'Z')
            && !openList.ContainsKey(Map[current.y][current.x - 1].key)
            && !closedList.ContainsKey(Map[current.y][current.x - 1].key))
                {
                    if (Map[current.y][current.x - 1].parent != null)
                    {
                        if (Map[current.y][current.x - 1].parent.key != Map[current.y][current.x].key)
                        {
                            Map[current.y][current.x - 1].g = Math.Abs(Map[current.y][current.x - 1].x - current.x) + Math.Abs(Map[current.y][current.x - 1].y - current.y);
                            Map[current.y][current.x - 1].h = Math.Abs(Map[current.y][current.x - 1].x - target.x) + Math.Abs(Map[current.y][current.x - 1].y - target.y);
                            Map[current.y][current.x - 1].f = Map[current.y][current.x - 1].g + Map[current.y][current.x - 1].h;
                            Map[current.y][current.x - 1].parent = Map[current.y][current.x];
                            openList.Add(Map[current.y][current.x - 1].key, Map[current.y][current.x - 1]);
                        }
                    }
                    else
                    {
                        Map[current.y][current.x - 1].g = Math.Abs(Map[current.y][current.x - 1].x - current.x) + Math.Abs(Map[current.y][current.x - 1].y - current.y);
                        Map[current.y][current.x - 1].h = Math.Abs(Map[current.y][current.x - 1].x - target.x) + Math.Abs(Map[current.y][current.x - 1].y - target.y);
                        Map[current.y][current.x - 1].f = Map[current.y][current.x - 1].g + Map[current.y][current.x - 1].h;
                        Map[current.y][current.x - 1].parent = Map[current.y][current.x];
                        openList.Add(Map[current.y][current.x - 1].key, Map[current.y][current.x - 1]);
                    }
                }
            }
        }

        private Stack<string> Returnbefehele(node endNode)
        {
            Stack<string> kommandos = new Stack<string>();

            node _target = endNode;


            while (true)
            {
                // Down
                if (_target.parent.y < _target.y && _target.parent.x == _target.x)
                    kommandos.Push("DOWN");

                // Up
                else if (_target.parent.y > _target.y && _target.parent.x == _target.x)
                    kommandos.Push("UP");

                // Left
                else if (_target.parent.y == _target.y && _target.parent.x > _target.x)
                    kommandos.Push("LEFT");

                // Rigth
                else if (_target.parent.y == _target.y && _target.parent.x < _target.x)
                    kommandos.Push("RIGHT");

                if (current.key == _target.parent.key)
                    break;

                else
                    _target = _target.parent;

            }

            // Reverse 
            //List<string> temp = new List<string>();

            //while (kommandos.Count > 0)
            //    temp.Add(kommandos.Pop());

            //foreach (string item in temp)
            //    kommandos.Push(item);

            return kommandos;
        }
    }
}
