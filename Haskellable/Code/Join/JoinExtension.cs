using Haskellable.Code.Join;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class JoinExtension
    {
        public static JoinContext<TValue,TReturn> ToJoinable<TValue,TReturn>(
            this Func<TValue,TReturn> @this)
        {
            return
                new JoinContext<TValue, TReturn>(@this);
        }
    }
}
