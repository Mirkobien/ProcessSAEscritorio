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
    class ModificarEmpresaViewModel : Base.BaseViewModel
    {
        private ICommand _actualizarCommand;
        private ICommand _volverCommand;
        public ModificarEmpresaViewModel(EmpresasViewModel vm, Empresa emp)
        {
            Name = "ModificarEmpresa";

            PreviousViewModel = vm;

            Empresa = emp;
        }

        public EmpresasViewModel PreviousViewModel { get; set; }
        public Empresa Empresa { get; set; }
        public bool IsCompleted { get; set; }

        public ICommand GuardarCommand
        {
            get
            {
                if (_actualizarCommand == null)
                {
                    _actualizarCommand = new RelayCommand(p => {
                        ActualizarEmpresa(Empresa);
                    });
                }

                return _actualizarCommand;
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

        public async void ActualizarEmpresa(Empresa emp)
        {
            await RESTClient.GuardarEmpresa(emp, new byte[1] { 0x1 });
            IsCompleted = true;
        }

        public override void Volver()
        {
            PreviousViewModel.Volver();
        }
    }
}
