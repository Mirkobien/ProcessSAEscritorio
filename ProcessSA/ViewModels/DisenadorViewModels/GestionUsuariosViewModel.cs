using ProcessSA.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.ViewModels.DisenadorViewModels
{
    class GestionUsuariosViewModel : BaseViewModel
    {
        public GestionUsuariosViewModel()
        {
            Name = "GestionUsuarios";
            DisplayName = "Usuarios";

            Icon = "User";
        }

        public string Icon { get; set; }
    }
}
