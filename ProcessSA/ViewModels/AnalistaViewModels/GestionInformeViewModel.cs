using ProcessSA.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
