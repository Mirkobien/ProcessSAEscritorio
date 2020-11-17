using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ProcessSA.Models;
using ProcessSA.ViewModels.Base;
using ProcessSA.ViewModels.DisenadorViewModels.Modals;
using ProcessSA.ViewModels.Interface;
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
    class GestionFlujoViewModel : BaseViewModel, IEmpresaHolder
    {
        public GestionFlujoViewModel()
        {
            DisplayName = "Flujos";
            Name = "GestionFlujo";
            Icon = "Share";
        }

        private Flujo _selectedFlujo;
        public Flujo SelectedFlujo {
            get => _selectedFlujo; set
            {
                _selectedFlujo = value;
                OnPropertyChanged("SelectedFlujo");
            }
        }

        private ICommand _crearFlujoCommand;
        public ICommand CrearFlujoCommand {
            get
            {
                if (_crearFlujoCommand == null)
                {
                    _crearFlujoCommand = new RelayCommand(p => { AgregarFlujo(); }, p => Empresa != null);
                }
                return _crearFlujoCommand;
            }
        }
        private ICommand _eliminarFlujoCommand;
        public ICommand EliminarFlujoCommand
        {
            get
            {
                if (_eliminarFlujoCommand == null)
                {
                    _eliminarFlujoCommand = new RelayCommand(p => EliminarFlujo(), p => SelectedFlujo != null);
                }
                return _eliminarFlujoCommand;
            }
        }

        private ObservableCollection<Flujo> _flujos;
        public ObservableCollection<Flujo> Flujos {
            get => _flujos; set
            {
                _flujos = value;
                OnPropertyChanged("Flujos");
            }
        }
        public string Icon { get; set; }
        public Empresa Empresa { get; set; }

        public override void Volver()
        {
            OnChangePage(this);
        }

        public void AgregarFlujo()
        {
            AgregarFlujoVM vm = new AgregarFlujoVM(this, Empresa);
            vm.ChangePage += Vm_ChangePage;
            OnChangePage(vm);
        }

        private void Vm_ChangePage(object sender, BaseViewModel e)
        {
            OnChangePage(e);
        }

        public async void EliminarFlujo()
        {
            if (SelectedFlujo == null)
                return;

            MessageDialogResult result = await (Application.Current.MainWindow as MetroWindow)
                .ShowMessageAsync(
                "Eliminar flujo",
                "¿Está seguro que desea eliminar el flujo? ",
                MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
                await RESTClient.EliminarFlujo(SelectedFlujo.Id);
                OnLoaded();
            }
        }

        public async override void OnLoaded()
        {
            if (Empresa != null)
                Flujos = new ObservableCollection<Flujo>(await RESTClient.GetAllFlujos(Empresa.Id));
        }
    }
}
