using ProcessSA.Models;
using ProcessSA.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProcessSA.ViewModels.DisenadorViewModels.Modals
{
    class AgregarRolViewModel : BaseViewModel
    {
        public AgregarRolViewModel(GestionRolesViewModel vm)
        {
            DisplayName = "Agregar Rol";
            PreviousVM = vm;

            Rol = new Rol();
        }

        public Rol Rol;
        public BaseViewModel PreviousVM;

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
