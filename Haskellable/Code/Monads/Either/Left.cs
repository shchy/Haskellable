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
    public class Left<T> 
        : ILeft<T>
    {
        public Left(T value)
        {
            this.Value = value;
        }

        public T Value { get; set; }

        public bool IsLeft
        {
            get { return true; }
        }

        public bool IsRight
        {
            get { return false; }
        }

        public override string ToString()
        {
            return string.Format("Left {0} {1}", typeof(T).Name, this.Value);
        }

        public IEither<T, TRight> ToEither<TRight>()
        {
            return new EitherLeft<T, TRight>(this.Value);
        }
    }

    public class EitherLeft<TLeft, TRight>
        : Left<TLeft>, IEither<TLeft, TRight>
    {
        public EitherLeft(TLeft value)
            : base(value)
        {

        }

        public Functor.IFunctor<TNew> FMap<TNew>(Func<TRight, TNew> selector)
        {
            return this.ToEither<TNew>();
        }
    }
}
