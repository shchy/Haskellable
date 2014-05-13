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
    }
}
