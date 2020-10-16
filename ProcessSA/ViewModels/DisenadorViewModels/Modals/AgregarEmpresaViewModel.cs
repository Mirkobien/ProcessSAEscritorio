using ProcessSA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace ProcessSA.ViewModels.DisenadorViewModels.Modals
{
    class AgregarEmpresaViewModel : Base.BaseViewModel
    {
        private ICommand _guardarCommand;
        public AgregarEmpresaViewModel()
        {
            Name = "AgregarEmpresa";
            Empresa = new Empresa();
        }

        public Empresa Empresa;

        public ICommand GuardarCommand
        {
            get
            {
                if (_guardarCommand == null)
                {
                    _guardarCommand = new RelayCommand(p => {  });
                }

                return _guardarCommand;
            }
        }
    }
}
