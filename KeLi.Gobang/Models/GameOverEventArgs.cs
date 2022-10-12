using System;

namespace KeLi.Gobang.Models
{
    public class GameOverEventArgs : EventArgs
    {
        public GameOverEventArgs(int color)
        {
            Color = color;
        }

        public int Color { get; set; }
    }
}