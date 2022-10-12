using System;
using System.Windows.Forms;

using KeLi.Gobang.Forms;

namespace KeLi.Gobang
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
