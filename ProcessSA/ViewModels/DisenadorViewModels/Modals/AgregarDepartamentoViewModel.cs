using ProcessSA.Models;
using ProcessSA.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProcessSA.ViewModels.DisenadorViewModels.Modals
{
    class AgregarDepartamentoViewModel : BaseViewModel
    {
        public AgregarDepartamentoViewModel(BaseViewModel previousVM, Departamento j, Empresa empresa)
        {
            PreviousViewModel = previousVM;
            Padre = j;
            Empresa = empresa;
            Task.Run(async () => await OnLoaded());
            Departamento = new Departamento();
        }

        private ICommand _guardarCommand;
        private ICommand _volverCommand;
        private ICommand _anadirCommand;

        public Empresa Empresa { get; set; }
        public BaseViewModel PreviousViewModel { get; set; }
        public Departamento Departamento { get; set; }
        public Departamento Padre { get; set; }
        public bool IsCompleted { get; set; }

        public ICommand GuardarCommand
        {
            get
            {
                if (_guardarCommand == null)
                {
                    _guardarCommand = new RelayCommand(p => {
                        GuardarDepartamento();
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

        public void Anadir(User p)
        {
            if (Departamento.Usuarios.Contains(p) || p == null)
                return;

            Departamento.Usuarios.Add(p);
        }

        public async Task OnLoaded()
        {

        }

        public async void GuardarDepartamento()
        {
            await RESTClient.GuardarDepartamento(Departamento, Padre.Id, Empresa.Id);
            IsCompleted = true;
            Volver();
        }

        public void Volver()
        {
            PreviousViewModel.Volver();
        }
    }
}
