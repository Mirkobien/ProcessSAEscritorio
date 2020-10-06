using ProcessSA.ViewModels;
using ProcessSA.ViewModels.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ProcessSA
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    /// 
    public partial class App : Application
    {
        private AppViewModel app;
        private MainWindow main;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //Start API Client
            Models.RESTClient.InitClient();

            app = new AppViewModel();
            main = new MainWindow();
            main.DataContext = app;
            main.Show();
        }
    }
}
