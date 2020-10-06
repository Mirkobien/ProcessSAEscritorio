using ProcessSA.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.ViewModels.AdminViewModels
{
    class ParametrizacionViewModel : BaseViewModel
    {
        public ParametrizacionViewModel()
        {
            Name = "Parametrizacion";
            DisplayName = "Parametrización";

            Icon = "Code";
        }

        public string Icon { get; set; }
    }
}
