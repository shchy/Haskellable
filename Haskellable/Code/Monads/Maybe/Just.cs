using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskellable.Code.Monads.Maybe
{
    public class Just<T> : IMaybe<T>
    {
        public T Value { get; private set; }
        public Just(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("just is not null");
            }
            this.Value = value;
        }

        public override string ToString()
        {
            return string.Format("Just {0}", this.Value);
        }

        public bool IsNothing
        {
            get { return false; }
        }

        public bool IsSomething
        {
            get { return true; }
        }
    }

    public static class Maybe
    {
        public static IMaybe<T> New<T>(T value)
        {
            if (value == null)
            {
                return new Nothing<T>();
            }
            return new Just<T>(value);
        }

        public static IMaybe<T> Nothing<T>()
        {
            return new Nothing<T>();
        }
    }
}
