using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Netzwerklabrinth_V_WPF
{
    class Labyrinth : IDisposable
    {
        private Socket socket;
        private NetworkStream networkStream;
        private StreamReader reader;
        private StreamWriter writer;

        private int width = 1024;
        private int height = 1024;
        private int depth = 1;

        private byte[,] map = new byte[32, 32];
        
        private int playerX = 0;
        private int playerY = 0;

        public Labyrinth(int width, int height, int depth)
        {
            this.width = width;
            this.height = height;
            this.depth = depth;
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect("labyrinth.ctrl-s.de", 50000);
            socket.NoDelay = true;
            networkStream = new NetworkStream(socket);
            writer = new StreamWriter(networkStream);
            reader = new StreamReader(networkStream);

            writer.WriteLine("WIDTH {0}", width);
            writer.Flush();
            writer.WriteLine("HEIGHT {0}", height);
            writer.Flush();
            writer.WriteLine("DEPTH {0}", depth);
            writer.Flush();
            writer.WriteLine("START");
            writer.Flush();

            // while (recv().Code != 2);

            // Wait for the message with code 9 and extract player coordinates
            Message message;
            while ((message = recv()).Code != 9 || !message.Text.Contains("X:") || !message.Text.Contains("Y:"))
            {
                writer.WriteLine("anything");
                writer.Flush();
            }

            // Extract coordinates from the message text
            var coordinates = message.Text.Split(new[] { '[', ']', 'X', 'Y', ':', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            playerX = int.Parse(coordinates[0]);
            playerY = int.Parse(coordinates[1]);

        }

        public void Up()
        {
            send("UP");

            if (recv().Code != 9)
                throw new InvalidOperationException("Can't move UP.");

            playerY--;
        }

        public void Down()
        {
            send("DOWN");

            if (recv().Code != 9)
                throw new InvalidOperationException("Can't move DOWN.");

            playerY++;
        }

        public void Right()
        {
            send("RIGHT");

            if (recv().Code != 9)
                throw new InvalidOperationException("Can't move RIGHT.");

            playerX++;
        }

        public void Left()
        {
            send("LEFT");

            if (recv().Code != 9)
                throw new InvalidOperationException("Can't move LEFT.");

            playerX--;
        }

        /// <summary>
        /// Führt alle bewegungsbefehle in einem Rutsch aus und frägt danach ab, ob diese Erfolgreich gewesen sind.
        /// Führt danach eine PRINT-Anweisung aus.
        /// </summary>
        /// <param name="directions">Die Richtungen, in die der Reihe nach gegangen werden soll.</param>
        public void QueuedCommandsAndPrint(Stack<Direction> directions)
        {
            int lastPosPlayerY = playerY;
            int lastPosPlayerX = playerX;

            while (directions.Count > 0)
            {
                Direction direction = directions.Pop();

                if (direction == Direction.Up)
                    Up();

                else if (direction == Direction.Down)
                    Down();

                else if (direction == Direction.Right)
                    Right();

                else if (direction == Direction.Left)
                    Left();
            }
            map[lastPosPlayerY, lastPosPlayerX] = 1;
            Print();
        }

        public void Print()
        {
            send("PRINT");

            int result = 0;
            int y = playerY - 5;
            int x = playerX - 5;


            while (result != 9)
            {
                Message newMessage = recv();
                result = newMessage.Code;

                if (newMessage.Code == 4)
                {
                    for (int i = 0; i < newMessage.Text.Length; i++, x++)
                    {
                        // 0 = Null
                        // 1 = Corridor
                        // 2 = Wall
                        // 3 = Player
                        // 4 = Target
                        // 5 = Rand
                        // 6 = Path

                        if (newMessage.Text[i] == ' ')
                            map[y, x] = 1;

                        else if (newMessage.Text[i] == 'W')
                            map[y, x] = 2;

                        else if (newMessage.Text[i] == 'P')
                            map[y, x] = 3;

                        else if (newMessage.Text[i] == 'Z')
                            map[y, x] = 4;

                    }
                    x = playerX - 5;
                    y++;
                }
            }
            Message nnewMessage = recv();
            result = nnewMessage.Code;
            nnewMessage = recv();
            result = nnewMessage.Code;
            nnewMessage = recv();
            result = nnewMessage.Code;
            nnewMessage = recv();
            result = nnewMessage.Code;

        }

        public AStar GetRouterFromPlayer()
        {
            return new AStar(map, playerX, playerY, Mission.FromPlayer);
        }

        public AStar GetRouterFromStart()
        {
            return new AStar(map, playerX, playerY, Mission.FromStart);
        }

        public int PlayerX => playerX;

        public int PlayerY => playerY;

        public int TargetX => 1023;

        public int TargetY => 1023;

        public int PlayerStartX => 512;

        public int PlayerStartY => 512;


        /// <summary>
        /// Liefert einen Ausschnitt des Labyrinths zurück, der immer genau (right-left) breit ist
        /// und (bottom-top) hoch ist. Der Ausschnitt startet horizontal bei left und endet bei right,
        /// während er vertikal bei top startet und bei bottom endet.
        /// 
        /// Das zurückgelieferte byte[,] ist immer nur so groß, wie der Ausschnitt.             
        /// </summary>
        /// <param name="left">Die linke Position im Labyrinth.</param>
        /// <param name="top">Die obere Position im Labyrinth.</param>
        /// <param name="right">Die rechte Position im Labyrinth.</param>
        /// <param name="bottom">Die untere Position im Labyrinth.</param>
        /// <returns>Ein byte[,]-Array der entsprechenden Größe.</returns>
        public byte[,] Map => map;

        public byte[,] Get(int left, int top, int right, int bottom)
        {
            int y = 0;
            int x = 0;


            byte[,] mapCutout = new byte[bottom - top, right - left];

            for (int Top = top; Top < bottom; Top++, y++)
            {
                if (Top < 0)
                {
                    while (x < right - left)
                    {
                        mapCutout[y, x] = 5;
                        x++;
                    }
                }
                else if (Top >= 1024)
                {
                    while (x < right - left)
                    {
                        mapCutout[y, x] = 5;
                        x++;
                    }
                }
                else
                {
                    for (int Left = left; Left < right; Left++, x++)
                    {
                        if (Left < 0)
                            mapCutout[y, x] = 5;

                        else if (Left >= 1024)
                            mapCutout[y, x] = 5;

                        else
                            mapCutout[y, x] = map[Top, Left];
                    }
                }
                x = 0;
            }
            return mapCutout;
        }

        //public byte[,] Get(int width, int height)
        //{
        //    int left = playerX - width / 2;
        //    int top = playerY - height / 2;
        //    int right = playerX + width / 2;
        //    int bottom = playerY + height / 2;

        //    byte[,] mapCutout = new byte[height, width];

        //    for (int y = 0; y < height; y++)
        //    {
        //        for (int x = 0; x < width; x++)
        //        {
        //            int mapY = top + y;
        //            int mapX = left + x;

        //            if (mapY < 0 || mapY >= 1024 || mapX < 0 || mapX >= 1024)
        //            {
        //                mapCutout[y, x] = 5; // Rand
        //            }
        //            else
        //            {
        //                mapCutout[y, x] = map[mapY, mapX];
        //            }
        //        }
        //    }

        //    return mapCutout;
        //}

        private Message recv()
        {
            Message message = new Message(reader.ReadLine());
            return message;
        }

        private void send(string message)
        {
            writer.WriteLine(message);
            writer.Flush();
        }

        public void Dispose()
        {
            reader.Dispose();
            writer.Dispose();
            networkStream.Dispose();
            socket.Dispose();
        }
    }
}
