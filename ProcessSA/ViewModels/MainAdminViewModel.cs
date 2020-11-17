using ProcessSA.Models;
using ProcessSA.ViewModels.AdminViewModels;
using ProcessSA.ViewModels.Base;
using ProcessSA.ViewModels.DisenadorViewModels;
using ProcessSA.ViewModels.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProcessSA.ViewModels
{
    class MainAdminViewModel : BaseVMListHolder, IEmpresaHolder
    {
        public MainAdminViewModel()
        {
            BaseViewModels.Add(new ElegirEmpresaVM(this));
            BaseViewModels.Add(new AdminViewModels.GestionUsuariosViewModel());
            //BaseViewModels.Add(new ConfiguracionViewModel());
            //BaseViewModels.Add(new ParametrizacionViewModel());

            foreach(BaseViewModel vm in BaseViewModels)
            {
                vm.ChangePage += CambiarPagina;
            }

            CurrentViewModel = BaseViewModels[0];

            Name = "MainAdmin";
            DisplayName = "Administrador";
        }

        private Empresa _empresa;
        public Empresa Empresa
        {
            get => _empresa; set
            {
                _empresa = value;
                foreach(IEmpresaHolder vm in BaseViewModels)
                {
                    vm.Empresa = _empresa;
                }
            }
        }

        public void CambiarPagina(object sender, BaseViewModel a)
        {
            CurrentViewModel = a;
        }
    }
}
