using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weerwolven.Models
{
    public class Player
    {
        public string Name { get; set; }
        public Role Role { get; set; }

        public bool IsRevealed { get; set; }    
        public bool IsDead { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
