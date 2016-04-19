using System.Collections.Generic;

namespace TechXplorers.ArgumentValidation
{
    public static class It 
    {
        public static bool HasValue<T>(T? value) where T : struct => value.HasValue;

        public static bool IsNotNull<T>(T value) where T : class => value != null;

        public static bool IsNull<T>(T value) where T : class => value == null;
    }

    public static class Its
    {
        public static bool DefaultValue<T>(T value)  where T : struct => EqualityComparer<T>.Default.Equals(value, default(T));
    }
}
