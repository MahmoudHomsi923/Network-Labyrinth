using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Netzwerklabyrinth_V_Console
{
    enum InputTyp
    {
        Manuel = 0,
        Automatisch
    }
    enum Answertyp
    {
        Move = 0,
        Print
    }
    class Answer
    {
        public InputTyp inputTyp;
        public Answertyp answertyp;

        public SortedDictionary<int, SortedDictionary<int, node>> _Labyrinth = null;

        public node startNode = null;
        public node endNode = null;
        public node current = null;
        public node tempCurrent = null;

        bool goalArchived = false;
        Dictionary<string, node> openList = new Dictionary<string, node>();
        Dictionary<string, node> closedList = new Dictionary<string, node>();
        Stack<string> kommandos = new Stack<string>();

        public Answer(InputTyp answerTyp)
        {
            this.inputTyp = answerTyp;
            this.answertyp = Answertyp.Print;
        }


        public string ManuelInput()
        {
            string inputText = "";

            ConsoleKeyInfo input;

            if (answertyp == Answertyp.Print)
            {
                inputText = "PRINT";
                answertyp = Answertyp.Move;
            }
            else if (answertyp == Answertyp.Move)
            {
                input = Console.ReadKey();
                if (input.Key == ConsoleKey.UpArrow)
                {
                    inputText = "UP";
                    answertyp = Answertyp.Print;
                }
                else if (input.Key == ConsoleKey.DownArrow)
                {
                    inputText = "DOWN";
                    answertyp = Answertyp.Print;
                }
                else if (input.Key == ConsoleKey.RightArrow)
                {
                    inputText = "RIGHT";
                    answertyp = Answertyp.Print;
                }
                else if (input.Key == ConsoleKey.LeftArrow)
                {
                    inputText = "LEFT";
                    answertyp = Answertyp.Print;
                }
                else
                {
                    inputText = "NOTHING";
                }
            }

            return inputText;
        }



        public string AutomatischInput()
        {
            string input = "";
            if (answertyp == Answertyp.Print)
            {
                input = "PRINT";
                answertyp = Answertyp.Move;
            }
            else if (answertyp == Answertyp.Move)
            {
                if (kommandos.Count == 0)
                {
                    if (!openList.ContainsKey(current.key))
                        openList.Add(current.key, current);

                    input = Astar();
                    answertyp = Answertyp.Print;
                }
                else if (kommandos.Count > 0)
                {
                    input = kommandos.Pop();
                    answertyp = Answertyp.Print;
                }
            }
            return input;
        }
        private string Astar()
        {
            string input = "";
            if (!goalArchived)
            {
                while (openList.Count > 0)
                {
                    node theSmallest = TheSmallest();

                    tempCurrent = theSmallest;

                    if (tempCurrent.value == 'Z')
                    {
                        goalArchived = true;
                        SmallPath s = new SmallPath();
                        kommandos = s.ReturnEndPath(_Labyrinth, current, tempCurrent.parent);
                        input = kommandos.Pop();
                        return input;
                    }
                    else
                    {
                        openList.Remove(tempCurrent.key);

                        if (!closedList.ContainsKey(tempCurrent.key))
                            closedList.Add(tempCurrent.key, tempCurrent);

                        int result = findNeighbors(tempCurrent);

                        if (result == 3)
                        {
                            SmallPath s = new SmallPath();
                            kommandos = s.ReturnEndPath(_Labyrinth, current, tempCurrent);
                            closedList = new Dictionary<string, node>();
                            input = kommandos.Pop();
                            //kommandos = new Stack<string>();
                            //openList = new Dictionary<string, node>();
                            return input;
                        }
                    }
                }
            }
            else
            {
                SmallPath s = new SmallPath();
                node _endnode = s.AstarEndPath(_Labyrinth, startNode, endNode);
                DrawGameEnde(_endnode);
            }
            return input;
        }

        public node TheSmallest()
        {
            node TheSmallest = openList.First().Value;

            foreach (KeyValuePair<string, node> item in openList)
            {
                if (item.Value.f < TheSmallest.f)
                    TheSmallest = item.Value;

                else if (item.Value.f == TheSmallest.f && item.Key != TheSmallest.key)
                {
                    if (item.Value.x != TheSmallest.x && item.Value.y == TheSmallest.y)
                    {
                        if (item.Value.x > TheSmallest.x)
                            TheSmallest = item.Value;
                    }
                    else if (item.Value.x == TheSmallest.x && item.Value.y != TheSmallest.y)
                    {
                        if (item.Value.y > TheSmallest.y)
                            TheSmallest = item.Value;
                    }
                }
            }
            return TheSmallest;
        }

        public int findNeighbors(node current)
        {
            // 1 = found
            // 2 = No Neighbors
            // 3 = Null
            int z = 0;

            // Down
            if (!_Labyrinth.ContainsKey(current.y - 1))
            {
                return 3;
            }
            else
            {
                if (!_Labyrinth[current.y - 1].ContainsKey(current.x))
                {
                    return 3;
                }
            }
            // Right
            if (!_Labyrinth.ContainsKey(current.y))
            {
                return 3;
            }
            else
            {
                if (!_Labyrinth[current.y].ContainsKey(current.x + 1))
                {
                    return 3;
                }
            }
            // Up
            if (!_Labyrinth.ContainsKey(current.y + 1))
            {
                return 3;
            }
            else
            {
                if (!_Labyrinth[current.y + 1].ContainsKey(current.x))
                {
                    return 3;
                }
            }
            // Left
            if (!_Labyrinth.ContainsKey(current.y))
            {
                return 3;
            }
            else
            {
                if (!_Labyrinth[current.y].ContainsKey(current.x - 1))
                {
                    return 3;
                }
            }

            // Down
            if (openList.ContainsKey(_Labyrinth[current.y - 1][current.x].key))
            {
                z++;
            }
            else
            {
                if ((_Labyrinth[current.y - 1][current.x].value == '.' || _Labyrinth[current.y - 1][current.x].value == 'Z')
                        && !openList.ContainsKey(_Labyrinth[current.y - 1][current.x].key)
                        && !closedList.ContainsKey(_Labyrinth[current.y - 1][current.x].key))
                {
                    if (_Labyrinth[current.y - 1][current.x].parent != null)
                    {
                        if (_Labyrinth[current.y - 1][current.x].parent.key != _Labyrinth[current.y][current.x].key)
                        {
                            _Labyrinth[current.y - 1][current.x].g = Math.Abs(_Labyrinth[current.y - 1][current.x].x - startNode.x) + Math.Abs(_Labyrinth[current.y - 1][current.x].y - startNode.y);
                            _Labyrinth[current.y - 1][current.x].h = StreetCost(_Labyrinth[current.y - 1][current.x].x, _Labyrinth[current.y - 1][current.x].y) *
                                (Math.Abs(_Labyrinth[current.y - 1][current.x].x - endNode.x) + Math.Abs(_Labyrinth[current.y - 1][current.x].y - endNode.y));
                            _Labyrinth[current.y - 1][current.x].f = _Labyrinth[current.y - 1][current.x].g + _Labyrinth[current.y - 1][current.x].h;
                            _Labyrinth[current.y - 1][current.x].parent = _Labyrinth[current.y][current.x];
                            openList.Add(_Labyrinth[current.y - 1][current.x].key, _Labyrinth[current.y - 1][current.x]);
                            z++;
                        }
                    }
                    else
                    {
                        _Labyrinth[current.y - 1][current.x].g = Math.Abs(_Labyrinth[current.y - 1][current.x].x - startNode.x) + Math.Abs(_Labyrinth[current.y - 1][current.x].y - startNode.y);
                        _Labyrinth[current.y - 1][current.x].h = StreetCost(_Labyrinth[current.y - 1][current.x].x, _Labyrinth[current.y - 1][current.x].y) *
                            (Math.Abs(_Labyrinth[current.y - 1][current.x].x - endNode.x) + Math.Abs(_Labyrinth[current.y - 1][current.x].y - endNode.y));
                        _Labyrinth[current.y - 1][current.x].f = _Labyrinth[current.y - 1][current.x].g + _Labyrinth[current.y - 1][current.x].h;
                        _Labyrinth[current.y - 1][current.x].parent = _Labyrinth[current.y][current.x];
                        openList.Add(_Labyrinth[current.y - 1][current.x].key, _Labyrinth[current.y - 1][current.x]);
                        z++;
                    }
                }
            }

            // Right
            if (openList.ContainsKey(_Labyrinth[current.y][current.x + 1].key))
            {
                z++;
            }
            else
            {
                if ((_Labyrinth[current.y][current.x + 1].value == '.' || _Labyrinth[current.y][current.x + 1].value == 'Z')
                && !openList.ContainsKey(_Labyrinth[current.y][current.x + 1].key)
                && !closedList.ContainsKey(_Labyrinth[current.y][current.x + 1].key))
                {
                    if (_Labyrinth[current.y][current.x + 1].parent != null)
                    {
                        if (_Labyrinth[current.y][current.x + 1].parent.key != _Labyrinth[current.y][current.x].key)
                        {
                            _Labyrinth[current.y][current.x + 1].g = Math.Abs(_Labyrinth[current.y][current.x + 1].x - startNode.x) + Math.Abs(_Labyrinth[current.y][current.x + 1].y - startNode.y);
                            _Labyrinth[current.y][current.x + 1].h = StreetCost(_Labyrinth[current.y][current.x + 1].x, _Labyrinth[current.y][current.x + 1].y) *
                                (Math.Abs(_Labyrinth[current.y][current.x + 1].x - endNode.x) + Math.Abs(_Labyrinth[current.y][current.x + 1].y - endNode.y));
                            _Labyrinth[current.y][current.x + 1].f = _Labyrinth[current.y][current.x + 1].g + _Labyrinth[current.y][current.x + 1].h;
                            _Labyrinth[current.y][current.x + 1].parent = _Labyrinth[current.y][current.x];
                            openList.Add(_Labyrinth[current.y][current.x + 1].key, _Labyrinth[current.y][current.x + 1]);
                            z++;
                        }
                    }
                    else
                    {
                        _Labyrinth[current.y][current.x + 1].g = Math.Abs(_Labyrinth[current.y][current.x + 1].x - startNode.x) + Math.Abs(_Labyrinth[current.y][current.x + 1].y - startNode.y);
                        _Labyrinth[current.y][current.x + 1].h = StreetCost(_Labyrinth[current.y][current.x + 1].x, _Labyrinth[current.y][current.x + 1].y) *
                            (Math.Abs(_Labyrinth[current.y][current.x + 1].x - endNode.x) + Math.Abs(_Labyrinth[current.y][current.x + 1].y - endNode.y));
                        _Labyrinth[current.y][current.x + 1].f = _Labyrinth[current.y][current.x + 1].g + _Labyrinth[current.y][current.x + 1].h;
                        _Labyrinth[current.y][current.x + 1].parent = _Labyrinth[current.y][current.x];
                        openList.Add(_Labyrinth[current.y][current.x + 1].key, _Labyrinth[current.y][current.x + 1]);
                        z++;
                    }
                }
            }

            // Up
            if (openList.ContainsKey(_Labyrinth[current.y + 1][current.x].key))
            {
                z++;
            }
            else
            {
                if ((_Labyrinth[current.y + 1][current.x].value == '.' || _Labyrinth[current.y + 1][current.x].value == 'Z')
                && !openList.ContainsKey(_Labyrinth[current.y + 1][current.x].key)
                && !closedList.ContainsKey(_Labyrinth[current.y + 1][current.x].key))
                {
                    if (_Labyrinth[current.y + 1][current.x].parent != null)
                    {
                        if (_Labyrinth[current.y + 1][current.x].parent.key != _Labyrinth[current.y][current.x].key)
                        {
                            _Labyrinth[current.y + 1][current.x].g = Math.Abs(_Labyrinth[current.y + 1][current.x].x - startNode.x) + Math.Abs(_Labyrinth[current.y + 1][current.x].y - startNode.y);
                            _Labyrinth[current.y + 1][current.x].h = StreetCost(_Labyrinth[current.y + 1][current.x].x, _Labyrinth[current.y + 1][current.x].y) *
                                (Math.Abs(_Labyrinth[current.y + 1][current.x].x - endNode.x) + Math.Abs(_Labyrinth[current.y + 1][current.x].y - endNode.y));
                            _Labyrinth[current.y + 1][current.x].f = _Labyrinth[current.y + 1][current.x].g + _Labyrinth[current.y + 1][current.x].h;
                            _Labyrinth[current.y + 1][current.x].parent = _Labyrinth[current.y][current.x];
                            openList.Add(_Labyrinth[current.y + 1][current.x].key, _Labyrinth[current.y + 1][current.x]);
                            z++;
                        }
                    }
                    else
                    {
                        _Labyrinth[current.y + 1][current.x].g = Math.Abs(_Labyrinth[current.y + 1][current.x].x - startNode.x) + Math.Abs(_Labyrinth[current.y + 1][current.x].y - startNode.y);
                        _Labyrinth[current.y + 1][current.x].h = StreetCost(_Labyrinth[current.y + 1][current.x].x, _Labyrinth[current.y + 1][current.x].y) *
                            (Math.Abs(_Labyrinth[current.y + 1][current.x].x - endNode.x) + Math.Abs(_Labyrinth[current.y + 1][current.x].y - endNode.y));
                        _Labyrinth[current.y + 1][current.x].f = _Labyrinth[current.y + 1][current.x].g + _Labyrinth[current.y + 1][current.x].h;
                        _Labyrinth[current.y + 1][current.x].parent = _Labyrinth[current.y][current.x];
                        openList.Add(_Labyrinth[current.y + 1][current.x].key, _Labyrinth[current.y + 1][current.x]);
                        z++;
                    }
                }
            }

            // Left
            if (openList.ContainsKey(_Labyrinth[current.y][current.x - 1].key))
            {
                z++;
            }
            else
            {
                if ((_Labyrinth[current.y][current.x - 1].value == '.' || _Labyrinth[current.y][current.x - 1].value == 'Z')
                && !openList.ContainsKey(_Labyrinth[current.y][current.x - 1].key)
                && !closedList.ContainsKey(_Labyrinth[current.y][current.x - 1].key))
                {
                    if (_Labyrinth[current.y][current.x - 1].parent != null)
                    {
                        if (_Labyrinth[current.y][current.x - 1].parent.key != _Labyrinth[current.y][current.x].key)
                        {
                            _Labyrinth[current.y][current.x - 1].g = Math.Abs(_Labyrinth[current.y][current.x - 1].x - startNode.x) + Math.Abs(_Labyrinth[current.y][current.x - 1].y - startNode.y);
                            _Labyrinth[current.y][current.x - 1].h = StreetCost(_Labyrinth[current.y][current.x - 1].x, _Labyrinth[current.y][current.x - 1].y) *
                                (Math.Abs(_Labyrinth[current.y][current.x - 1].x - endNode.x) + Math.Abs(_Labyrinth[current.y][current.x - 1].y - endNode.y));
                            _Labyrinth[current.y][current.x - 1].f = _Labyrinth[current.y][current.x - 1].g + _Labyrinth[current.y][current.x - 1].h;
                            _Labyrinth[current.y][current.x - 1].parent = _Labyrinth[current.y][current.x];
                            openList.Add(_Labyrinth[current.y][current.x - 1].key, _Labyrinth[current.y][current.x - 1]);
                            z++;
                        }
                    }
                    else
                    {
                        _Labyrinth[current.y][current.x - 1].g = Math.Abs(_Labyrinth[current.y][current.x - 1].x - startNode.x) + Math.Abs(_Labyrinth[current.y][current.x - 1].y - startNode.y);
                        _Labyrinth[current.y][current.x - 1].h = StreetCost(_Labyrinth[current.y][current.x - 1].x, _Labyrinth[current.y][current.x - 1].y) *
                            (Math.Abs(_Labyrinth[current.y][current.x - 1].x - endNode.x) + Math.Abs(_Labyrinth[current.y][current.x - 1].y - endNode.y));
                        _Labyrinth[current.y][current.x - 1].f = _Labyrinth[current.y][current.x - 1].g + _Labyrinth[current.y][current.x - 1].h;
                        _Labyrinth[current.y][current.x - 1].parent = _Labyrinth[current.y][current.x];
                        openList.Add(_Labyrinth[current.y][current.x - 1].key, _Labyrinth[current.y][current.x - 1]);
                        z++;
                    }
                }
            }

            if (z == 0)
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }

        public int StreetCost(int x, int y)
        {
            return Math.Abs(x - endNode.x) + Math.Abs(y - endNode.y);
        }

        public void LabyrinthUpdate(SortedDictionary<int, SortedDictionary<int, node>> _TempLabyrinth, node _startNode, node _current)
        {
            if (_TempLabyrinth != null && _startNode != null && _current != null)
            {
                if (_Labyrinth == null)
                {
                    _Labyrinth = _TempLabyrinth;
                    if (startNode == null)
                    {
                        startNode = _startNode;
                        endNode = new node(startNode.x + 479, startNode.y + 479, 'Z');
                    }
                    if (current == null)
                        current = _current;

                }
                else
                {
                    foreach (KeyValuePair<int, SortedDictionary<int, node>> kvp in _TempLabyrinth)
                    {
                        if (!_Labyrinth.ContainsKey(kvp.Key))
                        {
                            _Labyrinth.Add(kvp.Key, kvp.Value);
                        }
                        else if (_Labyrinth.ContainsKey(kvp.Key))
                            foreach (KeyValuePair<int, node> item in kvp.Value)
                            {
                                if (!_Labyrinth[kvp.Key].ContainsKey(item.Key))
                                {
                                    _Labyrinth[kvp.Key].Add(item.Key, item.Value);
                                }
                                else if (_Labyrinth[kvp.Key].ContainsKey(item.Key))
                                {
                                    if (_Labyrinth[kvp.Key][item.Key].value != item.Value.value)
                                        _Labyrinth[kvp.Key][item.Key].value = item.Value.value;

                                    if (_Labyrinth[kvp.Key][item.Key].value == 'S')
                                        current = _Labyrinth[kvp.Key][item.Key];
                                }
                            }
                    }
                }
            }
        }

        public void DrawGameEnde(node endNode)
        {
            while (endNode != null)
            {
                Console.SetCursorPosition(endNode.Left, endNode.Top);
                Console.BackgroundColor = ConsoleColor.Green;
                Console.Write(' ');
                Console.SetCursorPosition(0, 0);
                endNode = endNode.parent;
            }

            while (true)
            {
                Console.Beep();
            }
        }
    }
}
