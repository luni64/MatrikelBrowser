using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MbCore.Interfaces
{
    internal interface IBookInfoProvider
    {
        List<string> Pages { get; }
    }
}
