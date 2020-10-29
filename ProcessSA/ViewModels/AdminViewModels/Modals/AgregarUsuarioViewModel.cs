using ProcessSA.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProcessSA.ViewModels.AdminViewModels.Modals
{
    class AgregarUsuarioViewModel : BaseViewModel
    {
        public AgregarUsuarioViewModel(BaseViewModel previousVM)
        {
            PreviousViewModel = previousVM;
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

        public BaseViewModel PreviousViewModel { get; set; }

        public override void Volver()
        {
            PreviousViewModel.Volver();
        }
    }
}
