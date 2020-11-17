using ProcessSA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProcessSA.ViewModels.Windows
{
    class SeleccionarEmpresaVM : ObservableObject
    {
        public SeleccionarEmpresaVM()
        {
            Task.Run(() => { Load(); });
            DisplayMemberPath = "Nombre";
        }

        public string DisplayMemberPath { get; set; }
        public Empresa SelectedItem { get; set; }

        private List<Empresa> _empresa;
        public List<Empresa> ListaSource
        {
            get { return _empresa; }
            set { _empresa = value; OnPropertyChanged("ListaSource"); }
        }
        public Action CloseAction { get; set; }

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

        public void Volver()
        {
            CloseAction();
        }
        public async void Load()
        {
            ListaSource = await RESTClient.GetAllEmpresas();
        }
    }
}
