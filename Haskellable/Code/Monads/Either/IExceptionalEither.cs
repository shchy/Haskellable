using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskellable.Code.Monads.Either
{
    public interface IExceptionalEither<TValue>
        : IEither<Exception, TValue>
    {
    }
}
