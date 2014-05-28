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
    public static class JoinExtension
    {
        public static Func<TValue,TNewReturn> Join<TValue,TReturn, TNewReturn>(
            this Func<TValue,TReturn> @this
            , Func<TReturn,TNewReturn> selector)
        {
            return
                Fn.New((TValue x) => selector(@this(x)));
        }

        public static Func<T2,TReturn> Join<T1,T2,TReturn>(
            this Func<T1,T2,TReturn> @this
            , T1 t1)
        {
            return
                Fn.New((T2 t2) => @this(t1, t2));
        }


    }
}
