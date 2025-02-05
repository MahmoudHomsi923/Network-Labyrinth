using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Netzwerklabyrinth_V_Console
{
    class Messgae
    {
        StringReader stringReader;
        public Labyrinth Labyrinth = new Labyrinth();
        public void MessageManger(string message)
        {
            stringReader = new StringReader(message);
            SortedDictionary<int, SortedDictionary<int, node>> _TempLabyrinth = new SortedDictionary<int, SortedDictionary<int, node>>();

            int x = 0, y = 0;
            int yTemp = 0;
            string row;
            while (stringReader.Peek() > -1)
            {
                row = stringReader.ReadLine();
                if (row[0].Equals('3'))
                {
                    row = row.Split(Environment.NewLine.ToCharArray(),
                           1).FirstOrDefault();
                    Match match = Regex.Match(row, @"3\s{1}-?\d{1,3}\s{1}-?\d{1,3}");
                    if (match.Success)
                    {
                        string[] arr = Regex.Split(match.Value, @"\s");
                        x = int.Parse(arr[1]);
                        y = int.Parse(arr[2]);

                        Labyrinth.setPlayerPsition(x, y);
                    }
                    yTemp = y - 5;
                }
                else if (row[0].Equals('4'))
                {
                    int xTemp = x - 5;

                    SortedDictionary<int, node> _Temp = new SortedDictionary<int, node>();
                    for (int i = 2; i < row.Length; i++)
                    {
                        _Temp.Add(xTemp, new node(xTemp, yTemp, row[i]));
                        xTemp++;
                    }
                    _TempLabyrinth.Add(yTemp, _Temp);
                    yTemp++;
                }
            }
            if (_TempLabyrinth.Count > 0)
            {
                Labyrinth.Full(_TempLabyrinth);
                Labyrinth.Draw();
            }
        }
    }
}