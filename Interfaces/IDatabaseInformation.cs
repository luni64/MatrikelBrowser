using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IDatabaseInformation
    {
        public bool IsCompatible { get; }
        public bool Exists { get; }
        public int PendingMigrations { get; }      
    };
}
