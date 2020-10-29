using ProcessSA.ViewModels.Base;
using ProcessSA.ViewModels.EjecutivoViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.ViewModels
{
    class MainEjecutivoViewModel : Base.BaseVMListHolder
    {
        public MainEjecutivoViewModel()
        {
            BaseViewModels.Add(new EmpresasViewModel());
            CurrentViewModel = BaseViewModels[0];

            foreach (BaseViewModel vm in BaseViewModels)
            {
                vm.ChangePage += CambiarPagina;
            }

            Name = "MainEjecutivo";
            DisplayName = "Ejecutivo";
        }

        public void CambiarPagina(object sender, BaseViewModel a)
        {
            CurrentViewModel = a;
        }
    }
}
