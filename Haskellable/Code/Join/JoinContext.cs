using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskellable.Code.Join
{
    public class JoinContext<TValue, TReturn>
    {
        private Func<TValue, TReturn> func;
        public JoinContext(Func<TValue,TReturn> func)
        {
            this.func = func;
        }

        public JoinContext<TValue, TNewReturn> Next<TNewReturn>(Func<TReturn,TNewReturn> next)
        {
            return
                new JoinContext<TValue, TNewReturn>(x => next(this.func(x)));
        }

        public TReturn Return(TValue value)
        {
            return this.func(value);
        }
    }
}
