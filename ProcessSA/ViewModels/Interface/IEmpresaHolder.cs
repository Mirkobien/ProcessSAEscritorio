using ProcessSA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.ViewModels.Interface
{
    interface IEmpresaHolder
    {
        Empresa Empresa { get; set; }
    }
}
