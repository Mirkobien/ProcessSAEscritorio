using ProcessSA.Models;
using ProcessSA.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace ProcessSA.ViewModels.EjecutivoViewModels.Modals
{
    class AgregarEmpresaViewModel : Base.BaseViewModel
    {
        private ICommand _guardarCommand;
        private ICommand _volverCommand;
        public AgregarEmpresaViewModel(EmpresasViewModel vm)
        {
            Name = "AgregarEmpresa";
            Empresa = new Empresa();

            PreviousViewModel = vm;
        }

        public EmpresasViewModel PreviousViewModel { get; set; }
        public Empresa Empresa { get; set; }
        public bool IsCompleted { get; set; }

        public ICommand GuardarCommand
        {
            get
            {
                if (_guardarCommand == null)
                {
                    _guardarCommand = new RelayCommand(p => {
                        GuardarEmpresa(Empresa);
                    });
                }

                return _guardarCommand;
            }
        }

        public ICommand VolverCommand
        {
            get
            {
                if (_volverCommand == null)
                {
                    _volverCommand = new RelayCommand(p => Volver());
                }

                return _volverCommand;
            }
        }

        public async void GuardarEmpresa(Empresa emp)
        {
            await RESTClient.GuardarEmpresa(emp);
            IsCompleted = true;
            Volver();
        }

        public void Volver()
        {
            PreviousViewModel.Volver();
        }
    }
}
