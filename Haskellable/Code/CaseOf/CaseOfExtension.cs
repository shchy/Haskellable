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
        public static CaseOfSeed ToCaseOf<TValue>(this IMaybe<TValue> @this)
        {
            var just =
                from v in @this
                select new CaseOfSeed(v);

            return
                just
                .Return(()=>new CaseOfSeed(Maybe.Nothing<TValue>()));
                
        }

        public static CaseOfSeed ToCaseOf(this object @this)
        {   
            return
                new CaseOfSeed(@this);
        }
    }
}
