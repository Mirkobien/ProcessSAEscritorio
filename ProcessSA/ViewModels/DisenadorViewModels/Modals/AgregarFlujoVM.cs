using ProcessSA.Models;
using ProcessSA.ViewModels.Base;
using ProcessSA.ViewModels.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProcessSA.ViewModels.DisenadorViewModels.Modals
{
    class AgregarFlujoVM : BaseViewModel, IEmpresaHolder
    {
        public AgregarFlujoVM(BaseViewModel bas, Empresa emp, Flujo flujo)
        {
            Empresa = emp;
            PreviousViewModel = bas;
            Flujo = flujo;
            Cargos = new ObservableCollection<Cargo>();

            Flujo.Tareas = new ObservableCollection<Tarea>();

            Task.Run(() =>
            {
                OnLoaded();
            });
        }

        private ObservableCollection<Cargo> cargos;
        public ObservableCollection<Cargo> Cargos { get => cargos; set
            {
                cargos = value;
                OnPropertyChanged("Roles");
            }
        }

        public int Dias { get; set; }
        public BaseViewModel PreviousViewModel { get; set; }
        public Flujo Flujo { get; set; }

        public Tarea SelectedTarea { get; set; }

        private ICommand _volverCommand;

        public ICommand VolverCommand
        {
            get
            {
                if (_volverCommand == null)
                {
                    _volverCommand = new RelayCommand(v => Volver());
                }
                return _volverCommand;
            }
        }
        private ICommand _guardarCommand;

        public ICommand GuardarCommand
        {
            get
            {
                if (_guardarCommand == null)
                {
                    _guardarCommand = new RelayCommand(v => Agregar());
                }
                return _guardarCommand;
            }
        }
        private ICommand _eliminarTareaCommand;

        public ICommand EliminarTareaCommand
        {
            get
            {
                if (_eliminarTareaCommand == null)
                {
                    _eliminarTareaCommand = new RelayCommand(v => Eliminar());
                }
                return _eliminarTareaCommand;
            }
        }

        private ICommand _crearTareaCommand;
        public ICommand CrearTareaCommand
        {
            get
            {
                if (_crearTareaCommand == null)
                {
                    _crearTareaCommand = new RelayCommand(v => CrearTarea());
                }
                return _crearTareaCommand;
            }
        }

        public Empresa Empresa { get; set; }

        public async void Agregar()
        {
            int dias = 0;
            foreach(Tarea tar in Flujo.Tareas)
            {
                dias += (tar.Termino - tar.Comienzo).Days;
            }
            Flujo.Fin = Flujo.Inicio.AddDays(dias);
            Flujo.Estado = new EstadoFlujo { Id = 5 };
            await RESTClient.GuardarFlujo(Flujo);
            Volver();
        }
        public void CrearTarea()
        {
            OnChangePage(new AgregarTareaViewModel(this, Empresa) { Cargos = Cargos.ToList() });
        }
        public override void Volver()
        {
            PreviousViewModel.Volver();
        }

        public async override void OnLoaded()
        {
            if (Cargos.Count < 1)
                Cargos = new ObservableCollection<Cargo>(await RESTClient.GetAllDepartamentosList(Empresa.Id));
            Flujo.Cargo = Cargos[0];
        }

        internal void Volver(AgregarTareaViewModel agregarTareaViewModel)
        {
            OnChangePage(this);
        }

        public void Eliminar() {

            Flujo.Tareas.Remove(SelectedTarea);
        }
    }
}

