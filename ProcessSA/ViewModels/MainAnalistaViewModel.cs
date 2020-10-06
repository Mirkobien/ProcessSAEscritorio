using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.ViewModels
{
    class MainAnalistaViewModel : Base.BaseVMListHolder
    {
        public MainAnalistaViewModel()
        {
            BaseViewModels.Add(new AnalistaViewModels.GestionInformeViewModel());
            CurrentViewModel = BaseViewModels[0];

            Name = "MainAnalista";
            DisplayName = "Analista";
        }
    }
}
