using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weerwolven.Models
{
    public class Role
    {
        public string Name { get; set; }
        public string ImageSource { get; set; }

        public Role(string name, string imageSource)
        {
            Name = name;
            ImageSource = imageSource;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
