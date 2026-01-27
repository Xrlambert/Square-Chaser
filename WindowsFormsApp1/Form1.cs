// ISC3U Final Project - Square Chaser Game
// Author: Xavier Lambert
// 
// A 2D arcade-style game featuring two player-controlled squares competing to collect points.
// Demonstrates custom physics engine implementation including acceleration, friction, velocity clamping,
// wall bouncing, and power-up systems. The game supports both keyboard-controlled and AI-controlled players.
// 
// Key Features:
// - Physics-based movement with acceleration and friction
// - Vector-based velocity limiting (magnitude clamping)
// - Dynamic power-up system (Speed boost, Ice slow effect)
// - Simple predictive AI opponent
// - Full-screen windowed mode support
// - Real-time score tracking
// 
// Technical Details:
// - Two independent players controlled by arrow keys and WASD (AI uses WASD)
// - Three collectible types: Points (white), Speed (yellow), Ice (blue)
// - Power-up effects: Speed (temporary acceleration boost), Ice (reduced acceleration)
// - Win condition: First to 10 points
// - Dynamic collision detection for pickups and wall bouncing
// 
// Methods Overview:
// Movement: SpeedMathSquare, SpeedLimit
// Game Logic: timer_Tick, AddPoint, ApplySpeedBonus, ApplyIceEffect
// AI: AIMath (predictive interception)
// Utilities: PlaceObjectRandomly, IsInputKey, ProcessCmdKey
// Input: Form1_KeyDown, Form1_KeyUp, Form1_KeyPress
// Rendering: OnPaint

using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

namespace WindowsFormsApp1
{
    /// 
    /// Main game form for Square Chaser.
    /// Handles all game logic, physics simulation, input processing, rendering, and AI behavior.
    /// 
    public partial class Form1 : Form
    {
        /// Shared random number generator for object placement and initialization.
        Random rand = new Random();

        //Key States
        /// True when Up arrow key is pressed.
        bool UpDown, DownDown, RightDown, LeftDown;
        /// True when W, A, S, D keys are pressed (reserved for second player/AI control).
        bool WDown, SDown, ADown, DDown;

        //Position Variables
        /// Green square X position (screen coordinates).
        float x1 = 50;
        /// Green square Y position (screen coordinates).
        float y1 = 300;

        /// Red square X position (screen coordinates).
        float x2 = 30;
        /// Red square Y position (screen coordinates).
        float y2 = 30;

        //Acceleration
        /// Base acceleration for green square (increased by speed power-up or decreased by ice power-up).
        float GreenAccel = 0.2f;
        /// Base acceleration for red square (slightly higher than green).
        float RedAccell = 0.22f;

        //Friction
        /// Friction applied to green square when no input is detected (slows movement).
        float friction1 = 0.09f;
        /// Friction applied to red square when no input is detected.
        float friction2 = 0.11f;

        //Forward/Backward Speed (velocity components)
        /// Green square horizontal velocity component (X-axis movement).
        float Hori1 = 0;
        /// Green square vertical velocity component (Y-axis movement).
        float Vert1 = 0;
        /// Red square horizontal velocity component (X-axis movement).
        float Hori2 = 0;
        /// Red square vertical velocity component (Y-axis movement).
        float Vert2 = 0;

        //Max Speed
        /// Maximum horizontal speed cap for green square (also used for radial magnitude limiting).
        float GreenMaxHori = 10f;
        /// Maximum vertical speed cap for green square.
        float GreenMaxVert = 10f;
        /// Maximum horizontal speed cap for red square.
        float RedMaxHori = 11f;
        /// Maximum vertical speed cap for red square.
        float RedMaxVert = 11f;

        //Variables for "powerups" (point collectible)
        /// Point collectible X position.
        float pointX, pointY;
        /// Whether the point collectible is currently visible and active on screen.
        bool pointVisible = false;
        /// Lifetime duration (in ticks) before point collectible automatically disappears (~5 seconds).
        float pointLifetime = 300; //ticks(roughly 5 seconds)
        /// Countdown timer tracking remaining lifetime of visible point collectible.
        float pointTimer = 0;
        /// Visual size in pixels for point collectible (also used for collision detection padding).
        int pelletSize = 10;
        /// Delay (in ticks) before point respawns after being collected or timing out (~2 seconds).
        int pointRespawnDelay = 120;   // 2 seconds
        /// Countdown timer for point respawn delay.
        int pointRespawnTimer = 0;
        bool pointGreen = false;
        bool pointRed = false;

        string score = "";
        /// Green player's current score (collected points).
        int gScore;
        /// Red player's current score (collected points).
        int rScore;

