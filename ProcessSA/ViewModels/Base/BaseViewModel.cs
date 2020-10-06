using ProcessSA.Models;
using ProcessSA.ViewModels.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.ViewModels.Base
{
    abstract class BaseViewModel : ObservableObject, IViewModel
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public event EventHandler<string> ChangePage;

        protected void OnChangePage(string e)
        {
            EventHandler<string> eventHandler = ChangePage;
            eventHandler?.Invoke(this, e);
        }

        public class ChangePageEventArgs : EventArgs
        {
            public string PageName { get; set; }
        }

        public void OnLoaded()
        {

        }
    }
}
