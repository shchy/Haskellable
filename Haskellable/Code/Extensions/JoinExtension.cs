using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace System
{

#if !DEBUG
        [DebuggerStepThrough]
#endif
    public static class JoinExtension
    {
        public static Func<TValue,TNewReturn> Join<TValue,TReturn, TNewReturn>(
            this Func<TValue,TReturn> @this
            , Func<TReturn,TNewReturn> selector)
        {
            return
                Fn.New((TValue x) => selector(@this(x)));
        }
    }
}
