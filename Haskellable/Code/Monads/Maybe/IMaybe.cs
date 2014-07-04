using Haskellable.Code.Functor;
using Haskellable.Code.Monoid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Haskellable.Code.Monads.Maybe
{
    public interface IMaybe<T> : IFunctor<T>
    {
        bool IsNothing { get; }
        bool IsSomething { get; }
    }
}
