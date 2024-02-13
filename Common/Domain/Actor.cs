using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    [Serializable]
    public class Actor
    {
        public int ActorId { get; set; }
        public string Name { get; set; }
        public Gender? Gender { get; set; }
        public string RoleName { get; set; }
    }
}
