using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProcessSA.ViewModels.Base
{
    abstract class BaseVMListHolder : BaseViewModel
    {
        private BaseViewModel _currentViewModel;
        private List<BaseViewModel> _baseViewModels;

        private int _index;

        public BaseVMListHolder()
        {
        }

        public List<BaseViewModel> BaseViewModels
        {
            get
            {
                if (_baseViewModels == null)
                {
                    _baseViewModels = new List<BaseViewModel>();
                }

                return _baseViewModels;
            }
        }
        public BaseViewModel CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                if (_currentViewModel != value)
                {
                    _currentViewModel = value;
                    OnPropertyChanged("CurrentViewModel");
                }
            }
        }
        public int Index
        {
            get
            {
                return _index;
            }
            set
            {
                if (value < BaseViewModels.Count)
                {
                    CurrentViewModel = BaseViewModels[value];
                    _index = value;
                }
            }
        }
    }
}
