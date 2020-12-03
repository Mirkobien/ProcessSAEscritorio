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

        public AgregarTareaViewModel(AgregarFlujoVM agregarFlujoVM, Empresa emp, string nombreFlujo)
        {
            PreviousViewModel = agregarFlujoVM;
            Empresa = emp;
            Task.Run(async () => await OnLoaded());
            Tarea = new Tarea();
            Cargos = agregarFlujoVM.Cargos.ToList();
            Tarea.Cargo = Cargos[0];
            FlujoNombre = nombreFlujo;
        }

        public string FlujoNombre { get; set; }

        private ICommand _guardarCommand;
        private ICommand _volverCommand;
        private ICommand _elegirRolCommand;

        private ObservableCollection<User> _usuariosEmpresa;

        public int Dias { get; set; }

        private List<Cargo> cargos;
        public List<Cargo> Cargos { get => cargos; set {
                cargos = value;
                OnPropertyChanged("Cargos");
            }
        }
        public Empresa Empresa { get; set; }
        public AgregarFlujoVM PreviousViewModel { get; set; }
        public Tarea Tarea { get; set; }
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

        public ICommand ElegirRolCommand
        {
            get
            {
                if (_elegirRolCommand == null)
                {
                    _elegirRolCommand = new RelayCommand(p => Anadir((User)p));
                }
                return _elegirRolCommand;
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
            UsuariosEmpresa = new ObservableCollection<User>(await RESTClient.GetUsersDeEmpresa(1));
        }

        public void GuardarTarea()
        {
            Tarea.Estado = new EstadoTarea { Id = 1 };
            Tarea.Termino = Tarea.Comienzo.AddDays(Dias);
            PreviousViewModel.Flujo.Tareas.Add(Tarea);
            Volver();
        }

        public override void Volver()
        {
            PreviousViewModel.Volver(this);
        }
    }
}
