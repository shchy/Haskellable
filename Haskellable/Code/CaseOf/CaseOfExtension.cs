using Haskellable.Code.CaseOf;
using Haskellable.Code.Monads.Maybe;
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
    public static class CaseOfExtension
    {
        public static CaseOfSeed<TValue> ToCaseOf<TValue>(this TValue @this)
        {   
            return
                new CaseOfSeed<TValue>(@this);
        }
    }
}
