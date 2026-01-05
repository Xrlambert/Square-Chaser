//Xavier Lambert ISC3U
//Square chaser project with semi realistic car movement as a test bed for final project while remaining separate utilising
//custom physics engine with first in depth learning with an example project using in depth pseudocode and questions, made from scratch.
//two sets of coordinate draw squares that can be moved with arrow keys and WASD respectively with acceleration, friction, maximum speed
//and bounce off walls implemented. sends points to two functions to calculate speed and limit speed each tick of the timer before updating position and redrawing.


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Random rand = new Random();

        //Key States
        bool UpDown, DownDown, RightDown, LeftDown;
        bool WDown, SDown, ADown, DDown;

        //Position Variables
        float x1 = 50;
        float y1 = 300;

        float x2 = 30;
        float y2 = 30;

        //Accelleration
        float GreenAccel = 0.2f;
        float RedAccell = 0.2f;

        //Friction
        float friction1 = 0.09f;
        float friction2 = 0.09f;

        //Fwd/backward Speed
        float Hori1 = 0;
        float Vert1 = 0;
        float Hori2 = 0; 
        float Vert2 = 0;

        //Max Speed
        float GreenMaxHori = 10f;
        float GreenMaxVert = 10f; 
        float RedMaxHori = 10f;
        float RedMaxVert = 10f;

        //Variables for "powerups"
        float pointX, pointY;
        bool pointVisible = false;
        float pointLifetime = 2500; //ticks(roughly 5 seconds)
        float pointTimer = 0;
        int pelletSize = 10;
        int pointRespawnDelay = 120;   // 2 seconds
        int pointRespawnTimer = 0;
        bool pointGreen = false;
        bool pointRed = false;



        public Form1()
        {
            InitializeComponent();

            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            
            timer.Tick += timer_Tick;

            Thread.Sleep(1000);
            PlaceObjectRandomly(ref pointX, ref pointY);
            pointVisible = true;
            pointTimer = pointLifetime; 
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            //call math for square speed, using passed variables to allow for more than one square
            SpeedMathSquare(GreenAccel, ref Vert1, ref Hori1, friction1, ref x1, ref y1, UpDown, DownDown, LeftDown, RightDown);
            SpeedLimit(GreenMaxHori, ref Hori1, ref Vert1, GreenMaxVert);


            SpeedMathSquare(RedAccell, ref Vert2, ref Hori2, friction2, ref x2, ref y2, WDown, SDown, ADown, DDown);
            SpeedLimit(RedMaxHori, ref Hori2, ref Vert2, RedMaxVert);

            x1 += Hori1;
            y1 += Vert1;

            x2 += Hori2;
            y2 += Vert2;

            if (pointVisible)
            {
                pointTimer--;
                if (pointTimer <= 0)
                    pointVisible = false;
            }

            if (pointX +- 10 == x1 && pointY +- 10 == y1)
            {
                //ADD TO SCORE
                pointVisible = false;
            }

            if (pointX +- 10 == x2 && pointY +- 10 == y2)
            {
                //ADD TO SCORE
                pointVisible = false;
            }

            if (!pointVisible)
            {
                if (pointRespawnTimer > 0)
                {
                    pointRespawnTimer--;
                }
                else
                {
                    PlaceObjectRandomly(ref pointX, ref pointY);
                    pointVisible = true;
                    pointTimer = pointLifetime;
                    pointRespawnTimer = pointRespawnDelay;
                }
            }

            Invalidate();
            //debug for positioning of squares
            debugLabel.Text = $"G{x1}, {y1}\nR{x2}, {y2}\nPT{pointX}, {pointY}";
        }

        private void SpeedMathSquare(float acelerate, ref float vertical, ref float horizontal, float friction, ref float x, ref float y, bool Up, bool Down, bool Left, bool Right)
        {
            //apply speed
            if (Up) vertical -= acelerate;
            if (Down) vertical += acelerate;
            if (Left) horizontal -= acelerate;
            if (Right) horizontal += acelerate;

            //apply friction
            if (!Up && !Down)
            {
                if (vertical > 0)
                {
                    vertical -= friction;
                }
                if (vertical < 0)
                {
                    vertical += friction;
                }
                //jitter/creep fix
                if (Math.Abs(vertical) < 0.1f)
                    vertical = 0;
            }
            if (!Left && !Right)
            {
                if (horizontal > 0)
                {
                    horizontal -= friction;
                }
                if (horizontal < 0)
                {
                    horizontal += friction;
                }

                //jitter/creep fix
                if(Math.Abs(horizontal) < 0.1f)
                    horizontal = 0;
            }

            //Bounce square off walls when square location is out of bounds
            if (x < 25) x = 25;
            if (x > 1520) x = 1520;
            if (y < 5) y = 5;
            if (y > 790) y = 790;
            if (x == 25 || x == 1520) horizontal = -horizontal * 0.8f;
            if (y == 5 || y == 790) vertical = -vertical * 0.8f;
            return;
        }



        private void SpeedLimit(float maxHori, ref float horizontal, ref float vertical, float maxVert)
        {
            //Limit top speed (Having to use vector)
            if (horizontal > maxHori) horizontal = maxHori;
            if (horizontal < -maxHori) horizontal = -maxHori;
            if (vertical > maxVert) vertical = maxVert;
            if (vertical < -maxVert) vertical = -maxVert;

            if (Math.Sqrt(horizontal * horizontal + vertical * vertical) > maxHori)
            {
                float angle = (float)Math.Atan2(vertical, horizontal);
                horizontal = maxHori * (float)Math.Cos(angle);
                vertical = maxHori * (float)Math.Sin(angle); ////////////////////////////PROBLEM CAUSED BY FIRST VERTICAL ON THE LINE BENG VERT1 FROM GREEN INSTAD OF UNIVERSAL PARAMTER
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                // add boolean for each key condition
                case Keys.Up:
                    UpDown = false;
                    break;
                case Keys.Down:
                    DownDown = false;
                    break;
                case Keys.Left:
                    LeftDown = false;
                    break;
                case Keys.Right:
                    RightDown = false;
                    break;
                case Keys.W:
                    WDown = false;
                    break;
                case Keys.S:
                    SDown = false;
                    break;
                case Keys.A:
                    ADown = false;
                    break;
                case Keys.D:
                    DDown = false;
                    break;
            }
            //if (e.KeyCode == Keys.Up) UpDown = true;
            //if (e.KeyCode == Keys.Down) DownDown = true;
            //if (e.KeyCode == Keys.Left) LeftDown = true;
            //if (e.KeyCode == Keys.Right) RightDown = true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            { // add boolean for each key condition
                case Keys.Up:
                    UpDown = true;
                    break;
                case Keys.Down:
                    DownDown = true;
                    break;
                case Keys.Left:
                    LeftDown = true;
                    break;
                case Keys.Right:
                    RightDown = true;
                    break;
                case Keys.W:
                    WDown = true;
                    break;
                case Keys.S:
                    SDown = true;
                    break;
                case Keys.A:
                    ADown = true;
                    break;
                case Keys.D:
                    DDown = true;
                    break;
            }
        }

        private void PlaceObjectRandomly(ref float x, ref float y)
        {
            int margin = 40;
            x = rand.Next(margin, (this.ClientSize.Width - margin));
            y = rand.Next(margin, (this.ClientSize.Height - margin));
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            if (pointVisible) { g.FillRectangle(Brushes.White, pointX - pelletSize / 2, pointY - pelletSize / 2, pelletSize, pelletSize); }

            //sq1
            g.TranslateTransform(x1, y1);
            g.FillRectangle(Brushes.Lime, -30, -15, 20, 20);
            g.ResetTransform();

            //sq2
            g.TranslateTransform(x2, y2);
            g.FillRectangle(Brushes.MediumVioletRed, -30, -15, 20, 20);
            g.ResetTransform();
            
        }
    }
}
