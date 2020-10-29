using ProcessSA.Models;
using ProcessSA.ViewModels.Base;
using ProcessSA.ViewModels.DisenadorViewModels.Modals;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProcessSA.ViewModels.DisenadorViewModels
{
    class GestionRolesViewModel : BaseViewModel
    {
        public GestionRolesViewModel()
        {
            Name = "GestionRoles";
            DisplayName = "Roles";

            Icon = "Group";

            
        }

        private ObservableCollection<Rol> _roles;
        public ObservableCollection<Rol> Roles { get => _roles; set
            {
                _roles = value;
                OnPropertyChanged("Roles");
            }
        }

        public Rol SelectedRol { get; set; }
        private ICommand _crearRolCommand;
        public ICommand CrearRolCommand
        {
            get
            {
                if (_crearRolCommand == null)
                {
                    _crearRolCommand = new RelayCommand(p =>
                        OnChangePage(new AgregarRolViewModel(this))
                    );
                }

                return _crearRolCommand;
            }
        }
        private ICommand _eliminarRolCommand;
        public ICommand EliminarRolCommand
        {
            get
            {
                if (_eliminarRolCommand == null)
                {
                    _eliminarRolCommand = new RelayCommand(p =>
                    {
                        EliminarRol();
                    }, p => SelectedRol != null);
                }

                return _eliminarRolCommand;
            }
        }
        private ICommand _modificarRolCommand;
        public ICommand ModificarRolCommand
        {
            get
            {
                if (_modificarRolCommand == null)
                {
                    _modificarRolCommand = new RelayCommand(p =>
                    {
                        OnChangePage(new AgregarRolViewModel(this));
                    }, p => SelectedRol != null);
                }

                return _modificarRolCommand;
            }
        }

        public string Icon { get; set; }

        public async override void OnLoaded()
        {
            ActualizarRoles();
        }

        private async void EliminarRol()
        {
            await RESTClient.EliminarRol(SelectedRol.Id);
            ActualizarRoles();
        }

        private async void ActualizarRoles()
        {
            Roles = new ObservableCollection<Rol>(await RESTClient.GetAllRoles());
        }

        public override void Volver()
        {
            base.Volver();
            ActualizarRoles();
        }
    }
}