        //Speed power-up state
        /// Speed power-up X position on screen.
        float speedX, speedY;
        /// Whether the speed power-up is currently visible and active.
        bool speedVisible = false;
        /// Lifetime duration (in ticks) before speed power-up disappears if not collected (~5 seconds).
        float speedLifetime = 300; //ticks(roughly 5 seconds)
        /// Duration (in ticks) that speed boost effect remains active on the affected player (~5.1 seconds).
        float playerEffectSpeed = 320; //ticks(roughly 5.1 seconds)  
        /// Countdown timer for speed power-up visibility.
        float speedTimer = 0;
        /// Visual size in pixels for speed power-up.
        int speedSize = 10;
        /// Delay (in ticks) before speed power-up respawns after being collected or timing out.
        int speedRespawnDelay = 450;   // 
        /// Countdown timer for speed power-up respawn delay.
        int speedRespawnTimer = 0;
        bool speedGreen = false;
        bool speedRed = false;
        /// Whether any player currently has an active speed boost effect.
        bool speedActive = false;

        //Ice power-up state (slows player acceleration)
        /// Ice power-up X position on screen.
        float IceX, IceY;
        /// Whether the ice power-up is currently visible and active.
        bool IceVisible = false;
        /// Lifetime duration (in ticks) before ice power-up disappears if not collected (~5 seconds).
        float IceLifetime = 300; //ticks(roughly 5 seconds)
        /// Duration (in ticks) that ice effect remains active on the affected player (~5.1 seconds).
        float playerEffectIce = 320; //ticks(roughly 5.1 seconds)  
        /// Countdown timer for ice power-up visibility.
        float IceTimer = 0;
        /// Visual size in pixels for ice power-up.
        int IceSize = 10;
        /// Delay (in ticks) before ice power-up respawns after being collected or timing out.
        int IceRespawnDelay = 450;   // 
        /// Countdown timer for ice power-up respawn delay.
        int IceRespawnTimer = 0;
        bool IceGreen = false;
        bool IceRed = false;
        /// Whether any player currently has an active ice effect reducing their acceleration.
        bool IceActive = false;
        /// Tracks which player (if any) is affected by ice: true=green, false=red.
        bool iceGreen = false;

        /// Enables AI control for the red square to pursue the green square.
        bool AIEnabled = false;

        /// AI prediction horizon (in ticks) for calculating where green square will be. Adjustable via W/S keys.
        private float predictionTime = 7f;

        /// Stopwatch to track green square survival time from game start.
        private Stopwatch survivalTimer = new Stopwatch();
        

        /// Number of lives remaining for green square before elimination.
        private int greenLives = 3;

        /// Size of collision detection radius for player-to-player collisions.
        private int playerCollisionRadius = 15;

        /// Tracks if green square has been eliminated.
        private bool greenEliminated = false;

        /// Duration (in ticks) to display red flash on collision.
        private int collisionFlashDuration = 0;

        /// Maximum flash duration in ticks when collision occurs.
        private const int FLASH_DURATION = 15;

        
        /// History buffer to track green square position over time (approximately 4 seconds)
        private int greenHistorySize = 200;
        private float[] greenPosHistoryX;
        private float[] greenPosHistoryY;
        /// Current write index into circular history buffer
        private int greenHistoryIndex = 0;
        /// Flag indicating whether history buffer has been filled at least once
        private bool greenHistoryFilled = false;

        /// Tracks whether red AI is in aggressive "speed punishment" mode
        private bool redAggressive = false;
        /// Countdown timer for how long aggressive mode lasts
        private int redAggressiveTimer = 0;
        /// Duration (in ticks) that aggressive mode persists (~3 seconds)
        private int redAggressiveDurationTicks = 150;
        /// Saved normal acceleration for restoration
        private float savedRedAccell;
        /// Saved normal max horizontal speed for restoration
        private float savedRedMaxHori;
        /// Saved normal max vertical speed for restoration
        private float savedRedMaxVert;
        /// Phase of aggressive mode: 0=slow (punish idle), 1=fast (chase aggressively)
        private int aggressivePhase = 0;
        /// Duration of each phase (slow phase, then fast phase)
        private int phaseTickDuration = 75;

        bool debug = false;

        /// Counter for idle detection checks (only every few frames)
        private int idleCheckCounter = 0;
        /// Check idle detection every this many frames (~0.5 sec around 30 frames)
        private const int IDLE_CHECK_INTERVAL = 30;

