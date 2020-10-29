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

            foreach(BaseViewModel vm in BaseViewModels)
            {
                vm.ChangePage += CambiarPagina;
            }

            CurrentViewModel = BaseViewModels[0];

            Name = "MainAdmin";
            DisplayName = "Administrador";
        }

        public void CambiarPagina(object sender, BaseViewModel a)
        {
            CurrentViewModel = a;
        }
    }
}
