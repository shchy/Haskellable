using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Haskellable.Code.Monoid
{
    public abstract class Monoid<T> : IMonoid<Monoid<T>, T>
    {
        public Monoid()
        {
            this.Value = Empty().Value;
        }

        public Monoid(T v)
        {
            this.Value = v;
        }

        public abstract IMonoid<Monoid<T>, T> Empty();

        public abstract IMonoid<Monoid<T>, T> Append(IMonoid<Monoid<T>, T> item);

        public T Value { get; private set; }
    }
}
