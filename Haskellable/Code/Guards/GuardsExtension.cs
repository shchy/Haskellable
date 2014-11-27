using Haskellable.Code.Guards;
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
    public static class GuardsExtension
    {
        public static GuardsSeed<TValue> ToGuards<TValue>(
            this TValue @this)
        {
            return
                new GuardsSeed<TValue>(@this);
        }

        public static Guards<TValue,TReturn> When<TValue,TReturn>(
            this GuardsSeed<TValue> @this
            , TValue whenValue
            , Func<TValue,TReturn> selector)
        {
            return
                @this.When(v => v.Equals(whenValue), selector);
        }

        public static Guards<TValue, TReturn> When<TValue, TReturn>(
        this Guards<TValue, TReturn> @this
        , TValue whenValue
        , Func<TValue, TReturn> selector)
        {
            return
                @this.When(v => v.Equals(whenValue), selector);
        }

        public static Guards<TValue, TReturn> When<TValue, TReturn>(
          this GuardsSeed<TValue> @this
          , TValue whenValue
          , TReturn returnValue)
        {
            return
                @this.When(v => v.Equals(whenValue), _ => returnValue);
        }

        public static Guards<TValue, TReturn> When<TValue, TReturn>(
            this Guards<TValue, TReturn> @this
            , TValue whenValue
          , TReturn returnValue)
        {
            return
                @this.When(v => v.Equals(whenValue), _ => returnValue);
        }

        public static TReturn Return<TValue, TReturn>(
            this Guards<TValue, TReturn> @this
            , TReturn returnValue)
        {
            return
                @this.Return(_ => returnValue);
        }
            
    }
}


