using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskellable.Code.Monads.Either
{
    public class ExceptionalEitherLeft<TValue>
        : EitherLeft<Exception, TValue>
        , IExceptionalEither<TValue>
    {
        public ExceptionalEitherLeft(Exception value)
            : base(value)
        {
        }
    }
}
