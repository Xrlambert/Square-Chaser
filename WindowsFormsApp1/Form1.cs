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
        float accel1 = 0.2f;
        float accel2 = 0.2f;

        //Friction
        float friction1 = 0.09f;
        float friction2 = 0.09f;

        //Fwd/backward Speed
        float speed1 = 0;

        float speed2 = 0;

        //Max Speed
        float maxSpeed1 = 10f;

        public Form1()
        {
            InitializeComponent();

            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
        }


        private void timer_Tick(object sender, EventArgs e)
        {

            //apply speed
            if (UpDown) 
            {
                speed1 += accel1;
                
            }
            if (DownDown)
            {
                speed1 -= accel1;
            }

            if (!UpDown && !DownDown)
            {
                if (speed1 > 0)
                {
                    speed1 -= friction1;
                }
                if (speed1 < 0)
                {
                    speed1 += friction1;
                }
            }

            x1 += speed1;
            y1 += speed1;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
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
            {
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
            g.FillRectangle(Brushes.Green, -30, -15, 60, 30);
            g.FillRectangle(Brushes.Black, -25, -10, 10, 20);
            g.ResetTransform();

            //sq2
            //g.TranslateTransform(x2, y2);

            //g.FillRectangle(Brushes.Red, -30, -15, 60, 30);
            //g.FillRectangle(Brushes.Black, -25, -10, 10, 20);
            //  g.ResetTransform();





        }
    }
}
