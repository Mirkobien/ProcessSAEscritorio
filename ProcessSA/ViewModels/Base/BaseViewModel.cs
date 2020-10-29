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

        public event EventHandler<string> ChangePageString;

        public event EventHandler<BaseViewModel> ChangePage;

        protected void OnChangePageString(string e)
        {
            EventHandler<string> eventHandler = ChangePageString;
            eventHandler?.Invoke(this, e);
        }

        protected void OnChangePage(BaseViewModel vm)
        {
            EventHandler<BaseViewModel> eventHandler = ChangePage;
            eventHandler?.Invoke(this, vm);
        }

        public virtual void OnLoaded()
        {

        }

        public virtual void Volver()
        {
            OnChangePage(this);
        }
    }
}
