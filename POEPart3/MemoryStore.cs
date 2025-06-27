using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POEPart3; 

namespace POEPart3
{
    public static class MemoryStore
    {
        public static MemoryManager Instance { get; } = new MemoryManager();
    }


}
