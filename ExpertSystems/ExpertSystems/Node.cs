using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystems
{
    public class Node
    {
        public string Id { get; set; }
        public string Answer { get; set; }
        public string Question { get; set; }
        public string[] Reference { get; set; }
    }
}
