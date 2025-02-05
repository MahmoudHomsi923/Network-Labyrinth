using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Netzwerklabyrinth_V_Console
{

    public class Client : IDisposable
    {
        private Socket socket;
        private NetworkStream networkStream;
        private StreamReader reader;
        private StreamWriter writer;
        private StringBuilder sb;
        private KommunikationManger kommunikationManger;
        MessageTyp messageTyp;
        InputTyp answerTyp;
        public void Start(int inputWahl)
        {
            if (inputWahl == 0)
                answerTyp = InputTyp.Manuel;

            else if (inputWahl == 1)
                answerTyp = InputTyp.Automatisch;


            using (socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                socket.Connect("87.193.160.126", 50003);
                socket.NoDelay = true;

                using (networkStream = new NetworkStream(socket))
                using (reader = new StreamReader(networkStream))
                using (writer = new StreamWriter(networkStream))
                {
                    sb = new StringBuilder();
                    kommunikationManger = new KommunikationManger(answerTyp);

                    string input;
                    string data;

                    writer.WriteLine("supsup");
                    writer.Flush();

                    while (true)
                    {
                        data = reader.ReadLine();
                        sb.AppendLine(data);

                        if (data == null)
                        {
                            Console.WriteLine();
                        }
                       

                        // Ok or Bereit
                        if (data[0].Equals('2'))
                        {
                            messageTyp = MessageTyp.OutputAndInput;
                            input = kommunikationManger.messageManger(messageTyp, sb.ToString());
                            
                            if(input == null)
                            {
                                Console.WriteLine();
                            }
                            if (input != "PRINT")
                            {
                                sb.Clear();
                                writer.WriteLine(input);
                                writer.Flush();
                            }
                            else
                            {
                                sb.Clear();
                                writer.WriteLine(input);
                                writer.Flush();
                            }


                        }
                        // verweigert or Unbekanntes Kommando
                        else if (data[0].Equals('5') || data[0].Equals('6'))
                        {
                            messageTyp = MessageTyp.Input;
                            input = kommunikationManger.messageManger(messageTyp, sb.ToString());
                            sb.Clear();
                            writer.WriteLine(input);
                            writer.Flush();
                        }
                    }
                }
            }


        }

        public void Dispose()
        {

        }
    }
}