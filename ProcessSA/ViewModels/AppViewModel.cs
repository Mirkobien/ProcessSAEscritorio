using ControlzEx.Theming;
using ProcessSA.Models;
using ProcessSA.ViewModels.Base;
using ProcessSA.ViewModels.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProcessSA.ViewModels
{
    class AppViewModel : BaseViewModel, IEmpresaHolder
    {
        private ICommand _changePageCommand;
        private ICommand _changePageCommandByName;
        private ICommand _switchThemeCommand;
        private ICommand _logoutCommand;

        private BaseViewModel _currentPageViewModel;
        private List<BaseViewModel> _pageViewModels;

        private bool _loggedIn;

        private User _loggedUser;

        public AppViewModel()
        {
            LoggedIn = false;
            PageViewModels.Add(new LoginViewModel(this));
            PageViewModels.Add(new MainAdminViewModel(this));
            PageViewModels.Add(new MainDisenadorViewModel(this));
            PageViewModels.Add(new MainEjecutivoViewModel());
            PageViewModels.Add(new MainAnalistaViewModel(this));
            CurrentPageViewModel = PageViewModels[0];

            foreach (BaseViewModel vm in PageViewModels)
            {
                vm.ChangePageString += Vm_ChangePage;
            }

            Task.Run(async () => {
                Empresas = await RESTClient.GetAllEmpresas();
                Empresa = Empresas[0];
            });
        }

        #region propiedades
        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new RelayCommand(
                        p => ChangeViewModel((BaseViewModel)p),
                        p => p is BaseViewModel);
                }

                return _changePageCommand;
            }
        }

        public ICommand ChangePageCommandByName
        {
            get
            {
                if (_changePageCommandByName == null)
                {
                    _changePageCommandByName = new RelayCommand(
                        p => ChangeViewModel(p.ToString()),
                        p => true
                    );
                }

                return _changePageCommandByName;
            }
        }

        public ICommand SwitchThemeCommand
        {
            get
            {
                if (_switchThemeCommand == null)
                {
                    _switchThemeCommand = new RelayCommand(
                        p => SwitchTheme());
                }

                return _switchThemeCommand;
            }
        }

        public ICommand LogoutCommand
        {
            get
            {
                if (_logoutCommand == null)
                {
                    _logoutCommand = new RelayCommand(
                        p => ChangeViewModel("Login"));
                }
                return _logoutCommand;
            }
        }

        public List<BaseViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<BaseViewModel>();

                return _pageViewModels;
            }
        }

        public BaseViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    OnPropertyChanged("CurrentPageViewModel");
                }
            }
        }

        public User LoggedUser { get => _loggedUser; set { _loggedUser = value; OnPropertyChanged("LoggedUser"); } } 

        public bool LoggedIn { get => _loggedIn; set { _loggedIn = value; OnPropertyChanged("LoggedIn"); } }

        private Empresa _empresa;
        public Empresa Empresa { get => _empresa; set
            {
                _empresa = value;
                OnPropertyChanged("Empresa");
            }
        }
        private bool _verEmpresas;
        public bool VerEmpresas { get => _verEmpresas; set
            {
                _verEmpresas = value;
                OnPropertyChanged("VerEmpresas");
            }
        }
        private List<Empresa> _empresas;
        public List<Empresa> Empresas { get => _empresas; set
            {
                _empresas = value;
                OnPropertyChanged("Empresas");
            }
        }

        #endregion

        #region methods

        private void Vm_ChangePage(object sender, string e)
        {
            if (sender is LoginViewModel)
            {
                LoggedUser = ((LoginViewModel)sender).User;
                LoggedIn = true;
            }
            ChangeViewModel(e);
        }

        private void ChangeViewModel(BaseViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        private void ChangeViewModel(string name)
        {
            BaseViewModel vm = PageViewModels.FirstOrDefault(viewm => viewm.Name == name);
            if (vm != null)
            {
                if (vm.Name == "Login")
                {
                    LoggedIn = false;
                    LoggedUser = null;
                    VerEmpresas = false;

                    Task.Run(async () => {
                        Empresas = await RESTClient.GetAllEmpresas();
                        Empresa = Empresas[0];
                    });
                } else
                {
                    

                    VerEmpresas = vm.GetType() != typeof(MainEjecutivoViewModel);
                    LoggedIn = true;
                }

                CurrentPageViewModel = vm;
            }
        }

        private void SwitchTheme()
        {
            ThemeManager manager = ThemeManager.Current;
            manager.ChangeTheme(Application.Current, manager.GetInverseTheme(manager.DetectTheme()));
        }

        #endregion
    }
}
