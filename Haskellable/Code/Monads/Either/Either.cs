using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskellable.Code.Monads.Either
{
    public interface IEither<TLeft, TRight>
    {
        bool IsLeft { get; }
        bool IsRight { get; }
    }

    public interface ILeft<T> 
    {
        T Value { get; }
    }
    public interface IRight<T>
    {
        T Value { get; }
    }

    public class Left<TLeft, TRight> : IEither<TLeft, TRight>, ILeft<TLeft>
    {
        public Left(TLeft value)
        {
            this.Value = value;
        }

        public TLeft Value { get; set; }

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
            return string.Format("Left {0}", this.Value);
        }
    }

    public class Right<TLeft, TRight> : IEither<TLeft, TRight>, IRight<TRight>
    {
        public Right(TRight value)
        {
            this.Value = value;
        }

        public TRight Value { get; set; }

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
            return string.Format("Right {0}", this.Value);
        }
    }
}
