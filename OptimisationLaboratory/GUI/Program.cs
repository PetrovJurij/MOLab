using System;
using Core;
using System.Windows.Forms;
using System.IO;

namespace GUI
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MethodsLoader loader = new MethodsLoader(Path.GetDirectoryName(Application.ExecutablePath) + "\\methods\\");
            var methods = loader.Load();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
