using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Haskellable.Code.Monads.Either
{

#if !DEBUG
        [DebuggerStepThrough]
#endif
    public class Right<T> 
        : IRight<T>
    {
        public Right(T value)
        {
            this.Value = value;
        }

        public T Value { get; set; }

        public bool IsLeft
        {
            get { return false; }
        }

        public bool IsRight
        {
            get { return true; }
        }

        public override string ToString()
        {
            return string.Format("Right {0} {1}", typeof(T).Name, this.Value);
        }

        public IEither<TLeft, T> ToEither<TLeft>()
        {
            return new EitherRight<TLeft, T>(this.Value);
        }
    }

    public class EitherRight<TLeft, TRight>
        : Right<TRight>, IEither<TLeft, TRight>
    {
        public EitherRight(TRight value)
            : base(value)
        {
        }

        public Functor.IFunctor<TNew> FMap<TNew>(Func<TRight, TNew> selector)
        {
            return new EitherRight<TLeft, TNew>(selector(this.Value));
        }
    }
}
