using ProcessSA.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.ViewModels.DisenadorViewModels
{
    class GestionFlujoViewModel : BaseViewModel
    {
        public GestionFlujoViewModel()
        {
            DisplayName = "Flujos";
            Name = "GestionFlujo";
            Icon = "Share";
        }

        public string Icon { get; set; }
    }
}
