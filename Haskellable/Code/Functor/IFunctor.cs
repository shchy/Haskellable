using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Haskellable.Code.Functor
{
    public interface IFunctor<T>
    {
        IFunctor<TNew> FMap<TNew>(Func<T, TNew> selector);
    }
}
