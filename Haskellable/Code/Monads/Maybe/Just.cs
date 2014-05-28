using Haskellable.Code.Functor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskellable.Code.Monads.Maybe
{

#if !DEBUG
        [DebuggerStepThrough]
#endif
    public class Just<T> : IMaybe<T>
    {
        private Lazy<T> lazyValue;
        public T Value { get { return this.lazyValue.Value; } }
        public Just(Func<T> value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("just is not null");
            }
            this.lazyValue = new Lazy<T>(value);
        }

        public override string ToString()
        {
            return string.Format("Just {0} {1}", typeof(T).Name, this.Value);
        }

        public bool IsNothing
        {
            get { return false; }
        }

        public bool IsSomething
        {
            get { return true; }
        }

        public IFunctor<TNew> FMap<TNew>(Func<T, TNew> selector)
        {
            return new Just<TNew>(() => selector(this.Value));
        }
    }
}
