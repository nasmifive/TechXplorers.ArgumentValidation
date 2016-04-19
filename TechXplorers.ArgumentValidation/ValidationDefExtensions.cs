using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace TechXplorers.ArgumentValidation
{
    public static class ValidationDefExtensions
    {
        [DebuggerHidden, DebuggerStepThrough]   // ReSharper disable once InconsistentNaming
        public static IValidationDef<T> AndThat<_, T>(this IValidationDef<_> __, Expression<Func<T>> expr) => ValidationDef<T>.Create(expr);

        [DebuggerHidden, DebuggerStepThrough]   // ReSharper disable once InconsistentNaming
        public static IValidationDef<T> AndThat<_, T>(this IValidationDef<_> __, T value, string namedAs) => ValidationDef<T>.Create(value, namedAs);
    }
}
