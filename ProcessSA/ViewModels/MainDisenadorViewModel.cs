using ProcessSA.ViewModels.Base;
using ProcessSA.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcessSA.ViewModels.DisenadorViewModels;

namespace ProcessSA.ViewModels
{
    class MainDisenadorViewModel : BaseVMListHolder
    {
        public MainDisenadorViewModel()
        {
            BaseViewModels.Add(new GestionRolesViewModel());
            BaseViewModels.Add(new GestionUnidadesViewModel());
            BaseViewModels.Add(new GestionJerarquiasViewModel());
            BaseViewModels.Add(new GestionFlujoViewModel());
            BaseViewModels.Add(new ModeloProcesoViewModel());
            //BaseViewModels.Add(new GestionUsuariosViewModel());

            foreach(BaseViewModel vm in BaseViewModels)
            {
                vm.ChangePage += CambiarPagina;
            }

            CurrentViewModel = BaseViewModels[0];

            Name = "MainDisenador";
            DisplayName = "Diseñador";
        }

        public void CambiarPagina(object sender, BaseViewModel a)
        {
            CurrentViewModel = a;
        }
    }
}
