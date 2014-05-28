using Haskellable.Code.Guards;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{

#if !DEBUG
        [DebuggerStepThrough]
#endif
    public static class GuardsExtension
    {
        public static GuardsSeed<TValue> ToGuards<TValue>(
            this TValue @this)
        {
            return
                new GuardsSeed<TValue>(@this);
        }
    }
}


