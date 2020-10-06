using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.ViewModels.Interface
{
    public interface IViewModel
    {
        string Name { get; set; }

        string DisplayName { get; set; }
    }
}
