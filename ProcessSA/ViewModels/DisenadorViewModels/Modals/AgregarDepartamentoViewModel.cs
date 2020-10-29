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
        public AgregarDepartamentoViewModel(BaseViewModel previousVM, JerarquiaDepartamento j)
        {
            PreviousViewModel = previousVM;
            Jerarquia = j;
            Task.Run(async () => await OnLoaded());
            Departamento = new Departamento();
        }

        private ICommand _guardarCommand;
        private ICommand _volverCommand;
        private ICommand _anadirCommand;

        private ObservableCollection<User> _usuariosEmpresa;

        public Empresa Empresa { get; set; }
        public BaseViewModel PreviousViewModel { get; set; }
        public Departamento Departamento { get; set; }
        public JerarquiaDepartamento Jerarquia { get; set; }
        public ObservableCollection<User> UsuariosEmpresa 
        { 
            get { return _usuariosEmpresa; }
            set { _usuariosEmpresa = value; OnPropertyChanged("UsuariosEmpresa"); }
        }
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

        public ICommand AnadirCommand
        {
            get
            {
                if (_anadirCommand == null)
                {
                    _anadirCommand = new RelayCommand(p => Anadir((User)p));
                }
                return _anadirCommand;
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
            UsuariosEmpresa = new ObservableCollection<User>(await RESTClient.GetUsersDeEmpresa(1));
        }

        public async void GuardarDepartamento()
        {
            await RESTClient.GuardarDepartamento(Departamento, Jerarquia.Id);
            IsCompleted = true;
            Volver();
        }

        public void Volver()
        {
            PreviousViewModel.Volver();
        }
    }
}
