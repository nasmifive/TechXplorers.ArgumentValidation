using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace TechXplorers.ArgumentValidation
{
    public static class Check
    {
        [DebuggerHidden, DebuggerStepThrough]
        public static IValidationDef<T> That<T>(T value, string namedAs) => ValidationDef<T>.Create(value, namedAs);

        [DebuggerHidden, DebuggerStepThrough]
        public static IValidationDef<T> That<T>(Expression<Func<T>> expr) => ValidationDef<T>.Create(expr);
    }
}
