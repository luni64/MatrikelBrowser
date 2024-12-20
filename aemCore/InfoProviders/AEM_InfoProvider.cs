using AEM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AEM.InfoProviders
{
    


    internal class AEM_InfoProvider : IBookInfoProvider
    {
        public AEM_InfoProvider(Book book)
        {
            Pages = [];
        }

        public List<string> Pages { get; }
        
    }

    internal class MAT_InfoProvider : IBookInfoProvider
    {
        public MAT_InfoProvider(Book book)
        {
            Pages = [];
        }

        public List<string> Pages { get; }
    }


}
