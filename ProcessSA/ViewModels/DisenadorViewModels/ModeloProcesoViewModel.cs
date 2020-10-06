using ProcessSA.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.ViewModels.DisenadorViewModels
{
    class ModeloProcesoViewModel : BaseViewModel
    {
        public ModeloProcesoViewModel()
        {
            Name = "ModeloProceso";
            Icon = "Grid";
            DisplayName = "Tareas";
        }

        public string Icon { get; set; }
    }
}
