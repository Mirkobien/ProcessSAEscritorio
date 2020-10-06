using ProcessSA.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.ViewModels.DisenadorViewModels
{
    class GestionUnidadesViewModel : BaseViewModel
    {
        public GestionUnidadesViewModel()
        {
            DisplayName = "Unidades";
            Name = "GestionUnidades";
            Icon = "Plus";
        }

        public string Icon { get; set; }
    }
}
