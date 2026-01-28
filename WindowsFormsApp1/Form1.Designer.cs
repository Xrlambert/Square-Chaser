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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.debugLabel = new System.Windows.Forms.Label();
            this.ScoreGreen = new System.Windows.Forms.Label();
            this.GreenVic = new System.Windows.Forms.Label();
            this.RedVic = new System.Windows.Forms.Label();
            this.Restart = new System.Windows.Forms.Button();
            this.Win = new System.Windows.Forms.Button();
            this.tutorialLabel = new System.Windows.Forms.Label();
            this.LeaderboardLabel = new System.Windows.Forms.Label();
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
            // GreenVic
            // 
            this.GreenVic.AutoSize = true;
            this.GreenVic.Font = new System.Drawing.Font("MS PGothic", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GreenVic.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.GreenVic.Location = new System.Drawing.Point(610, 328);
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
            this.RedVic.Location = new System.Drawing.Point(650, 328);
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
            this.Restart.Location = new System.Drawing.Point(740, 476);
            this.Restart.Name = "Restart";
            this.Restart.Size = new System.Drawing.Size(177, 80);
            this.Restart.TabIndex = 5;
            this.Restart.TabStop = false;
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
            this.Win.Location = new System.Drawing.Point(733, 339);
            this.Win.Name = "Win";
            this.Win.Size = new System.Drawing.Size(150, 49);
            this.Win.TabIndex = 6;
            this.Win.TabStop = false;
            this.Win.Text = "Start";
            this.Win.UseVisualStyleBackColor = false;
            this.Win.Click += new System.EventHandler(this.Win_Click);
            // 
            // tutorialLabel
            // 
            this.tutorialLabel.AutoSize = false;
            this.tutorialLabel.Width = 1460;
            this.tutorialLabel.Height = 630;
            this.tutorialLabel.Left = 20;
            this.tutorialLabel.Top = 20;
            this.tutorialLabel.BackColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.tutorialLabel.ForeColor = System.Drawing.Color.White;
            this.tutorialLabel.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.tutorialLabel.Padding = new System.Windows.Forms.Padding(20);
            this.tutorialLabel.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Regular);
            this.tutorialLabel.Text = @"HOW TO PLAY - SQUARE CHASER

OBJECTIVE:
Collect white points while avoiding the AI-controlled red square.
Each collision costs you 1 life (you start with 3 lives).

CONTROLS:
↑ ↓ ← → Arrow Keys = Move GREEN square around the screen

COLLECTIBLES:
⬜ WHITE = Points - Collect to increase your score
🟨 YELLOW = Speed Boost - Increases acceleration & max speed for ~5 seconds
🟦 BLUE = Ice Slow - Reduces opponent's acceleration for ~5 seconds

GAMEPLAY MECHANICS:
• Collect white squares to earn points
• Each point increases red AI speed by 5% (game gets harder progressively)
• Red AI gets faster and more aggressive as you score
• Wall bounces lose 20% energy
• Speed boost increases your velocity significantly
• Ice effect slows opponent's acceleration
• AI predicts your movement and intercepts

SCORING:
• 1 point = +5% red AI speed
• Survive longer and score more to reach the leaderboard
• Your scores are saved automatically

TIPS:
• Grab points quickly before AI catches up
• Use power-ups strategically to escape danger
• Bounce off walls to change direction unpredictably
• Try to maintain at least 1 life to keep playing

UI SHORTCUTS:
F11 = Toggle fullscreen (hides mouse)
Esc = Exit fullscreen (shows mouse, pauses game)

Click the START button below to begin!";
            this.tutorialLabel.Name = "tutorialLabel";
            this.tutorialLabel.TabIndex = 7;
            // 
            // leaderboardLabel
            // 
            this.leaderboardLabel = new System.Windows.Forms.Label();
            this.leaderboardLabel.AutoSize = false;
            this.leaderboardLabel.Width = 720;
            this.leaderboardLabel.Height = 630;
            this.leaderboardLabel.Left = 760;
            this.leaderboardLabel.Top = 20;
            this.leaderboardLabel.BackColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.leaderboardLabel.ForeColor = System.Drawing.Color.Cyan;
            this.leaderboardLabel.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.leaderboardLabel.Padding = new System.Windows.Forms.Padding(20);
            this.leaderboardLabel.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Regular);
            this.leaderboardLabel.Text = "Loading leaderboard...";
            this.leaderboardLabel.Name = "leaderboardLabel";
            this.leaderboardLabel.TabIndex = 8;
            this.leaderboardLabel.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1504, 781);
            this.Controls.Add(this.Win);
            this.Controls.Add(this.tutorialLabel);
            this.Controls.Add(this.Restart);
            this.Controls.Add(this.RedVic);
            this.Controls.Add(this.GreenVic);
            this.Controls.Add(this.ScoreGreen);
            this.Controls.Add(this.debugLabel);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label debugLabel;
        private System.Windows.Forms.Label ScoreGreen;
        private System.Windows.Forms.Label GreenVic;
        private System.Windows.Forms.Label RedVic;
        private System.Windows.Forms.Button Restart;
        private System.Windows.Forms.Button Win;
        private System.Windows.Forms.Label tutorialLabel;
        private System.Windows.Forms.Label LeaderboardLabel;
        private System.Windows.Forms.Label leaderboardLabel;
    }
}

