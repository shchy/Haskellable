using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Haskellable.Code.Functor
{
    public interface IApplicativeFunctor<T> : IFunctor<T>
    {
        Func<IApplicativeFunctor<T2>, IApplicativeFunctor<TNew>> Apply<T2, TNew>(Func<T, T2, TNew> selector);
    }
}
