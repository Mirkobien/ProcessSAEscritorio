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
            BaseViewModels.Add(new ContratosViewModel());
            CurrentViewModel = BaseViewModels[0];

            Name = "MainEjecutivo";
            DisplayName = "Ejecutivo";
        }
    }
}
