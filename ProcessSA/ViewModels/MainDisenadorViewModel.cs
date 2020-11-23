using ProcessSA.ViewModels.Base;
using ProcessSA.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcessSA.ViewModels.DisenadorViewModels;
using ProcessSA.Models;
using ProcessSA.ViewModels.Interface;

namespace ProcessSA.ViewModels
{
    class MainDisenadorViewModel : BaseVMListHolder, IEmpresaHolder
    {
        public MainDisenadorViewModel(AppViewModel app)
        {
            app.PropertyChanged += App_PropertyChanged;

            //BaseViewModels.Add(new ElegirEmpresaVM(this));
            //BaseViewModels.Add(new GestionRolesViewModel());
            //BaseViewModels.Add(new GestionUnidadesViewModel());
            BaseViewModels.Add(new GestionJerarquiasViewModel());
            BaseViewModels.Add(new GestionFlujoViewModel());
            //BaseViewModels.Add(new ModeloProcesoViewModel());
            //BaseViewModels.Add(new GestionUsuariosViewModel());
            //Hecho por Mirko

            foreach(BaseViewModel vm in BaseViewModels)
            {
                vm.ChangePage += CambiarPagina;
            }

            CurrentViewModel = BaseViewModels[0];

            Name = "MainDisenador";
            DisplayName = "Diseñador";
        }

        private void App_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Empresa")
            {
                Empresa = ((AppViewModel)sender).Empresa;
                Index = 0;
            }
        }

        private Empresa _empresa;
        public Empresa Empresa { get => _empresa; set
            {
                _empresa = value;
                foreach(IEmpresaHolder vm in BaseViewModels)
                {
                    vm.Empresa = Empresa;
                }
            }
        }

        public void CambiarPagina(object sender, BaseViewModel a)
        {
            CurrentViewModel = a;
        }
    }
}
