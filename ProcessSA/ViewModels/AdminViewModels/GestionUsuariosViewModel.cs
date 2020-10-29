using ProcessSA.Models;
using ProcessSA.ViewModels.AdminViewModels.Modals;
using ProcessSA.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProcessSA.ViewModels.AdminViewModels
{
    class GestionUsuariosViewModel : BaseViewModel
    {
        public GestionUsuariosViewModel()
        {
            Name = "GestionUsuarios";
            DisplayName = "Usuarios";

            Icon = "User";
        }

        public ObservableCollection<User> Users { get; set; }

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

        public async override void OnLoaded()
        {
            Users = new ObservableCollection<User>(
                new ObservableCollection<User>(
                    await RESTClient.GetUsersAsync())
                .Where(u => u.TipoUsuario == UserType.USUARIO_CLIENTE)
                .ToList());
        }

        public void CrearUsuario()
        {
            OnChangePage(new AgregarUsuarioViewModel(this));
        }
    }
}
