using ProcessSA.Models;
using ProcessSA.ViewModels.AdminViewModels;
using ProcessSA.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProcessSA.ViewModels
{
    class MainAdminViewModel : BaseVMListHolder
    {
        public MainAdminViewModel()
        {
            BaseViewModels.Add(new GestionUsuariosViewModel());
            BaseViewModels.Add(new ConfiguracionViewModel());
            BaseViewModels.Add(new ParametrizacionViewModel());

            CurrentViewModel = BaseViewModels[0];

            Name = "MainAdmin";
            DisplayName = "Administrador";
        }
    }
}
