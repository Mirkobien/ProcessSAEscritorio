using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ProcessSA.Models;
using ProcessSA.ViewModels.Base;
using ProcessSA.ViewModels.DisenadorViewModels.Modals;
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
    class ModeloProcesoViewModel : BaseViewModel
    {
        public ModeloProcesoViewModel()
        {
            Name = "ModeloProceso";
            Icon = "Grid";
            DisplayName = "Tareas";
        }

        public string Icon { get; set; }

        private ObservableCollection<Tarea> _tareas;

        private ICommand _crearTareaCommand;
        private ICommand _eliminarTareaCommand;
        private ICommand _modificarTareaCommand;

        public ObservableCollection<Tarea> Tareas
        {
            get { return _tareas; }
            set
            {
                _tareas = value;
                OnPropertyChanged("Tareas");
            }
        }

        public Tarea SelectedTarea { get; set; }

        public ICommand CrearTareaCommand
        {
            get
            {
                if (_crearTareaCommand == null)
                {
                    _crearTareaCommand = new RelayCommand(p => { OnChangePage(new AgregarTareaViewModel(this)); });
                }

                return _crearTareaCommand;
            }
        }

        public ICommand EliminarTareaCommand
        {
            get
            {
                if (_eliminarTareaCommand == null)
                {
                    _eliminarTareaCommand = new RelayCommand(p => EliminarTarea(), p => { return (SelectedTarea != null); });
                }

                return _eliminarTareaCommand;
            }
        }

        public ICommand ModificarTareaCommand
        {
            get
            {
                if (_modificarTareaCommand == null)
                {
                    _modificarTareaCommand = new RelayCommand(p => ModificarTarea(), p => { return (SelectedTarea != null); });
                }

                return _modificarTareaCommand;
            }
        }

        public async void EliminarTarea()
        {
            if (SelectedTarea == null)
                return;

            MessageDialogResult result = await (Application.Current.MainWindow as MetroWindow)
                .ShowMessageAsync(
                "Eliminar tarea",
                "¿Está seguro que desea eliminar la tarea? " + SelectedTarea.Descripcion,
                MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
                await RESTClient.EliminarTarea(SelectedTarea);
                Tareas.Remove(SelectedTarea);
            }

        }

        public void ModificarTarea()
        {
        }

        public void Volver()
        {
            OnChangePage(this);
        }

        public async override void OnLoaded()
        {
            SelectedTarea = null;
            Tareas = new ObservableCollection<Tarea>(await RESTClient.GetAllTareas());
        }
    }
}
