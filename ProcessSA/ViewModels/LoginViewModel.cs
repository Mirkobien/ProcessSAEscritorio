using ProcessSA.Helpers;
using ProcessSA.Models;
using ProcessSA.ViewModels.Base;
using ProcessSA.ViewModels.Interface;
using ProcessSA.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProcessSA.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        public const string ERR_MESSAGE_REST = "Ocurrió un error al conectarse con el Servicio Web. Verifique con su superior.";
        public const string ERR_MESSAGE_INVALID_CREDENTIALS = "Las credenciales son incorrectas. Si está seguro de ellas, contacte con su administrador.";
        public const string ERR_MESSAGE_UNKNOWN = "Ocurrió un error desconocido.";
        public const string ERR_NONEXISTEN_ROLE = "Su Rol asociado no existe en la aplicación.";

        private User user;

        private string _username;
        private string _password;

        private bool _loading;

        private bool _hasError;
        private string _errorMessage;

        public LoginViewModel()
        {

            Name = "Login";
            HasError = false;
        }

        public User User { get => user; set => user = value; }

        private ICommand login;

        public ICommand Login
        {
            get
            {
                if (login == null)
                {
                    login = new RelayCommand(
                        u => LoginUser(u),
                        u => true
                        );
                }
                return login;
            } set
            {
                login = value;
            }
        }

        private async void LoginUser(object passwordBox)
        {
            //Logica de Login
            //Dependiendo del tipo de Usuario, cambiar a la página respectiva
            //En este caso siempre es admin
            user = null;
            try
            {
                HasError = false;
                Loading = true;
                user = await RESTClient.LoginSistema(Username, (passwordBox as PasswordBox).Password);
            } catch (HttpRequestException ex)
            {
                HasError = true;
                ErrorMessage = ERR_MESSAGE_REST;
                ExceptionHandler.LogException(ex);
                return;
            } catch (Exception ex)
            {
                HasError = true;
                ErrorMessage = ERR_MESSAGE_UNKNOWN;
                ExceptionHandler.LogException(ex);
                return;
            }
            finally
            {
                Loading = false;
            }

            if (user == null || user.TipoUsuario == UserType.USUARIO_CLIENTE)
            {
                
                HasError = true;
                ErrorMessage = ERR_MESSAGE_INVALID_CREDENTIALS;
                return;
                
            }

            switch (user.Rol.Nombre)
            {
                case "Administrador":
                    OnChangePageString("MainAdmin");
                    break;
                case "Diseñador":
                    OnChangePageString("MainDisenador");
                    break;
                case "Analista":
                    OnChangePageString("MainAnalista");
                    break;
                case "Ejecutivo":
                    OnChangePageString("MainEjecutivo");
                    break;
                default:
                    HasError = true;
                    ErrorMessage = ERR_NONEXISTEN_ROLE;
                    break;
            }

        }

        public string Username { get; set; }
        public string Password { get; set; }

        public bool HasError { get => _hasError; set { _hasError = value; OnPropertyChanged("HasError"); } }
        public string ErrorMessage { get => _errorMessage; set { _errorMessage = value; OnPropertyChanged("ErrorMessage"); } }
        public bool Loading { get => _loading; set { _loading = value; OnPropertyChanged("Loading"); } }
    }
}
