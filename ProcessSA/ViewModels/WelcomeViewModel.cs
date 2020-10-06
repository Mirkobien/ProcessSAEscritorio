using ProcessSA.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.ViewModels
{
    class WelcomeViewModel : BaseViewModel
    {
        private User _user;
        public WelcomeViewModel()
        {

            Name = "Welcome";
        }

        public User User { get => _user; set => _user = value; }
    }
}
