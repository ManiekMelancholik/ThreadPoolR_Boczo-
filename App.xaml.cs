using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ThreadPoolR_Boczoń
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void AppInitialization(object o, StartupEventArgs e)
        {
            MainWindowModelView mv = new MainWindowModelView(true);
        }
    }
}
