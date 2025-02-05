using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netzwerklabrinth_V_WPF
{
    class Message
    {
        public int Code;
        public string Text;

        public Message(string message)
        {
            if (message == null)
                throw new IOException("Connection terminated.");

            Code = message[0] - 48;
            Text = message.Substring(2);
        }
    }
}
