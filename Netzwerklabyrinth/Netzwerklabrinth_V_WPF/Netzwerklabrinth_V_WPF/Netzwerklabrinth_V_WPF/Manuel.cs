using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Netzwerklabrinth_V_WPF
{

    public partial class ManuelForm : Form
    {
        private string input = "";
        private Labyrinth labyrinth;
        public ManuelForm()
        {
            InitializeComponent();
        }

        private void ManuelForm_Load(object sender, EventArgs e)
        {
            panel.Visible = false;
            labyrinth = new Labyrinth(32, 32, 1);
            labyrinth.Print();
            Draw();
            timer.Start();
        }

        private void Draw()
        {
            Bitmap bitmap = new Bitmap(@"E:\Ctrl-s\Netzwerklabrinth_V_WPF - Kopie\Netzwerklabrinth_V_WPF\First.bmp");
            Graphics GFX = Graphics.FromImage(bitmap);

            int width = pictureBoxCutout.Width / 10;
            int height = pictureBoxCutout.Height / 10;

            int left = labyrinth.PlayerX - (width / 2);
            int top = labyrinth.PlayerY - (height / 2);

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
                            GFX.FillRectangle(Brushes.Blue, new Rectangle(Left, Top, 20, 20));
                            break;
                        case 5:
                            GFX.FillRectangle(Brushes.Gray, new Rectangle(Left, Top, 20, 20));
                            break;
                        default:
                            GFX.FillRectangle(Brushes.White, new Rectangle(Left, Top, 20, 20));
                            break;
                    }
                    Left += 20;
                }
                Top += 20;
                Left = 0;
            }
            pictureBoxCutout.Image = bitmap;
        }

        private void ManuelForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                input = "UP";
            }
            else if (e.KeyCode == Keys.Down)
            {
                input = "DOWN";
            }
            else if (e.KeyCode == Keys.Left)
            {
                input = "LEFT";
            }
            else if (e.KeyCode == Keys.Right)
            {
                input = "RIGHT";
            }
        }
   

        private void timer_Tick(object sender, EventArgs e)
        {
            switch (input)
            {
                case "UP":
                    try
                    {
                        labyrinth.Up();
                        goto case "PRINT";
                    }
                    catch (Exception exc)
                    {
                    }
                    break;
                case "DOWN":
                    try
                    {
                        labyrinth.Down();
                        goto case "PRINT";
                    }
                    catch (Exception exc)
                    {
                    }
                    break;
                case "RIGHT":
                    try
                    {
                        labyrinth.Right();
                        goto case "PRINT";
                    }
                    catch (Exception exc)
                    {
                    }
                    break;
                case "LEFT":
                    try
                    {
                        labyrinth.Left();
                        goto case "PRINT";
                    }
                    catch (Exception exc)
                    {        
                    }
                    break;
                case "PRINT":
                    labyrinth.Print();
                    Draw();
                    break;
            }
            input = "";
        }
    }
}

