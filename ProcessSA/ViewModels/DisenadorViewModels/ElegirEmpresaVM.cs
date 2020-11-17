using MahApps.Metro.Controls;
using ProcessSA.Models;
using ProcessSA.ViewModels.Base;
using ProcessSA.ViewModels.Interface;
using ProcessSA.ViewModels.Windows;
using ProcessSA.Views.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProcessSA.ViewModels.DisenadorViewModels
{
    class ElegirEmpresaVM : BaseViewModel, IEmpresaHolder
    {
        public ElegirEmpresaVM(IEmpresaHolder main)
        {
            DisplayName = "Seleccionar Empresa";
            Main = main;
            Icon = "AlignCenter";
        }

        public string Icon { get; set; }

        private Empresa _empresa;
        public Empresa Empresa
        {
            get => _empresa; set
            {
                _empresa = value;
                OnPropertyChanged("Empresa");
            }
        }

        private ObservableCollection<Empresa> _empresas;

        public ObservableCollection<Empresa> Empresas
        {
            get { return _empresas; }
            set { _empresas = value;
                OnPropertyChanged("Empresas");
            }
        }
            

        public IEmpresaHolder Main { get; set; }

        private ICommand _elegirEmpresaCommand;
        public ICommand ElegirEmpresaCommand
        {
            get
            {
                if (_elegirEmpresaCommand == null)
                {
                    _elegirEmpresaCommand = new RelayCommand(emp => {
                        Empresa = (Empresa)emp;
                        Main.Empresa = Empresa;
                    });
                }
                return _elegirEmpresaCommand;
            }
        }

        public async override void OnLoaded()
        {
            Empresas = new ObservableCollection<Empresa>(await RESTClient.GetAllEmpresas());
        }
    }
}
