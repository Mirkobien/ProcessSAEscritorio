using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using ProcessSA.ViewModels.AnalistaViewModels.Modal;
using ProcessSA.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace ProcessSA.ViewModels.AnalistaViewModels
{
    class GestionInformeViewModel : BaseViewModel
    {
        public GestionInformeViewModel()
        {
            Name = "GestionInforme";
            DisplayName = "Gestión de Informes";

            Icon = "User";
        }

        public string Icon { get; set; }

        private ICommand reportMetaFlujoCommand;

        public ICommand ReportMetaFlujoCommand
        {
            get
            {
                if (reportMetaFlujoCommand == null)
                {
                    reportMetaFlujoCommand = new RelayCommand(p => { Reporte(); });
                }
                return reportMetaFlujoCommand;
            }
        }

        public void Reporte()
        {
            OnChangePage(new ReporteViewModel());
        }
    }
}
