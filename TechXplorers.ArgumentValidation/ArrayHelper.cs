namespace TechXplorers.ArgumentValidation
{
    internal static class Empty
    {
        internal static T[] ArrayOf<T>() => new T[] {};
    }
}
