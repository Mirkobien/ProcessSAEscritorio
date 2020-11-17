using Microsoft.Win32;
using ProcessSA.ViewModels.EjecutivoViewModels.Modals;
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

namespace ProcessSA.Views.EjecutivoViews.Modal
{
    /// <summary>
    /// Lógica de interacción para AgregarEmpresaView.xaml
    /// </summary>
    public partial class AgregarEmpresaView : UserControl
    {
        public AgregarEmpresaView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            ((AgregarEmpresaViewModel)this.DataContext).FilePath = dialog.FileName;
        }
    }
}
