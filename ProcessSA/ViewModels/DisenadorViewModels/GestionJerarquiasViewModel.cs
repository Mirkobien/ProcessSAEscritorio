using MahApps.Metro.Controls;
using ProcessSA.Models;
using ProcessSA.ViewModels.Base;
using ProcessSA.ViewModels.DisenadorViewModels.Modals;
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
    class GestionJerarquiasViewModel : BaseViewModel
    {
        public GestionJerarquiasViewModel()
        {
            DisplayName = "Organigrama";
            Name = "GestionJerarquias";

            Icon = "Chapters";
            Task.Run(() => OnLoaded());
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

        private List<Empresa> empresas;
        public List<Empresa> Empresas
        {
            get { return empresas; }
            set { empresas = value; OnPropertyChanged("Empresas"); }
        }

        private ICommand _modificarDepartamentoCommand;
        public ICommand ModificarDepartamentoCommand
        {
            get
            {
                if (_modificarDepartamentoCommand == null)
                {
                    _modificarDepartamentoCommand = new RelayCommand(id => ModificarDepartamento((int)id));
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
                    _crearDepartamentoCommand = new RelayCommand(id => CrearDepartamento((JerarquiaDepartamento)id));
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
                    _elegirEmpresaCommand = new RelayCommand(id => ElegirEmpresa());
                }
                return _elegirEmpresaCommand;
            }
        }
        public string Icon { get; set; }
        public void CrearDepartamento(JerarquiaDepartamento jerarquia)
        {
            OnChangePage(new AgregarDepartamentoViewModel(this, jerarquia));
        }
        public void ModificarDepartamento(int id)
        {

        }
        public void ElegirEmpresa()
        {
            MetroWindow metro = new SelectView();
            SeleccionarEmpresaVM vm = new SeleccionarEmpresaVM();
            metro.DataContext = vm;
            metro.ShowDialog();
            Empresa = vm.SelectedItem;
        }
        public async override void OnLoaded()
        {
            Empresa = await RESTClient.GetEmpresa(1);
        }
    }
}
