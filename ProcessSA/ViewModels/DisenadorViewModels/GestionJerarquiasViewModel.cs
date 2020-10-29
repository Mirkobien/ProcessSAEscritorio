using ProcessSA.Models;
using ProcessSA.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public string Icon { get; set; }

        public async override void OnLoaded()
        {
            Empresa = await RESTClient.GetEmpresa(1);
        }
    }
}