        /// Cached hearts display string to avoid rebuilding every frame
        private string cachedHeartsDisplay = "❤ ❤ ❤";
        /// Last greenLives value
        private int lastCachedLives = 3;
        // ====================================================================
        /// 
        /// Initializes a new instance of the Form1 class.
        /// Sets up event handlers, configures full-screen mode, initializes game objects, and positions both players.
        /// 
        public Form1()
        {
            InitializeComponent();

            survivalTimer.Stop();

            // Configure fullscreen borderless window
            this.FormBorderStyle = FormBorderStyle.None; 
            this.Bounds = Screen.PrimaryScreen.Bounds; 
            this.TopMost = true;

            Thread.Sleep(1000);
            // Randomly place all three collectibles
            PlaceObjectRandomly(ref speedX, ref speedY);
            PlaceObjectRandomly(ref IceX, ref IceY);

            // Initialize collectible visibility and timers
            speedVisible = true;
            speedTimer = speedLifetime;
            IceVisible = true;
            IceTimer = IceLifetime;

            // Randomly position both players at opposite sides of screen
            x1 = rand.Next(100, 400);
            y1 = rand.Next(100, 400);
            x2 = rand.Next(600, 1200);
            y2 = rand.Next(300, 600);

            // Initialize green lives and survival timer
            greenLives = 3;
            greenEliminated = false;
            survivalTimer.Reset();  // Reset but don't start yet

            // ===== Initialize history arrays for idle detection =====
            greenPosHistoryX = new float[greenHistorySize];
            greenPosHistoryY = new float[greenHistorySize];
            // Initialize with starting position to avoid spurious movement detection
            for (int i = 0; i < greenHistorySize; i++)
            {
                greenPosHistoryX[i] = x1;
                greenPosHistoryY[i] = y1;
            }
            greenHistoryFilled = false;
            greenHistoryIndex = 0;
            redAggressive = false;
            // ==========================================================

            debugLabel.Visible = false;
        }

