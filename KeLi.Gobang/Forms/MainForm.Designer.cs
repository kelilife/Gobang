
using KeLi.Gobang.Controls;
using KeLi.Gobang.Models;

namespace KeLi.Gobang.Forms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.gameGobang = new GameBoard();
            this.pnlOperation = new System.Windows.Forms.Panel();
            this.btnQuit = new System.Windows.Forms.Button();
            this.btnRestart = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.pnlGame = new System.Windows.Forms.Panel();
            this.pnlOperation.SuspendLayout();
            this.pnlGame.SuspendLayout();
            this.SuspendLayout();
            // 
            // gameGobang
            // 
            this.gameGobang.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gameGobang.BackgroundImage")));
            this.gameGobang.Location = new System.Drawing.Point(8, 8);
            this.gameGobang.Name = "gameGobang";
            this.gameGobang.Size = new System.Drawing.Size(520, 520);
            this.gameGobang.TabIndex = 0;
            this.gameGobang.OnGameOver += new System.EventHandler<GameOverEventArgs>(this.GameGobang_OnGameOver);
            // 
            // pnlOperation
            // 
            this.pnlOperation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlOperation.Controls.Add(this.btnQuit);
            this.pnlOperation.Controls.Add(this.btnRestart);
            this.pnlOperation.Controls.Add(this.btnStart);
            this.pnlOperation.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlOperation.Location = new System.Drawing.Point(651, 0);
            this.pnlOperation.Name = "pnlOperation";
            this.pnlOperation.Size = new System.Drawing.Size(138, 536);
            this.pnlOperation.TabIndex = 1;
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(32, 200);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(75, 23);
            this.btnQuit.TabIndex = 2;
            this.btnQuit.Text = "Quit";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.BtnQuit_Click);
            // 
            // btnRestart
            // 
            this.btnRestart.Location = new System.Drawing.Point(32, 152);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(75, 23);
            this.btnRestart.TabIndex = 1;
            this.btnRestart.Text = "Restart";
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.BtnRestart_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(32, 104);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // pnlStatus
            // 
            this.pnlStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlStatus.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlStatus.Location = new System.Drawing.Point(0, 0);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(114, 536);
            this.pnlStatus.TabIndex = 2;
            // 
            // pnlGame
            // 
            this.pnlGame.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlGame.Controls.Add(this.gameGobang);
            this.pnlGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGame.Location = new System.Drawing.Point(114, 0);
            this.pnlGame.Name = "pnlGame";
            this.pnlGame.Size = new System.Drawing.Size(537, 536);
            this.pnlGame.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(789, 536);
            this.Controls.Add(this.pnlGame);
            this.Controls.Add(this.pnlStatus);
            this.Controls.Add(this.pnlOperation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Gobang";
            this.pnlOperation.ResumeLayout(false);
            this.pnlGame.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.GameBoard gameGobang;
        private System.Windows.Forms.Panel pnlOperation;
        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.Panel pnlGame;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Button btnStart;
    }
}

