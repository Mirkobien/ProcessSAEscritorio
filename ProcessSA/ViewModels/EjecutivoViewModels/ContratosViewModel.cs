using ProcessSA.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.ViewModels.EjecutivoViewModels
{
    class ContratosViewModel : BaseViewModel
    {
        public ContratosViewModel()
        {
            Name = "Contratos";
            DisplayName = "Contratos";

            Icon = "File";
        }

        public string Icon { get; set; }
    }
}