        /// 
        /// Overrides key input handling to ensure arrow keys are treated as game input rather than focus navigation.
        /// 
        /// <name= "keyData">\ The key being queried.
        /// True if the key is an arrow key (should be handled by game); otherwise base implementation.
        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                    return true;
            }
            return base.IsInputKey(keyData);
        }

        /// 
        /// Processes global command keys for fullscreen mode toggling (F11) and application exit (Escape).
        /// Escape key pauses the game and shows the mouse cursor.
        /// 
        /// <name="msg">The Windows message being processed.
        /// <name="keyData">The key being processed.
        /// <returns>True if the key was handled; false to continue propagating.</returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Exit fullscreen and pause game
            if (keyData == Keys.Escape)
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
                this.TopMost = false;
                timer.Enabled = false;
                Cursor.Show();  // Show mouse cursor when pausing
                return true;
            }

            // Enter fullscreen and resume game
            if (keyData == Keys.F11)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = true;
                timer.Enabled = true;
                Cursor.Hide();  // Hide mouse cursor when resuming
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }



        /// 
        /// Main game update loop invoked every timer tick (~60Hz).
        /// Updates physics, applies power-ups, processes AI, checks win conditions, and triggers rendering.
        /// 
        private void timer_Tick(object sender, EventArgs e)
        {
            // Update green square: apply acceleration, friction, and boundary collision
            SpeedMathSquare(GreenAccel, ref Vert1, ref Hori1, friction1, ref x1, ref y1, UpDown, DownDown, LeftDown, RightDown);
            // Constrain green square velocity to maximum speeds
            SpeedLimit(GreenMaxHori, ref Hori1, ref Vert1, GreenMaxVert);

            // Update red square: apply acceleration, friction, and boundary collision
            SpeedMathSquare(RedAccell, ref Vert2, ref Hori2, friction2, ref x2, ref y2, WDown, SDown, ADown, DDown);
            // Constrain red square velocity to maximum speeds
            SpeedLimit(RedMaxHori, ref Hori2, ref Vert2, RedMaxVert);

            // Apply calculated velocities to position updates
            x1 += Hori1;
            y1 += Vert1;

            x2 += Hori2;
            y2 += Vert2;

            // Check for idle player and trigger aggressive AI if needed
            SitStill();
            
            // Process game logic: power-ups, AI, collision detection, and rendering
            ApplySpeedBonus();
            ApplyIceEffect();
            PlayerCollision();  // Check for collision between squares
            AIMath();
            Invalidate();

            // Update life indicator label with heart characters (uses cached string)
            if (RedScore != null)
            {
                RedScore.Text = GetLivesDisplay();
            }

            // Display survival timer in ScoreGreen label (top-left)
            if (ScoreGreen != null)
            {
                ScoreGreen.Text = $"⏱ {survivalTimer.Elapsed.TotalSeconds:F1}s";
            }

            // OPTIMIZATION: Only update debug label if in debug mode
            if (debug)
            {
                debugLabel.Visible = true;
                debugLabel.Text = $"G{x1}, {y1}\nR{x2}, {y2}";
                debugLabel.Text += $"\nUp:{UpDown} Down:{DownDown} Left:{LeftDown} Right:{RightDown}";
                debugLabel.Text += $"\nx1:{x1} y1:{y1} H:{Hori1} V:{Vert1}";
                debugLabel.Text += $"\nAggressive: {redAggressive}";
            }
            else
            {
                debugLabel.Visible = false;
            }

            // Check if green has been eliminated
            if (greenEliminated)
            {
                timer.Stop();
                survivalTimer.Stop();
                MessageBox.Show($"Green Square Eliminated!\n\nSurvival Time: {survivalTimer.Elapsed.TotalSeconds:F2} seconds", "Game Over");
                Restart.Visible = true;
            }
        }

        /// <summary>
        /// Converts the current green lives count into a visual heart character display.
        /// Returns a cached string of heart characters (❤) representing remaining lives.
        /// Only rebuilds cache when lives change to avoid string allocation every frame.
        /// </summary>
        private string GetLivesDisplay()
        {
            // OPTIMIZATION: Only rebuild hearts string if lives changed
            if (greenLives != lastCachedLives)
            {
                lastCachedLives = greenLives;
                
                string hearts = "";
                for (int i = 0; i < greenLives; i++)
                {
                    hearts += "❤ ";  // Heart character with spacing
                }
                cachedHeartsDisplay = hearts.Trim();  // Remove trailing space
            }

            return cachedHeartsDisplay;
        }
        /// <summary>
        /// Enters aggressive mode: red's speed caps are severely reduced to force green to move,
        /// then boosted significantly to aggressively pursue.
        /// Phase 0: Slow (punishment for idleness) - lasts phaseTickDuration ticks
        /// Phase 1: Fast (aggressive chase) - lasts phaseTickDuration ticks
        /// </summary>
        private void EnterRedAggressiveMode()
        {
            if (redAggressive) return; // Already in aggressive mode
            
            redAggressive = true;

            // Save normal stats
            savedRedAccell = RedAccell;
            savedRedMaxHori = RedMaxHori;
            savedRedMaxVert = RedMaxVert;

            // Start with SLOW phase (punish green for being idle)
            aggressivePhase = 0;
            RedAccell = 0.05f;           // Cripple acceleration
            RedMaxHori = 1.5f;           // Reduce speed drastically (forces green to move or red will be very slow)
            RedMaxVert = 1.5f;

            redAggressiveTimer = phaseTickDuration * 2;  // Total time for both phases
        }

        private void SitStill()
        {

            // Store green's current position into history buffer (always does this)
            greenPosHistoryX[greenHistoryIndex] = x1;
            greenPosHistoryY[greenHistoryIndex] = y1;
            greenHistoryIndex++;
            if (greenHistoryIndex >= greenHistorySize)
            {
                greenHistoryIndex = 0;
                greenHistoryFilled = true;
            }

            // Check idle detection every few frames to reduce computation
            idleCheckCounter++;
            if (idleCheckCounter >= IDLE_CHECK_INTERVAL && greenHistoryFilled)
            {
                idleCheckCounter = 0;

                // FOR-LOOP: Iterate over entire history buffer to find max displacement
                float maxDisplacement = 0f;
                for (int i = 0; i < greenHistorySize; i++)
                {
                    float dx = x1 - greenPosHistoryX[i];
                    float dy = y1 - greenPosHistoryY[i];
                    float distance = (float)Math.Sqrt(dx * dx + dy * dy);
                    if (distance > maxDisplacement)
                    {
                        maxDisplacement = distance;
                    }
                }

                // If green hasn't moved more than ~20-30 pixels in 4 seconds, trigger aggressive mode
                if (maxDisplacement < 25f)
                {
                    EnterRedAggressiveMode();
                }
                else
                {
                    // If green moved sufficiently, exit aggressive mode
                    ExitRedAggressiveMode();
                }
            }

            // Handle aggressive mode: timer
            if (redAggressive)
            {
                redAggressiveTimer--;
                if (redAggressiveTimer <= 0)
                {
                    ExitRedAggressiveMode();
                }
            }
            // ===========================================================================

        }


        /// <summary>
        /// Exits aggressive mode and restores red's normal acceleration and speed caps.
        /// </summary>
        private void ExitRedAggressiveMode()
        {
            if (!redAggressive) return;
            
            redAggressive = false;

            // Restore normal stats
            RedAccell = savedRedAccell;
            RedMaxHori = savedRedMaxHori;
            RedMaxVert = savedRedMaxVert;

            aggressivePhase = 0;
            redAggressiveTimer = 0;
        }

        /// <summary>
        /// Updates aggressive mode phases: switches from SLOW (low speed cap) to FAST (high speed cap)
        /// at the midpoint to force green into action or face a sudden aggressive chase.
        /// This is called from ApplyIceEffect to integrate smoothly with power-up timing.
        /// </summary>
        private void UpdateAggressivePhase()
        {
            if (!redAggressive) return;

            // Switch phases halfway through aggressive duration
            if (redAggressiveTimer == phaseTickDuration)
            {
                // Switch to FAST phase: boost red's speed significantly to chase aggressively
                aggressivePhase = 1;
                RedAccell = savedRedAccell * 1.8f;      // Boost acceleration
                RedMaxHori = savedRedMaxHori * 2.0f;    // Double top speed (very aggressive)
                RedMaxVert = savedRedMaxVert * 2.0f;
            }
        }

        /// 
        /// Handles the Win (start) button click event.
        /// Hides the start UI, enables AI control for the red player, and hides the mouse cursor.
        /// 
        private void Win_Click(object sender, EventArgs e)
        {
            Win.Visible = false;
            AIEnabled = true;
            Win.TabStop = false;
            Restart.TabStop = false;
            survivalTimer.Restart();
            Cursor.Hide();  // Hide mouse cursor during gameplay
            tutorialLabel.Visible = false;
        }

        /// 
        /// Handles the Restart button click event.
        /// Resets all game state, scores, positions, velocities, and survival tracking before restarting.
        /// 
        private void Restart_Click(object sender, EventArgs e)
        {
            // Reset scores and UI
            gScore = 0;
            rScore = 0;
            Restart.Visible = false;
            GreenVic.Visible = false;
            RedVic.Visible = false;
            
            // Reset all velocities to zero
            Hori1 = 0;
            Vert1 = 0;
            Hori2 = 0;
            Vert2 = 0;

            // Reset survival tracking and green lives
            greenLives = 3;
            greenEliminated = false;
            survivalTimer.Restart();

            // Randomly reposition both players
            x1 = rand.Next(100, 400);
            y1 = rand.Next(100, 400);
            x2 = rand.Next(600, 1200);
            y2 = rand.Next(300, 600);
            
            Cursor.Hide();  // Hide mouse cursor during gameplay        
            timer.Start();
        }

        /// 
        /// Manages the point collectible lifecycle: visibility timeout, collision detection with both players,
        /// score updates, and respawn timing.
        /// DEPRECATED: Point collection system removed. Kept for reference.
        /// 
        /*
        private void AddPoint()
        {
            // Decrement visibility timer
            if (pointVisible)
            {
                pointTimer--;
                if (pointTimer <= 0)
                {
                    pointVisible = false;
                }
            }

            // Check collision with green square
            if (pointVisible && Math.Abs(pointX - x1) < pelletSize + 10 && Math.Abs(pointY - y1) < pelletSize + 10)
            {
                gScore++;
                pointVisible = false;
                ScoreGreen.Text = gScore.ToString();
            }

            // Check collision with red square
            if (pointVisible && Math.Abs(pointX - x2) < pelletSize + 10 && Math.Abs(pointY - y2) < pelletSize + 10)
            {
                rScore++;
                pointVisible = false;
                RedScore.Text = rScore.ToString();
            }

            // Handle respawn timing when point is not visible
            if (!pointVisible)
            {
                if (pointRespawnTimer >= 0)
                {
                    pointRespawnTimer--;
                }
                else
                {
                    // Respawn point at random location
                    PlaceObjectRandomly(ref pointX, ref pointY);
                    pointVisible = true;
                    pointTimer = pointLifetime;
                    pointRespawnTimer = pointRespawnDelay;
                }
            }

        }
        */


        /// 
        /// Manages ice power-up logic: collision detection, applying slow effect (reduced acceleration),
        /// visual feedback via label color, effect timeout, and respawn timing.
        /// Ice reduces acceleration from base values and persists for a duration.
        /// 
        private void ApplyIceEffect()
        {
            // Update aggressive phase timing
            UpdateAggressivePhase();

            // Decrement visibility timer
            if (IceVisible)
            {
                IceTimer--;
                if (IceTimer <= 0)
                {
                    IceVisible = false;
                }
            }

            // Check collision with red square and apply ice effect
            if (IceVisible && Math.Abs(IceX - x2) < IceSize + 10 && Math.Abs(IceY - y2) < IceSize + 10)
            {
                // Reduce green's acceleration
                GreenAccel = 0.1f;
                GreenMaxHori = 10f;
                GreenMaxVert = 10f;

                IceVisible = false;
                ScoreGreen.BackColor = Color.LightBlue;  // Visual feedback
                iceGreen = true;

                IceActive = true;
                playerEffectIce = 320;  // Reset effect duration
            }

            // Check collision with green square and apply ice effect
            if (IceVisible && Math.Abs(IceX - x1) < IceSize + 10 && Math.Abs(IceY - y1) < IceSize + 10)
            {
                // Reduce red's acceleration
                RedAccell = 0.1f;
                RedMaxHori = 10f;
                RedMaxVert = 10f;

                IceVisible = false;
                RedScore.BackColor = Color.LightBlue;  // Visual feedback
                iceGreen = false;

                IceActive = true;
                playerEffectIce = 320;  // Reset effect duration
            }

            // Decrement effect timer and reset to normal when expired
            if (playerEffectIce > 0 && IceActive)
            {
                playerEffectIce--;
            }
            else
            {
                // Restore normal acceleration values
                GreenAccel = 0.2f;
                GreenMaxHori = 10f;
                GreenMaxVert = 10f;

                RedAccell = 0.22f;
                RedMaxHori = 11f;
                RedMaxVert = 11f;

                IceActive = false;
                playerEffectIce = 320;

                // Clear visual feedback
                ScoreGreen.BackColor = Color.Black;
                RedScore.BackColor = Color.Black;
            }

            // Handle respawn timing when ice power-up is not visible
            if (!IceVisible)
            {
                if (IceRespawnTimer >= 0)
                {
                    IceRespawnTimer--;
                }
                else
                {
                    // Respawn ice power-up at random location
                    PlaceObjectRandomly(ref IceX, ref IceY);
                    IceVisible = true;
                    IceTimer = IceLifetime;
                    IceRespawnTimer = IceRespawnDelay;
                }
            }
        }

        /// 
        /// Keyboard press handler for runtime tuning of AI prediction parameter.
        /// Press 'w' to increase prediction time (makes AI look further ahead).
        /// Press 's' to decrease prediction time (makes AI less predictive).
        /// 
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'w':
                    predictionTime += 0.5f;
                    break;
                case 's':
                    predictionTime -= 0.5f;
                    break;
            }
        }

        /// 
        /// Manages speed power-up logic: collision detection, applying acceleration/speed boost,
        /// visual feedback via label color, effect timeout, and respawn timing.
        /// Speed boosts increase acceleration and maximum velocity temporarily.
        /// 
        private void ApplySpeedBonus()
        {
            // Decrement visibility timer
            if (speedVisible)
            {
                speedTimer--;
                if (speedTimer <= 0)
                {
                    speedVisible = false;
                }
            }

            // Check collision with green square and apply speed boost
            if (speedVisible && Math.Abs(speedX - x1) < speedSize + 10 && Math.Abs(speedY - y1) < speedSize + 10)
            {
                // Increase green's acceleration and max speeds
                GreenAccel = 0.4f;
                GreenMaxHori = 13f;
                GreenMaxVert = 13f;
                speedVisible = false;
                ScoreGreen.BackColor = Color.Yellow;  // Visual feedback
                speedActive = true;
                playerEffectSpeed = 320;  // Reset effect duration
            }

            // Check collision with red square and apply speed boost
            if (speedVisible && Math.Abs(speedX - x2) < speedSize + 10 && Math.Abs(speedY - y2) < speedSize + 10)
            {
                // Increase red's acceleration and max speeds (slightly more than green)
                RedAccell = 0.42f;
                RedMaxHori = 14f;
                RedMaxVert = 14f;
                speedVisible = false;
                RedScore.BackColor = Color.Yellow;  // Visual feedback
                speedActive = true;
                playerEffectSpeed = 320;  // Reset effect duration
            }

            // Decrement effect timer and reset to normal when expired
            if (playerEffectSpeed > 0 && speedActive)
            {
                playerEffectSpeed--;
            }
            else
            {
                // Restore normal acceleration and speed values
                GreenAccel = 0.2f;
                GreenMaxHori = 10f;
                GreenMaxVert = 10f;
                RedAccell = 0.22f;
                RedMaxHori = 11f;
                RedMaxVert = 11f;
                speedActive = false;
                playerEffectSpeed = 320;

                // Clear visual feedback
                ScoreGreen.BackColor = Color.Black;
                RedScore.BackColor = Color.Black;
            }

            // Handle respawn timing when speed power-up is not visible
            if (!speedVisible)
            {
                if (speedRespawnTimer >= 0)
                {
                    speedRespawnTimer--;
                }
                else
                {
                    // Respawn speed power-up at random location
                    PlaceObjectRandomly(ref speedX, ref speedY);
                    speedVisible = true;
                    speedTimer = speedLifetime;
                    speedRespawnTimer = speedRespawnDelay;
                }
            }

        }

        /// 
        /// Calculates and applies physics for a single square: acceleration from input, friction deceleration,
        /// velocity clamping per-axis, boundary collision detection, and wall bounce with energy loss.
        /// 
        /// <name="acelerate"> acceleration applied per frame when input is active.
        /// <name="vertical"> Reference to vertical velocity (Y component) to update.
        /// <name="horizontal"> Reference to horizontal velocity (X component) to update.
        /// <name="friction"> Magnitude of friction deceleration applied when no input is active in that axis.
        /// <name="x"> Reference to X position; clamped to screen bounds and bounced.
        /// <name="y"> Reference to Y position; clamped to screen bounds and bounced.
        /// <name="Up"> Whether up input is active (decreases vertical velocity).
        /// <name="Down"> Whether down input is active (increases vertical velocity).
        /// <name="Left"> Whether left input is active (decreases horizontal velocity).
        /// <name="Right"> Whether right input is active (increases horizontal velocity).
        private void SpeedMathSquare(float acelero, ref float vertical, ref float horizontal, float friction, ref float x, ref float y, bool Up, bool Down, bool Left, bool Right)
        {
            //apply acceleration when input is active
            if (Up) vertical -= acelero;
            if (Down) vertical += acelero;
            if (Left) horizontal -= acelero;
            if (Right) horizontal += acelero;

            // Apply friction (deceleration) when no input is present in that axis
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
                // Jitter/creep fix: set to zero if very close to zero
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

                // Jitter/creep fix: set to zero if very close to zero
                if (Math.Abs(horizontal) < 0.1f)
                    horizontal = 0;
            }

            // Boundary collision: clamp position and bounce with energy loss
            if (x < 10) x = 10;
            if (x > this.ClientSize.Width) x = this.ClientSize.Width;
            if (y < 10) y = 10;
            if (y > this.ClientSize.Height) y = this.ClientSize.Height;
            // Reverse velocity and apply 20% energy loss (0.8 multiplier)
            if (x == 10 || x == this.ClientSize.Width) horizontal = -horizontal * 0.8f;
            if (y == 10 || y == this.ClientSize.Height) vertical = -vertical * 0.8f;
            return;
        }


        /// 
        /// Simple predictive AI for the red square: calculates where the green square will be
        /// at a predicted future time, then sets movement inputs (WASD flags) to move toward that position.
        /// Prediction accuracy is adjustable via w/s keys during gameplay.
        /// 
        private void AIMath()
        {
            if (!AIEnabled) return;

            // Predict green square's position at future time based on current velocity
            float predictedX = x1 + Hori1 * predictionTime;
            float predictedY = y1 + Vert1 * predictionTime;

            // Set WASD flags to move red square toward predicted position
            if (predictedX < x2)
            {
                ADown = true;
                DDown = false;
            }
            if (predictedX > x2)
            {
                DDown = true;
                ADown = false;
            }
            if (predictedY < y2)
            {
                WDown = true;
                SDown = false;
            }
            if (predictedY > y2)
            {
                SDown = true;
                WDown = false;
            }
        }

        /// 
        /// Constrains velocity to configured maximum values: per-axis clamping and radial (vector magnitude) limiting.
        /// Ensures neither component exceeds its per-axis maximum and the overall velocity vector
        /// does not exceed the radial maximum while preserving movement direction.
        /// 
        /// <name= "maxHori" > Maximum magnitude for per-axis horizontal limit (also used for radial limit).
        /// <name= "horizontal" > Reference to horizontal velocity component to constrain.
        /// <name= "vertical" > Reference to vertical velocity component to constrain.
        /// <name= "maxVert" > Maximum magnitude for per-axis vertical limit.
        private void SpeedLimit(float maxHori, ref float horizontal, ref float vertical, float maxVert)
        {
            // Simple per-axis clamping
            if (horizontal > maxHori) horizontal = maxHori;
            if (horizontal < -maxHori) horizontal = -maxHori;
            if (vertical > maxVert) vertical = maxVert;
            if (vertical < -maxVert) vertical = -maxVert;

            // Vector magnitude (radial) clamping to ensure overall velocity doesn't exceed limit
            if (Math.Sqrt(horizontal * horizontal + vertical * vertical) > maxHori)
            {
                // Calculate angle of velocity vector
                float angle = (float)Math.Atan2(vertical, horizontal);
                // Redistribute velocity components along that angle at the maximum magnitude
                horizontal = maxHori * (float)Math.Cos(angle);
                vertical = maxHori * (float)Math.Sin(angle);
            }
        }

        /// 
        /// Detects collision between the two player squares and handles green life loss.
        /// Green square loses one life per collision with red square.
        /// When green runs out of lives, the game ends and survival time is recorded.
        /// 
        private void PlayerCollision()
        {
            // Check if squares are colliding using distance-based collision detection
            float distanceX = Math.Abs(x1 - x2);
            float distanceY = Math.Abs(y1 - y2);
            float distance = (float)Math.Sqrt(distanceX * distanceX + distanceY * distanceY);

            // Collision occurs if distance is less than combined collision radius
            if (distance < playerCollisionRadius * 2 && !greenEliminated)
            {
                // Green square loses one life on collision
                greenLives--;

                // Move red square away to prevent continuous collision
                x2 += 50;
                y2 += 50;

                // Check if green has been eliminated
                if (greenLives <= 0)
                {
                    greenEliminated = true;
                    Cursor.Show();  // Show cursor on game over
                }
                else
                {
                    // Flash red color on collision for visual feedback
                    collisionFlashDuration = FLASH_DURATION;
                    RedScore.BackColor = Color.Red;
                }
            }

            // Handle collision flash effect duration
            if (collisionFlashDuration > 0)
            {
                collisionFlashDuration--;
                if (collisionFlashDuration == 0)
                {
                    RedScore.BackColor = Color.Black;  // Reset color after flash duration
                }
            }
        }

        /// 
        /// Handles key release events for arrow key input.
        /// Clears the corresponding key state flag when the key is released.
        /// 
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
            }
        }

        /// 
        /// Handles key press events for arrow key input.
        /// Sets the corresponding key state flag when the key is first pressed.
        /// 
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
            }
        }

        /// 
        /// Places an object at a random position within the client area, respecting a margin from edges.
        /// Used for initial placement and respawning of collectibles.
        /// 
        /// <name="x"> Reference to X coordinate to be set.
        /// <name="y"> Reference to Y coordinate to be set.
        private void PlaceObjectRandomly(ref float x, ref float y)
        {
            int margin = 40;
            x = rand.Next(margin, (this.ClientSize.Width - margin));
            y = rand.Next(margin, (this.ClientSize.Height - margin));
        }

        /// 
        /// Renders the game scene: collectibles and both player squares.
        /// Displays collision flash effect with red overlay when collision occurs.
        /// Survival timer is displayed via label control at the top.
        /// Called every frame via Invalidate().
        /// 
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Draw collision flash effect (red overlay)
            if (collisionFlashDuration > 0)
            {
                int flashAlpha = (int)(255 * (collisionFlashDuration / (float)FLASH_DURATION));
                Color flashColor = Color.FromArgb(flashAlpha, 255, 0, 0);
                using (SolidBrush flashBrush = new SolidBrush(flashColor))
                {
                    g.FillRectangle(flashBrush, 0, 0, this.ClientSize.Width, this.ClientSize.Height);
                }
            }

            // Draw speed power-up
            if (speedVisible) { g.FillRectangle(Brushes.Yellow, speedX - speedSize / 2, speedY - speedSize / 2, speedSize, speedSize); }
            // Draw ice power-up
            if (IceVisible) { g.FillRectangle(Brushes.Blue, IceX - IceSize / 2, IceY - IceSize / 2, IceSize, IceSize); }

            // Draw green square (sq1)
            g.TranslateTransform(x1, y1);
            if (IceActive && IceGreen)
            {
                g.FillRectangle(Brushes.Cyan, -10, -9, 20, 20);  // Ice effect color
            }
            else
            {
                g.FillRectangle(Brushes.Lime, -10, -9, 20, 20);  // Normal color
            }
            g.ResetTransform();

            // Draw red square (sq2)
            g.TranslateTransform(x2, y2);
            if (IceActive && !IceGreen)
            {
                g.FillRectangle(Brushes.MediumSlateBlue, -10, -9, 20, 20);  // Ice effect color
            }
            else
            {
                g.FillRectangle(Brushes.MediumVioletRed, -10, -9, 20, 20);  // Normal color
            }
            g.ResetTransform();
        }
    }
}