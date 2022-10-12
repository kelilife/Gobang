using System;
using System.Windows.Forms;

using KeLi.Gobang.Models;

namespace KeLi.Gobang.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            btnRestart.Enabled = false;
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            gameGobang.StartGame();

            btnStart.Enabled = false;
            btnRestart.Enabled = true;
            btnRestart.Focus();
        }

        private void BtnRestart_Click(object sender, EventArgs e)
        {
            gameGobang.RestartGame();
        }

        private void BtnQuit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void GameGobang_OnGameOver(object sender, GameOverEventArgs e)
        {
            MessageBox.Show($"{(e.Color == 1 ? "Black" : "White")} win!", "Game Over", MessageBoxButtons.OK);
        }
    }
}