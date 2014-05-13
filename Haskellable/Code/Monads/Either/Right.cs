using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskellable.Code.Monads.Either
{
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
    }
}
