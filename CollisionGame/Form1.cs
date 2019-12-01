using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CollisionGame
{
    public partial class Form1 : Form
    {
        double speed = 1.3;
        double velX = 0, velY = 0;
        bool onG = false;
        double grav = 0.5, friction = 0.95;
        Rectangle[] platforms = new Rectangle[50];
        Rectangle box;
        Random rnd = new Random();

        bool left, right;

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            {
                switch (e.KeyCode)
                {
                    case Keys.W:
                    case Keys.Up:
                    case Keys.Space:
                        if (onG)
                            velY = -10;
                        break;
                    case Keys.A:
                    case Keys.Left:
                        left = true;
                        break;
                    case Keys.D:
                    case Keys.Right:
                        right = true;
                        break;
                    case Keys.Escape:
                        this.Close();
                        break;
                }
            }
        }


        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                case Keys.Up:
                case Keys.Space:
                    if (velY < -3)
                        velY = -3;
                    break;
                case Keys.A:
                case Keys.Left:
                    left = false;
                    break;
                case Keys.D:
                case Keys.Right:
                    right = false;
                    break;
            }
        }

        private void Update_Tick(object sender, EventArgs e)
        {

            if (left)
                velX = -speed + velX;

            if (right)
                velX = speed + velX;

            box.X += (int)(velX);
            box.Y += (int)velY;

            if (onG)
                velX *= friction;
            else
                velY += grav;

            onG = false;

            for (int i = 0; i < 50; i++)
            {
                if (box.X > platforms[i].X && box.X < platforms[i].X + platforms[i].Width && box.Y > platforms[i].Y && box.Y < platforms[i].Y + platforms[i].Height)
                {
                    box.Y = platforms[i].Y;
                    onG = true;
                }
            }
            this.Refresh();
            //map.SetPixel(player.Location.X, player.Location.Y, Color.Red);
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
            this.CreateGraphics().FillRectangle(myBrush, new Rectangle(this.ClientRectangle.Location, this.ClientRectangle.Size));
            myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            this.CreateGraphics().FillRectangle(myBrush, new Rectangle(box.X - box.Width / 2, box.Y - box.Height, 25, 25));
            myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Gray);
            for (int i = 0; i < platforms.Length; i++)
                this.CreateGraphics().FillRectangle(myBrush, platforms[i]);
        }
        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < 50; i++)
                platforms[i] = new Rectangle(rnd.Next(this.ClientRectangle.Width), rnd.Next(this.ClientRectangle.Height), rnd.Next(30, 130), rnd.Next(20, 50));
            box = new Rectangle(200, 200, 25, 25);
        }
    }
}
