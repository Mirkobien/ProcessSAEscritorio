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
        public MainAnalistaViewModel()
        {
            BaseViewModels.Add(new ElegirEmpresaVM(this));
            BaseViewModels.Add(new AnalistaViewModels.GestionInformeViewModel());
            CurrentViewModel = BaseViewModels[0];

            Name = "MainAnalista";
            DisplayName = "Analista";
        }

        public Empresa Empresa { get; set; }
    }
}
