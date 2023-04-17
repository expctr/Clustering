using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Кластеризация
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm mainForm = new MainForm();
            MainModel mainModel = new MainModel(mainForm);
            MainController mainController = new MainController(mainForm, mainModel);
            mainController.PrepareForm();
            mainController.AddEventHandlers();
            Application.Run(mainForm);
        }
    }
}
