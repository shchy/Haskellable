using Haskellable.Code.Functor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Haskellable.Code.Monads.Either
{
    public interface IEither<TLeft, TRight> : IFunctor<TRight>
    {
        bool IsLeft { get; }
        bool IsRight { get; }
    }
}
