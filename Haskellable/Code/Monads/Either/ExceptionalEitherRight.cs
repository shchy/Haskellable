using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskellable.Code.Monads.Either
{
    public class ExceptionalEitherRight<TValue> : EitherRight<Exception, TValue>, IExceptionalEither<TValue>
    {
        public ExceptionalEitherRight(TValue value)
            : base(value)
        {

        }
    }
}
