using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Netzwerklabrinth_V_WPF
{
    public partial class AutomaticForm : Form
    {
        Labyrinth labyrinth;
        AStar aStern;
        public AutomaticForm()
        {
            InitializeComponent();
        }

        private void AutomaticForm_Load(object sender, EventArgs e)
        {
            panel.Visible = false;
            labyrinth = new Labyrinth(32, 32, 1);
            labyrinth.Print();
            Draw();
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            aStern = labyrinth.GetRouterFromPlayer();
            labyrinth.QueuedCommandsAndPrint(aStern.directions);
            Draw();
            if (aStern.Current == 4)
            {
                aStern = labyrinth.GetRouterFromStart();
                pictureBoxCutout.Visible = false;
                panel.Visible = true;
                pictureBoxFull.SizeMode = PictureBoxSizeMode.AutoSize;
                GameEnde(aStern.GetMap);
                timer.Stop();
            }
        }
        private void Draw()
        {
            Bitmap bitmap = new Bitmap(@"E:\Ctrl-s\Netzwerklabrinth_V_WPF - Kopie\Netzwerklabrinth_V_WPF\First.bmp");
            Graphics GFX = Graphics.FromImage(bitmap);

            int width = pictureBoxCutout.Width / 10;
            int height = pictureBoxCutout.Height / 10;

            int left = labyrinth.PlayerX - width / 2;
            int top = labyrinth.PlayerY - height / 2;

            byte[,] data = labyrinth.Get(left, top, left + width, top + height);
            //byte[,] data = labyrinth.Get(width, height);

            int Left = 0, Top = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    switch (data[y, x])
                    {
                        case 1:
                            GFX.FillRectangle(Brushes.LightCyan, new Rectangle(Left, Top, 20, 20));
                            break;
                        case 2:
                            GFX.FillRectangle(Brushes.Red, new Rectangle(Left, Top, 20, 20));
                            break;
                        case 3:
                            GFX.FillRectangle(Brushes.Blue, new Rectangle(Left, Top, 20, 20));
                            break;
                        case 4:
                            GFX.FillRectangle(Brushes.Green, new Rectangle(Left, Top, 20, 20));
                            break;
                        case 5:
                            GFX.FillRectangle(Brushes.Gray, new Rectangle(Left, Top, 20, 20));
                            break;
                        default:
                            GFX.FillRectangle(Brushes.White, new Rectangle(Left, Top, 20, 20));
                            break;
                    }
                    Left += 10;
                }
                Top += 10;
                Left = 0;
            }
            pictureBoxCutout.Image = bitmap;
        }

        private void GameEnde(byte[,] map)
        {
            Bitmap bitmap = new Bitmap(@"E:\Ctrl-s\Netzwerklabrinth_V_WPF - Kopie\Netzwerklabrinth_V_WPF\Bitmap.bmp");
            Graphics GFX = Graphics.FromImage(bitmap);

            int Le = 0; int To = 0;
            for (int y = 488; y < 1000; y++)
            {
                for (int x = 488; x < 1000; x++)
                {
                    switch (map[y, x])
                    {
                        case 1:
                            GFX.FillRectangle(Brushes.LightCyan, new Rectangle(Le, To, 20, 20));
                            break;
                        case 2:
                            GFX.FillRectangle(Brushes.Red, new Rectangle(Le, To, 20, 20));
                            break;
                        case 3:
                            GFX.FillRectangle(Brushes.Blue, new Rectangle(Le, To, 20, 20));
                            break;
                        case 4:
                            GFX.FillRectangle(Brushes.Green, new Rectangle(Le, To, 20, 20));
                            break;
                        case 5:
                            GFX.FillRectangle(Brushes.Gray, new Rectangle(Le, To, 20, 20));
                            break;
                        case 6:
                            GFX.FillRectangle(Brushes.DeepSkyBlue, new Rectangle(Le, To, 20, 20));
                            break;
                        default:
                            GFX.FillRectangle(Brushes.White, new Rectangle(Le, To, 20, 20));
                            break;
                    }
                    Le += 10;
                }
                Le = 0;
                To += 10;
            }
            pictureBoxFull.Image = bitmap;
        }
    }
}
