using ProcessSA.Models;
using ProcessSA.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProcessSA.ViewModels.DisenadorViewModels.Modals
{
    class AgregarTareaViewModel : BaseViewModel
    {
        public AgregarTareaViewModel(ModeloProcesoViewModel previousVM)
        {
            PreviousViewModel = previousVM;

            Task.Run(async () => await OnLoaded());
            Tarea = new Tarea();
        }

        private ICommand _guardarCommand;
        private ICommand _volverCommand;
        private ICommand _anadirCommand;

        private ObservableCollection<User> _usuariosEmpresa;

        public Empresa Empresa { get; set; }
        public ModeloProcesoViewModel PreviousViewModel { get; set; }
        public Tarea Tarea { get; set; }
        public List<EstadoTarea> Estados { get; set; }
        public EstadoTarea SelectedEstado { get; set; }
        public ObservableCollection<User> UsuariosEmpresa 
        { 
            get { return _usuariosEmpresa; }
            set { _usuariosEmpresa = value; OnPropertyChanged("UsuariosEmpresa"); }
        }
        public bool IsCompleted { get; set; }

        public ICommand GuardarCommand
        {
            get
            {
                if (_guardarCommand == null)
                {
                    _guardarCommand = new RelayCommand(p => {
                        GuardarTarea();
                    });
                }

                return _guardarCommand;
            }
        }

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

        public ICommand AnadirCommand
        {
            get
            {
                if (_anadirCommand == null)
                {
                    _anadirCommand = new RelayCommand(p => Anadir((User)p));
                }
                return _anadirCommand;
            }
        }

        public void Anadir(User p)
        {
            if (Tarea.Responsables.Contains(p) || p == null)
                return;

            Tarea.Responsables.Add(p);
        }

        public async Task OnLoaded()
        {
            Estados = await RESTClient.GetAllEstadoTareas();
            UsuariosEmpresa = new ObservableCollection<User>(await RESTClient.GetUsersDeEmpresa(0));
        }

        public async void GuardarTarea()
        {
            Tarea.Estado = SelectedEstado;
            await RESTClient.GuardarTarea(Tarea);
            IsCompleted = true;
            Volver();
        }

        public void Volver()
        {
            PreviousViewModel.Volver();
        }
    }
}
