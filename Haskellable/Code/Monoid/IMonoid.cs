using Haskellable.Code.Monads.Maybe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Haskellable.Code.Monoid
{
    public interface INewType<T>
    {
        T Value { get; }
    }

    public interface IMonoid<T, TValue> : INewType<TValue>
    {
        IMonoid<T, TValue> Empty();
        IMonoid<T, TValue> Append(IMonoid<T, TValue> item);
    }
}
