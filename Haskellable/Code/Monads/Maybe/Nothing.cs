using Haskellable.Code.Functor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Haskellable.Code.Monads.Maybe
{

#if !DEBUG
        [DebuggerStepThrough]
#endif
    public class Nothing<T> : IMaybe<T>
    {
        public override string ToString()
        {
            return string.Format("Nothing {0}", typeof(T).Name);
        }

        public bool IsNothing
        {
            get { return true; }
        }

        public bool IsSomething
        {
            get { return false; }
        }

        public Functor.IFunctor<TNew> FMap<TNew>(Func<T, TNew> selector)
        {
            return new Nothing<TNew>();
        }
    }
}
