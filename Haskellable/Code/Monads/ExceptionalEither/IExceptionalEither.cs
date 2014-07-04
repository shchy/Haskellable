using Haskellable.Code.Monads.Either;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Haskellable.Code.Monads.ExceptionalEither
{
    public interface IExceptionalEither<TValue>
        : IEither<Exception, TValue>
    {
    }
}
