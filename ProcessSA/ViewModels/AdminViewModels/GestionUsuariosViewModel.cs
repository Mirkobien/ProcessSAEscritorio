using ProcessSA.Models;
using ProcessSA.ViewModels.AdminViewModels.Modals;
using ProcessSA.ViewModels.Base;
using ProcessSA.ViewModels.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProcessSA.ViewModels.AdminViewModels
{
    class GestionUsuariosViewModel : BaseViewModel, IEmpresaHolder
    {
        public GestionUsuariosViewModel()
        {
            Name = "GestionUsuarios";
            DisplayName = "Usuarios";

            Icon = "User";
        }

        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users { get => _users; set
            {
                _users = value;
                OnPropertyChanged("Users");
            }
        }

        public string Icon { get; set; }

        private ICommand _crearUsuarioCommand;
        public ICommand CrearUsuarioCommand
        {
            get
            {
                if(_crearUsuarioCommand == null)
                {
                    _crearUsuarioCommand = new RelayCommand(p => CrearUsuario());
                }
                return _crearUsuarioCommand;
            }
        }

        public Empresa Empresa { get; set; }

        public async override void OnLoaded()
        {
            if(Empresa != null)
                Users = new ObservableCollection<User>(await RESTClient.GetUsersDeEmpresa(Empresa.Id));
        }

        public void CrearUsuario()
        {
            OnChangePage(new AgregarUsuarioViewModel(this, Empresa));
        }
    }
}
