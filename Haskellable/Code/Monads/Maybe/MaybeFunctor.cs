using Haskellable.Code.Monads.Maybe;
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
    public static class MaybeFunctor
    {
        public static IMaybe<TResult> Select<TValue, TResult>(
            this IMaybe<TValue> @this
            , Func<TValue, TResult> selector)
        {
            return @this.FMap(selector) as IMaybe<TResult>;
        }
    }
}
