using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.Models
{
    public class Cargo : ObservableObject
    {
        public Cargo()
        {
            Usuarios = new ObservableCollection<User>();
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
