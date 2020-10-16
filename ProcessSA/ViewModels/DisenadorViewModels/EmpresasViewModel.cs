using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProcessSA.ViewModels.DisenadorViewModels
{
    class EmpresasViewModel : Base.BaseViewModel
    {
        private ICommand _crearEmpresaCommand;
        public EmpresasViewModel()
        {
            Name = "EmpresasView";
            DisplayName = "Empresas";
            Icon = "Matrix";
        }

        private void DoNothing()
        {

        }

        public string Icon { get; set; }

        public ICommand CrearEmpresaCommand
        {
            get
            {
                if (_crearEmpresaCommand == null)
                {
                    _crearEmpresaCommand = new RelayCommand(p => DoNothing());
                }

                return _crearEmpresaCommand;
            }
        }
    }
}
