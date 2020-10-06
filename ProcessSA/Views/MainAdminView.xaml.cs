using MahApps.Metro.Controls;
using ProcessSA.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProcessSA.Views
{
    /// <summary>
    /// Lógica de interacción para MainAdminView.xaml
    /// </summary>
    public partial class MainAdminView : UserControl
    {
        public MainAdminView()
        {
            InitializeComponent();
        }

        private void HamburgerMenuControl_OnItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
        {
            if (!e.IsItemOptions && this.HamburgerMenuControl.IsPaneOpen)
            {
                this.HamburgerMenuControl.IsPaneOpen = false;
            }
        }
    }
}
