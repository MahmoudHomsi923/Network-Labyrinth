using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netzwerklabrinth_V_WPF
{
    class Node
    {
        public int Y;
        public int X;

        public Key Key;
        public int g;
        public int h;
        public int f;

        public Node Parent;
        public Direction From;

        public Node(int y, int x, Node parent, Direction from)
        {
            Y = y;
            X = x;
            Key = new Key(y, x);
            Parent = parent;
            From = from;
        }
    }
}
