using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace ProcessSA.Models
{
    [JsonObject]
    [Serializable]
    public class Cargo : ObservableObject
    {
        public Cargo()
        {
        }

        public string Nombre { get; set; }
        public int Id { get; set; }
        [XmlIgnore]
        [ScriptIgnore]
        public ObservableCollection<User> Usuarios { get; set; }
        [XmlIgnore]
        [ScriptIgnore]
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
