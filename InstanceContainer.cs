using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APInject
{
    internal class InstanceContainer
    {
        public object? ObjectInstance { get; set; }
        public Type ObjectInterface { get; set; }
        public Type ObjectType { get; set; }
        public string? Description { get; set; }
    }
}
