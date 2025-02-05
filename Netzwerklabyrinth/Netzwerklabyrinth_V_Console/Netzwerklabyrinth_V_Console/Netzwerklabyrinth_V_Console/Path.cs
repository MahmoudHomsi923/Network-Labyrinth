using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netzwerklabyrinth_V_Console
{
    class Path
    {
        public Stack<node> _path = new Stack<node>();
        public int sumOfF = 0;
        public Path(node endNode, node startNode)
        {

            node _endNode = endNode;
            while (_endNode.key != startNode.key)
            {
                this._path.Push(_endNode);
                this.sumOfF += _endNode.f;
                _endNode = _endNode.parent;
            }
            sumOfF = sumOfF / _path.Count;
        }
    }
}
