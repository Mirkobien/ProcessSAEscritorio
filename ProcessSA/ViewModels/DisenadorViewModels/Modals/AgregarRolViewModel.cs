using ProcessSA.Models;
using ProcessSA.ViewModels.Base;
using ProcessSA.ViewModels.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProcessSA.ViewModels.DisenadorViewModels.Modals
{
    class AgregarRolViewModel : BaseViewModel, IEmpresaHolder
    {
        public AgregarRolViewModel(GestionRolesViewModel vm, Empresa emp)
        {
            DisplayName = "Agregar Rol";
            PreviousVM = vm;
            Empresa = emp;

            Rol = new Rol();
        }

        public Empresa Empresa { get; set; }
        public Rol Rol { get; set; }
        public BaseViewModel PreviousVM { get; set; }

        private ObservableCollection<Departamento> _departamentos;
        public ObservableCollection<Departamento> Departamentos
        {
            get => _departamentos;
            set
            {
                _departamentos = value;
                OnPropertyChanged("Departamentos");
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

        public async override void OnLoaded()
        {
            Departamentos = new ObservableCollection<Departamento>(await RESTClient.GetAllDepartamentosList(Empresa.Id));
        }

        private async void Guardar()
        {
            await RESTClient.GuardarRol(Rol).ContinueWith(p => Volver());
        }

        public override void Volver()
        {
            PreviousVM.Volver();
        }
    }
}
