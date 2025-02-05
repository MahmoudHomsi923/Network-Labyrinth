using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netzwerklabyrinth_V_Console
{
    enum State
    {
        NotDrawn = 0,
        Drawn
    }
    class node
    {
        public int x;
        public int y;
        public string key;
        public char value;
        public int g;
        public int h;
        public int f;
        public node parent;
        public State state = State.NotDrawn;
        public int Left;
        public int Top;

        public node(int x, int y, char value)
        {
            this.x = x;
            this.y = y;
            this.key = x.ToString() + y.ToString();
            this.value = value;

            this.g = 0;
            this.h = 0;
            this.f = 0;

            this.parent = null;
        }
    }
}
