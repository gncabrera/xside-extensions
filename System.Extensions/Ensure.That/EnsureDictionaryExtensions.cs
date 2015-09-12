using EnsureThat;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Extensions;

namespace EnsureThat
{
    public static class EnsureDictionaryExtensions
    {
        [DebuggerStepThrough]
        public static Param<Dictionary<T, K>> HasKey<T, K>(this Param<Dictionary<T, K>> param, T key, Throws<Dictionary<T, K>>.ExceptionFnConfig exceptionFn = null)
        {
            if (!param.Value.ContainsKey(key))
            {
                if (exceptionFn != null)
                    throw exceptionFn(Throws<Dictionary<T, K>>.Instance)(param);

                throw ExceptionFactory.CreateForParamValidation(param, MyExceptionMessages.EnsureExtensions_NotContains.Inject("Dictionary", " Key " + key));
            }

            return param;
        }
    }
}
