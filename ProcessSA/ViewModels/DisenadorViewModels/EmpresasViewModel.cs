using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.ViewModels.DisenadorViewModels
{
    class EmpresasViewModel : Base.BaseViewModel
    {
        public EmpresasViewModel()
        {
            Name = "EmpresasView";
            DisplayName = "Empresas";
            Icon = "Matrix";
        }

        public string Icon { get; set; }
    }
}
