using Haskellable.Code.CaseOf;
using Haskellable.Code.Monads.Maybe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class CaseOfExtension
    {
        //public static CaseOfSeed<TValue> ToCaseOf<TValue>(this IMaybe<TValue> @this)
        //{
        //    var just =
        //        from v in @this
        //        select new CaseOfSeed<TValue>(v);
        //    return
        //        just
        //        .Return(()=>new CaseOfSeed<TValue>(default(TValue)));
        //}

        public static CaseOfSeed<TValue> ToCaseOf<TValue>(this TValue @this)
        {   
            return
                new CaseOfSeed<TValue>(@this);
        }
    }
}
