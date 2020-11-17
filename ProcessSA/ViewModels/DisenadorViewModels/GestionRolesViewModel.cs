using MahApps.Metro.Controls;
using ProcessSA.Models;
using ProcessSA.ViewModels.Base;
using ProcessSA.ViewModels.DisenadorViewModels.Modals;
using ProcessSA.ViewModels.Interface;
using ProcessSA.ViewModels.Windows;
using ProcessSA.Views.Windows;
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
    class GestionRolesViewModel : BaseViewModel, IEmpresaHolder
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

        private Empresa empresa;
        public Empresa Empresa
        {
            get { return empresa; }
            set { empresa = value; OnPropertyChanged("Empresa"); }
        }

        private ICommand _elegirEmpresaCommand;
        public ICommand ElegirEmpresaCommand
        {
            get
            {
                if (_elegirEmpresaCommand == null)
                {
                    _elegirEmpresaCommand = new RelayCommand(id => {
                        MetroWindow metro = new SelectView();
                        SeleccionarEmpresaVM vm = new SeleccionarEmpresaVM();
                        vm.CloseAction = new Action(metro.Close);
                        metro.DataContext = vm;
                        metro.ShowDialog();
                        Empresa = vm.SelectedItem;
                    });
                }
                return _elegirEmpresaCommand;
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
                        OnChangePage(new AgregarRolViewModel(this, Empresa)),
                        p => Empresa != null
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
                        OnChangePage(new AgregarRolViewModel(this, Empresa));
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
            if (Empresa != null)
                Roles = new ObservableCollection<Rol>(await RESTClient.GetAllRoles(Empresa.Id));
        }

        public override void Volver()
        {
            base.Volver();
            ActualizarRoles();
        }
    }
}
