using System;
using System.Windows.Forms;
using LinguaSeries.Forms;

namespace LinguaSeries
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var splash = new SplashForm())
            {
                splash.ShowDialog();
            }

            Application.Run(new WelcomeForm());
        }
    }
}
