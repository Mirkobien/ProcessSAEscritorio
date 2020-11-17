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
            }
        }

        public Departamento SelectedDepartamento { get; set; }
        private ObservableCollection<Departamento> _departamentos;
        public ObservableCollection<Departamento> Departamentos
        {
            get { return _departamentos; }
            set { _departamentos = value; OnPropertyChanged("Departamentos"); }
        }

        private ICommand _modificarDepartamentoCommand;
        public ICommand ModificarDepartamentoCommand
        {
            get
            {
                if (_modificarDepartamentoCommand == null)
                {
                    _modificarDepartamentoCommand = new RelayCommand(id => ModificarDepartamento((int)id), p => false);
                }
                return _modificarDepartamentoCommand;
            }
        }

        private ICommand _crearDepartamentoCommand;
        public ICommand CrearDepartamentoCommand
        {
            get
            {
                if (_crearDepartamentoCommand == null)
                {
                    _crearDepartamentoCommand = new RelayCommand(id => CrearDepartamento((Departamento)id), p => p != null);
                }
                return _crearDepartamentoCommand;
            }
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
        public string Icon { get; set; }
        public void CrearDepartamento(Departamento idPadre)
        {
            OnChangePage(new AgregarDepartamentoViewModel(this, idPadre, Empresa));
        }
        public void ModificarDepartamento(int id)
        {

        }
        public async override void OnLoaded()
        {
            Actualizar();
        }

        public async void Actualizar()
        {
            if (Empresa != null)
                Departamentos = new ObservableCollection<Departamento>(await RESTClient.GetAllDepartamentosJerarquia(Empresa.Id));
        }
    }
}
