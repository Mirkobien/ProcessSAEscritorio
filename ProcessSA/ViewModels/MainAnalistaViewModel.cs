using ProcessSA.Models;
using ProcessSA.ViewModels.DisenadorViewModels;
using ProcessSA.ViewModels.Interface;
using ProcessSA.Views.DisenadorViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.ViewModels
{
    class MainAnalistaViewModel : Base.BaseVMListHolder, IEmpresaHolder
    {
        public MainAnalistaViewModel(AppViewModel app)
        {
            app.PropertyChanged += App_PropertyChanged;

            BaseViewModels.Add(new AnalistaViewModels.GestionInformeViewModel());
            CurrentViewModel = BaseViewModels[0];

            Name = "MainAnalista";
            DisplayName = "Analista";
        }

        private void App_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Empresa")
            {
                Empresa = ((AppViewModel)sender).Empresa;
                Index = 0;
            }
        }

        public Empresa Empresa { get; set; }
    }
}
