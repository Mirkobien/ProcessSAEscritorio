using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.Models
{
    public class Cargo : ObservableObject, IDataErrorInfo
    {
        public Cargo()
        {
            Usuarios = new ObservableCollection<User>();
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                switch (columnName)
                {
                    case "Nombre":
                        if (string.IsNullOrEmpty(Nombre))
                            result = "El Nombre del Cargo no puede estar vacio.";
                        break;
                    default:
                        break;
                }

                return result;
            }
        }

        public string Error
        {
            get
            {
                return "Errorsito";
            }
        }


        public string Nombre { get; set; }
        public int Id { get; set; }
        public ObservableCollection<User> Usuarios { get; set; }
        public ObservableCollection<Cargo> Cargos { get; set; }

        public static IEnumerable<Cargo> Traverse(Cargo root)
        {
            var stack = new Stack<Cargo>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                yield return current;
                foreach (var child in current.Cargos)
                    stack.Push(child);
            }
        }
    }
}
