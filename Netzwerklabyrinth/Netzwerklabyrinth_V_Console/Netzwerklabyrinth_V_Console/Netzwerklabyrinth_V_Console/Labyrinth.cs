using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Netzwerklabyrinth_V_Console
{
    class Labyrinth
    {
        public SortedDictionary<int, SortedDictionary<int, node>> _Labyrinth = null;

        public node startNode = null;
        public node endNode = null;
        public node current = null;
        public node OldCurrent = null;
        public node tempCurrent = null;

        Dictionary<string, node> openList = new Dictionary<string, node>();
        Dictionary<string, node> closedList = new Dictionary<string, node>();
        Stack<string> kommandos = new Stack<string>();

        // Draw Variabls
        int MinX = 0;
        int MaxX = 0;
        int MinY = 0;
        int MaxY = 0;
        public void setPlayerPsition(int x, int y)
        {
            if (startNode == null)
            {
                startNode = new node(x, y, 'S');
                endNode = new node(startNode.x + 479, startNode.y + 479, 'Z');

                MinX = startNode.x - 25;
                MinY = startNode.y - 25;

                MaxX = startNode.x + 486;
                MaxY = startNode.y + 486;
            }
            if (current == null)
            {
                current = new node(x, y, 'S');
                OldCurrent = new node(x, y, 'S');
            }
            else
            {
                this.current.x = x;
                this.current.y = y;
            }
        }

        public void LabyrinthUpdate(SortedDictionary<int, SortedDictionary<int, node>> _TempLabyrinth)
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
                        }
                    }
            }
        }

        public void Full(SortedDictionary<int, SortedDictionary<int, node>> _TempLabyrinth)
        {
            if (_Labyrinth == null)
                _Labyrinth = _TempLabyrinth;

            else
            {
                LabyrinthUpdate(_TempLabyrinth);
            }

        }

        public void Draw()
        {
            if (Math.Abs(current.x - MinX) >= (Console.WindowWidth / 2) && Math.Abs(current.y - MinY) <= (Console.WindowHeight / 2))
            {
                Console.SetWindowPosition(Math.Abs(current.x - MinX) - (Console.WindowWidth / 2), 0);
            }
            else if (Math.Abs(current.x - MinX) <= (Console.WindowWidth / 2) && Math.Abs(current.y - MinY) >= (Console.WindowHeight / 2))
            {
                Console.SetWindowPosition(0, Math.Abs(current.y - MinY) - (Console.WindowHeight / 2));
            }
            if (Math.Abs(current.x - MinX) >= (Console.WindowWidth / 2) && Math.Abs(current.y - MinY) >= (Console.WindowHeight / 2))
            {
                Console.SetWindowPosition(Math.Abs(current.x - MinX) - (Console.WindowWidth / 2), Math.Abs(current.y - MinY) - (Console.WindowHeight / 2));
            }
            else
            {
                Console.SetWindowPosition(0, 0);
            }

            for (int Y = MinY, Top = 0; Y < MaxY; Y++, Top++)
            {
                _Labyrinth[OldCurrent.y][OldCurrent.x].state = State.NotDrawn;
                _Labyrinth[current.y][current.x].state = State.NotDrawn;
                OldCurrent = current;
                if (_Labyrinth.ContainsKey(Y))
                {
                    for (int X = MinX, Left = 0; X < MaxX; X++, Left++)
                    {
                        if (_Labyrinth[Y].ContainsKey(X))
                            if (_Labyrinth[Y][X].state == State.NotDrawn)
                            {
                                DrawAtPosition(Left, Top, _Labyrinth[Y][X].value);
                                _Labyrinth[Y][X].state = State.Drawn;

                                _Labyrinth[Y][X].Left = Left;
                                _Labyrinth[Y][X].Top = Top;
                            }
                    }
                }
            }
        }
     
        public void DrawAtPosition(int Left, int Top, char c)
        {

            Console.SetCursorPosition(Left, Top);

            if (c == '.')
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(' ');
            }
            else if (c == 'W')
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write(' ');
            }
            else if (c == 'Z')
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.Write(c);
            }
            else if (c == 'S')
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write('P');
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, 0);
        }

    }
}
