using ProcessSA.Models;
using ProcessSA.ViewModels.Base;
using ProcessSA.ViewModels.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace ProcessSA.ViewModels.AdminViewModels.Modals
{
    class AgregarUsuarioViewModel : BaseViewModel, IEmpresaHolder
    {
        public AgregarUsuarioViewModel(BaseViewModel previousVM, Empresa emp)
        {
            Empresa = emp;
            PreviousViewModel = previousVM;
            Task.Run(() =>
            {
                OnLoaded();
            });

            Usuario = new User();
        }

        public string Username { get; set; }
        public string Password { get; set; }

        private ICommand _volverCommand;
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
        private ICommand _guardarCommand;
        public ICommand GuardarCommand
        {
            get
            {
                if (_guardarCommand == null)
                {
                    _guardarCommand = new RelayCommand(p => Guardar());
                }
                return _guardarCommand;
            }
        }

        private ObservableCollection<Rol> _roles;
        public ObservableCollection<Rol> Roles
        {
            get => _roles; set
            {
                _roles = value;
                OnPropertyChanged("Roles");
            }
        }

        private ObservableCollection<Sexo> _sexos;
        public ObservableCollection<Sexo> Sexos
        {
            get => _sexos; set
            {
                _sexos = value;
                OnPropertyChanged("Sexos");
            }
        }

        private User _usuario;
        public User Usuario { get => _usuario; set
            {
                _usuario = value;
                OnPropertyChanged("Usuario");
            }
        }
        public BaseViewModel PreviousViewModel { get; set; }
        public Empresa Empresa { get; set; }

        public async override void OnLoaded()
        {
            Sexos = new ObservableCollection<Sexo>(await RESTClient.GetAllSexos());
            Roles = new ObservableCollection<Rol>(await RESTClient.GetAllRoles(Empresa.Id));
            Usuario.Sexo = Sexos[0];
            Usuario.Rol = Roles[0];
        }

        public override void Volver()
        {
            PreviousViewModel.Volver();
        }

        public async void Guardar()
        {
            Usuario.Estado = new EstadoUsuario()
            {
                Id = 1
            };
            if (await RESTClient.GuardarUser(Usuario, Username, Password))
            {
                Volver();
            }
        }
    }
}
