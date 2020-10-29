using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ProcessSA.Models;
using ProcessSA.ViewModels.EjecutivoViewModels.Modals;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProcessSA.ViewModels.EjecutivoViewModels
{
    class EmpresasViewModel : Base.BaseViewModel
    {
        private ObservableCollection<Empresa> _empresas;

        private ICommand _crearEmpresaCommand;
        private ICommand _eliminarEmpresaCommand;
        private ICommand _modificarEmpresaCommand;
        public EmpresasViewModel()
        {
            Name = "EmpresasView";
            DisplayName = "Empresas";
            Icon = "Matrix";
        }

        public string Icon { get; set; }

        public ObservableCollection<Empresa> Empresas { get { return _empresas; } set
            {
                _empresas = value;
                OnPropertyChanged("Empresas");
            }
        }

        public Empresa SelectedEmpresa { get; set; }

        public ICommand CrearEmpresaCommand
        {
            get
            {
                if (_crearEmpresaCommand == null)
                {
                    _crearEmpresaCommand = new RelayCommand(p => { OnChangePage(new AgregarEmpresaViewModel(this)); });
                }

                return _crearEmpresaCommand;
            }
        }

        public ICommand EliminarEmpresaCommand
        {
            get
            {
                if(_eliminarEmpresaCommand == null)
                {
                    _eliminarEmpresaCommand = new RelayCommand(p => EliminarEmpresa(), p => { return (SelectedEmpresa != null); });
                }

                return _eliminarEmpresaCommand;
            }
        }

        public ICommand ModificarEmpresaCommand
        {
            get
            {
                if (_modificarEmpresaCommand == null)
                {
                    _modificarEmpresaCommand = new RelayCommand(p => ModificarEmpresa(), p => { return (SelectedEmpresa != null); });
                }

                return _modificarEmpresaCommand;
            }
        }

        public async void EliminarEmpresa()
        {
            if (SelectedEmpresa == null)
                return;

            MessageDialogResult result = await (Application.Current.MainWindow as MetroWindow)
                .ShowMessageAsync(
                "Eliminar empresa", 
                "Está seguro que desea eliminar la empresa?" + SelectedEmpresa.Nombre, 
                MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
                RESTClient.EliminarEmpresa(SelectedEmpresa);
                Empresas.Remove(SelectedEmpresa);
            }

        }

        public void ModificarEmpresa()
        {
            if (SelectedEmpresa == null)
                return;

            OnChangePage(new ModificarEmpresaViewModel(this, SelectedEmpresa));
        }

        public void Volver()
        {
            OnChangePage(this);
        }

        public async override void OnLoaded()
        {
            SelectedEmpresa = null;
            Empresas = new ObservableCollection<Empresa>(await RESTClient.GetAllEmpresas());
        }
    }
}
