using System;

namespace TechXplorers.ArgumentValidation
{
    public interface IValidationDef<out T>
    {
        IValidationDef<T> IsNot(Func<T, bool> predicate, string message = null);
        IValidationDef<T> IsNot<TAny>(Func<T, bool> predicate, string message, params TAny[] validValues);
        IValidationDef<T> Is(Func<T, bool> predicate, string message = null);
        IValidationDef<T> Is<TAny>(Func<T, bool> predicate, string message, params TAny[] validValues);
        IValidationDef<T> When(Func<T, bool> ifTrue);
        IValidationDef<T> WhenNot(Func<T, bool> ifTrue);
        IValidationDef<T> When(bool ifTrue);
    }
}
