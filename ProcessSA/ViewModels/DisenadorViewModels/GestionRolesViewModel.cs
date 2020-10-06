using ProcessSA.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.ViewModels.DisenadorViewModels
{
    class GestionRolesViewModel : BaseViewModel
    {
        public GestionRolesViewModel()
        {
            Name = "GestionRoles";
            DisplayName = "Roles";

            Icon = "Group";
        }

        public string Icon { get; set; }
    }
}
