using SMT.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SMT
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DefineControlMessagesStyle();
            Application.Run(new MainForm());
        }

        private static void DefineControlMessagesStyle()
        {
            // define style.
            ControlMessagesExtension.ErrorBackColor = Color.LightCoral;
            ControlMessagesExtension.ErrorLabelColor = Color.OrangeRed;
            ControlMessagesExtension.WarningBackColor = Color.Khaki;
            ControlMessagesExtension.WarningLabelColor = Color.DarkOrange;
            ControlMessagesExtension.ValidBackColor = Color.LightGreen;
            ControlMessagesExtension.ValidLabelColor = Color.ForestGreen;
            ControlMessagesExtension.ClearBackColor = Color.White;
            ControlMessagesExtension.ClearLabelColor = Color.Black;
        }
    }
}
