using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Netzwerklabyrinth_V_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            Console.SetBufferSize(1000, 1000);
            Console.SetWindowSize(200, 50);
            
            Console.WriteLine("Willkommen zum Spiel Netzwerklabyrinth");
            Console.WriteLine("Bitte treffen Sie einen Auswahl: 0 = Manuel, 1 = Automatisch");

            int anfrage = Convert.ToInt32(Console.ReadLine());
            //Blalala
            Client client = new Client();

            Console.Clear();

            client.Start(anfrage);


            Console.ReadKey();
        }
    }
}
