using System;
using System.Windows.Forms;
using IoCManager.IoC;

namespace Features_Finder {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DIManager.RegisterDependencies();
            DIManager.UsingScopedLifeStyle(() => {
                var mainForm = DIManager.Resolve<IMainForm>();

                Application.Run(mainForm as Form);
            });
        }
    }
}
