using ProcessSA.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.ViewModels.AdminViewModels
{
    class ConfiguracionViewModel : BaseViewModel
    {
        public ConfiguracionViewModel()
        {
            DisplayName = "Configuración";
            Name = "Configuracion";

            Icon = "Cog";
        }

        public string Icon { get; set; }
    }
}
