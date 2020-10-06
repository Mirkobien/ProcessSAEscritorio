using ProcessSA.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.ViewModels.DisenadorViewModels
{
    class GestionJerarquiasViewModel : BaseViewModel
    {
        public GestionJerarquiasViewModel()
        {
            DisplayName = "Jerarquías";
            Name = "GestionJerarquias";

            Icon = "Chapters";
        }

        public string Icon { get; set; }
    }
}
