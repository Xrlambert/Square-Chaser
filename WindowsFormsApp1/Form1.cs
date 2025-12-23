//Xavier Lambert ISC3U
//Square chaser project with semi realistic car movement as a test bed for final project while remaining separate utilising
//custom physics engine with first education from example, made from scratch afterwards.


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        float GreenAccel1 = 0.2f;
        float RedAccell2 = 0.2f;

        //Friction
        float friction1 = 0.09f;
        float friction2 = 0.09f;

        //Fwd/backward Speed
        float Hori1 = 0;
        float Vert1 = 0;

        //Max Speed
        float GreenMaxHori1 = 10f;
        float GreenMaxVert1 = 10f;

        public Form1()
        {
            InitializeComponent();

            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            
            timer.Tick += timer_Tick;
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            //call math for square speed, using passed variables to allow for more than one square
            SpeedMathSquare(GreenAccel1, Vert1, Hori1, friction1);
            SpeedLimit();

            x1 += Hori1;
            y1 += Vert1;
            Invalidate();
        }

        private void SpeedMathSquare(float acelerate, float vertical, float horizontal, float friction)
        {
            //apply speed
            if (UpDown) vertical -= acelerate;
            if (DownDown) vertical += acelerate;
            if (LeftDown) horizontal -= acelerate;
            if (RightDown) horizontal += acelerate;

            //apply friction
            if (!UpDown && !DownDown)
            {
                if (vertical > 0)
                {
                    vertical -= friction1;
                }
                if (vertical < 0)
                {
                    vertical += friction1;
                }
                //jitter/creep fix
                if (Math.Abs(vertical) < 0.1f)
                    vertical = 0;
            }
            if (!LeftDown && !RightDown)
            {
                if (horizontal > 0)
                {
                    horizontal -= friction1;
                }
                if (horizontal < 0)
                {
                    horizontal += friction1;
                }

                //jitter/creep fix
                if(Math.Abs(horizontal) < 0.1f)
                    horizontal = 0;
            }

            //Bounce square off walls when square location is out of bounds
            if (x1 < 15 || x1 > 1505) horizontal = -horizontal * 0.8f + 0.2f;
            if (y1 < 15 || y1 > 805) vertical = -vertical * 0.8f + 0.2f;
        }

        private void SpeedLimit()
        {
            //Limit top speed (Having to use vector)
            if (Hori1 > GreenMaxHori1) Hori1 = GreenMaxHori1;
            if (Hori1 < -GreenMaxHori1) Hori1 = -GreenMaxHori1;
            if (Vert1 > GreenMaxVert1) Vert1 = GreenMaxVert1;
            if (Vert1 < -GreenMaxVert1) Vert1 = -GreenMaxVert1;

            if (Math.Sqrt(Hori1 * Hori1 + Vert1 * Vert1) > GreenMaxHori1)
            {
                float angle = (float)Math.Atan2(Vert1, Hori1);
                Hori1 = GreenMaxHori1 * (float)Math.Cos(angle);
                Vert1 = GreenMaxHori1 * (float)Math.Sin(angle);
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
                /*case Keys.W:
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
                    break;*/
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
                /*case Keys.W:
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
                    break;*/
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            
            // Move origin to vehicle center.
            g.TranslateTransform(x1, y1);

            //sq1
            g.FillRectangle(Brushes.Green, -30, -15, 30, 30);
            g.ResetTransform();

            //sq2
            //g.TranslateTransform(x2, y2);

            //g.FillRectangle(Brushes.Red, -30, -15, 60, 30);
            //g.FillRectangle(Brushes.Black, -25, -10, 10, 20);
            //  g.ResetTransform();
            
        }
    }
}
