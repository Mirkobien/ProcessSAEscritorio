using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Reporting.WinForms;
using ProcessSA.Models;

namespace ProcessSA.Views.AnalistaViews.Modal
{
    /// <summary>
    /// Lógica de interacción para ReporteView.xaml
    /// </summary>
    public partial class ReporteView : UserControl
    {
        public ReporteView()
        {
            InitializeComponent();
        }

        private async void _reportViewer_Load(object sender, EventArgs a)
        {/*
            List<Reporte1> lista = await RESTClient.GetReportes();
            var DataSource = new ReportDataSource()
            {
                Name = "DataSet1",
                Value = lista
            };
            _reportViewer.LocalReport.DataSources.Add(DataSource);
            string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Reportes\\Report1.rdlc";
            _reportViewer.LocalReport.ReportPath = startupPath;
            _reportViewer.Refresh();
            _reportViewer.RefreshReport();*/
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _reportViewer.Refresh();
            _reportViewer.Enabled = true;
        }
    }
}
