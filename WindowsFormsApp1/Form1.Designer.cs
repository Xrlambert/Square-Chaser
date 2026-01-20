namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.debugLabel = new System.Windows.Forms.Label();
            this.ScoreGreen = new System.Windows.Forms.Label();
            this.RedScore = new System.Windows.Forms.Label();
            this.GreenVic = new System.Windows.Forms.Label();
            this.RedVic = new System.Windows.Forms.Label();
            this.Restart = new System.Windows.Forms.Button();
            this.Win = new System.Windows.Forms.Button();
            this.StartButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 20;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // debugLabel
            // 
            this.debugLabel.AutoSize = true;
            this.debugLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.debugLabel.Location = new System.Drawing.Point(13, 13);
            this.debugLabel.Name = "debugLabel";
            this.debugLabel.Size = new System.Drawing.Size(0, 13);
            this.debugLabel.TabIndex = 0;
            // 
            // ScoreGreen
            // 
            this.ScoreGreen.AutoSize = true;
            this.ScoreGreen.Font = new System.Drawing.Font("MS PGothic", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScoreGreen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ScoreGreen.Location = new System.Drawing.Point(446, 13);
            this.ScoreGreen.Name = "ScoreGreen";
            this.ScoreGreen.Size = new System.Drawing.Size(34, 35);
            this.ScoreGreen.TabIndex = 1;
            this.ScoreGreen.Text = "0";
            // 
            // RedScore
            // 
            this.RedScore.AutoSize = true;
            this.RedScore.Font = new System.Drawing.Font("MS PGothic", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RedScore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.RedScore.Location = new System.Drawing.Point(1090, 11);
            this.RedScore.Name = "RedScore";
            this.RedScore.Size = new System.Drawing.Size(34, 35);
            this.RedScore.TabIndex = 2;
            this.RedScore.Text = "0";
            // 
            // GreenVic
            // 
            this.GreenVic.AutoSize = true;
            this.GreenVic.Font = new System.Drawing.Font("MS PGothic", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GreenVic.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.GreenVic.Location = new System.Drawing.Point(590, 323);
            this.GreenVic.Name = "GreenVic";
            this.GreenVic.Size = new System.Drawing.Size(413, 64);
            this.GreenVic.TabIndex = 3;
            this.GreenVic.Text = "GREEN WINS!";
            this.GreenVic.Visible = false;
            // 
            // RedVic
            // 
            this.RedVic.AutoSize = true;
            this.RedVic.Font = new System.Drawing.Font("MS PGothic", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RedVic.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.RedVic.Location = new System.Drawing.Point(630, 323);
            this.RedVic.Name = "RedVic";
            this.RedVic.Size = new System.Drawing.Size(332, 64);
            this.RedVic.TabIndex = 4;
            this.RedVic.Text = "RED WINS!";
            this.RedVic.Visible = false;
            // 
            // Restart
            // 
            this.Restart.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Restart.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Restart.ForeColor = System.Drawing.Color.White;
            this.Restart.Location = new System.Drawing.Point(704, 478);
            this.Restart.Name = "Restart";
            this.Restart.Size = new System.Drawing.Size(177, 80);
            this.Restart.TabIndex = 5;
            this.Restart.Text = "Restart";
            this.Restart.UseVisualStyleBackColor = false;
            this.Restart.Visible = false;
            this.Restart.Click += new System.EventHandler(this.Restart_Click);
            // 
            // Win
            // 
            this.Win.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Win.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Win.ForeColor = System.Drawing.Color.White;
            this.Win.Location = new System.Drawing.Point(166, 12);
            this.Win.Name = "Win";
            this.Win.Size = new System.Drawing.Size(74, 36);
            this.Win.TabIndex = 6;
            this.Win.Text = "Win";
            this.Win.UseVisualStyleBackColor = false;
            this.Win.Click += new System.EventHandler(this.Win_Click);
            // 
            // StartButton
            // 
            this.StartButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.StartButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartButton.ForeColor = System.Drawing.Color.White;
            this.StartButton.Location = new System.Drawing.Point(745, 340);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(95, 47);
            this.StartButton.TabIndex = 7;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = false;
            this.StartButton.Visible = false;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1504, 781);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.Win);
            this.Controls.Add(this.Restart);
            this.Controls.Add(this.RedVic);
            this.Controls.Add(this.GreenVic);
            this.Controls.Add(this.RedScore);
            this.Controls.Add(this.ScoreGreen);
            this.Controls.Add(this.debugLabel);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label debugLabel;
        private System.Windows.Forms.Label ScoreGreen;
        private System.Windows.Forms.Label RedScore;
        private System.Windows.Forms.Label GreenVic;
        private System.Windows.Forms.Label RedVic;
        private System.Windows.Forms.Button Restart;
        private System.Windows.Forms.Button Win;
        private System.Windows.Forms.Button StartButton;
    }
}

