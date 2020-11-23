using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ProcessSA.Models;
using ProcessSA.ViewModels.Base;
using ProcessSA.ViewModels.DisenadorViewModels.Modals;
using ProcessSA.ViewModels.Interface;
using ProcessSA.ViewModels.Windows;
using ProcessSA.Views.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProcessSA.ViewModels.DisenadorViewModels
{
    class GestionJerarquiasViewModel : BaseViewModel, IEmpresaHolder
    {
        public GestionJerarquiasViewModel()
        {
            DisplayName = "Organigrama";
            Name = "GestionJerarquias";

            Icon = "Chapters";
        }

        private Empresa _empresa;
        public Empresa Empresa
        {
            get => _empresa; set
            {
                _empresa = value;
                OnPropertyChanged("Empresa");
                Actualizar();
            }
        }
        public Cargo SelectedDepartamento { get; set; }
        private ObservableCollection<Cargo> _departamentos;
        public ObservableCollection<Cargo> Departamentos
        {
            get { return _departamentos; }
            set { _departamentos = value; OnPropertyChanged("Departamentos"); }
        }

        private ICommand _eliminarCargoCommand;
        public ICommand EliminarCargoCommand
        {
            get
            {
                if (_eliminarCargoCommand == null)
                {
                    _eliminarCargoCommand = new RelayCommand(cargo => EliminarCargo((Cargo)cargo), p => p != null);
                }
                return _eliminarCargoCommand;
            }
        }

        private ICommand _crearDepartamentoCommand;
        public ICommand CrearDepartamentoCommand
        {
            get
            {
                if (_crearDepartamentoCommand == null)
                {
                    _crearDepartamentoCommand = new RelayCommand(id => CrearDepartamento((Cargo)id), p => p != null);
                }
                return _crearDepartamentoCommand;
            }
        }

        private ICommand crearCargoRootCommand;

        public ICommand CrearCargoRootCommand
        {
            get 
            {
                if (crearCargoRootCommand == null)
                {
                    crearCargoRootCommand = new RelayCommand(cargo => CrearCargoRoot());
                }
                return crearCargoRootCommand;
            }
        }


        public string Icon { get; set; }
        public void CrearDepartamento(Cargo idPadre)
        {
            OnChangePage(new AgregarDepartamentoViewModel(this, idPadre, Empresa));
        }
        public void CrearCargoRoot()
        {
            Cargo cargo = new Cargo();
            cargo.Nombre = "Ninguno";
            cargo.Id = 0;
            OnChangePage(new AgregarDepartamentoViewModel(this, cargo, Empresa));
        }
        public async void EliminarCargo(Cargo cargo)
        {
            bool res = await RESTClient.EliminarCargo(cargo.Id);
            if (res)
            {
                Actualizar();
            }
        }
        public async override void OnLoaded()
        {
            Actualizar();
        }

        public async void Actualizar()
        {
            if (Empresa != null)
                Departamentos = new ObservableCollection<Cargo>(await RESTClient.GetAllDepartamentosJerarquia(Empresa.Id));
        }
    }
}
