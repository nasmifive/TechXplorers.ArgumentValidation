using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TechXplorers.ArgumentValidation
{
	internal static class GeneralExtensions
	{
		[DebuggerStepThrough] //Can get rid of this when upgraded to c# 6.0. It has Null Reference opertaor as part of the language
		internal static TReturn IfNotNull<TIn, TReturn>(this TIn obj, Func<TIn, TReturn> then, TReturn @else = default(TReturn))
			where TIn : class
			=> obj != null ? then(obj) : @else;

		[DebuggerStepThrough] //Can get rid of this when upgraded to c# 6.0. It has Null Reference opertaor as part of the language
		internal static TReturn IfNotNull<TIn, TReturn>(this TIn? value, Func<TIn?, TReturn> then, TReturn @else = default(TReturn))
			where TIn : struct
			=> value.HasValue ? then(value) : @else;

		internal static bool In<T>(this T value, IEqualityComparer<T> comparer, params T[] list) 
			=> list?.Contains(value, comparer) ?? false;

		internal static bool In<T>(this T value, params T[] list)
			=> value.In(EqualityComparer<T>.Default, list);

		internal static bool NotIn<T>(this T value, IEqualityComparer<T> comparer, params T[] list)
			=> !value.In(comparer, list);

		internal static bool NotIn<T>(this T value, params T[] list)
			=> !value.In(list);

		internal static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
			=> collection == null || collection.Any() == false;

		//[DebuggerStepThrough]
		//public static long? TryParseInt64(this string s, long? @default = null, NumberStyles style = NumberStyles.Any, IFormatProvider provider = null)
		//{
		//    long value;
		//    if (Int64.TryParse(s, style, provider, out value))
		//        return value;

		//    return @default;
		//}

		//[DebuggerStepThrough]
		//public static long TryParseInt64(this string s, long @default, NumberStyles style = NumberStyles.Any, IFormatProvider provider = null)
		//    => s.TryParseInt64(style: style, provider: provider) ?? @default;
		
		//[DebuggerStepThrough]
		//public static int? TryParseInt32(this string s, int? @default = null, NumberStyles style = NumberStyles.Any, IFormatProvider provider = null)
		//{
		//    int value;
		//    if (Int32.TryParse(s, style, provider, out value))
		//        return value;

		//    return @default;
		//}

		//[DebuggerStepThrough]
		//public static int TryParseInt32(this string s, int @default, NumberStyles style = NumberStyles.Any, IFormatProvider provider = null)
		//    => s.TryParseInt32(style: style, provider: provider) ?? @default;

		//[DebuggerStepThrough]
		//public static short? TryParseInt16(this string s, short? @default = null, NumberStyles style = NumberStyles.Any, IFormatProvider provider = null)
		//{
		//    short value;
		//    if (Int16.TryParse(s, style, provider, out value))
		//        return value;

		//    return @default;
		//}

		//[DebuggerStepThrough]
		//public static short TryParseInt16(this string s, short @default, NumberStyles style = NumberStyles.Any, IFormatProvider provider = null)
		//    => s.TryParseInt16(style: style, provider: provider) ?? @default;

		//[DebuggerStepThrough]
		//public static byte? TryParseByte(this string s, byte? @default = null, NumberStyles style = NumberStyles.Any, IFormatProvider provider = null)
		//{
		//    byte value;
		//    if (Byte.TryParse(s, style, provider, out value))
		//        return value;

		//    return @default;
		//}

		//[DebuggerStepThrough]
		//public static byte TryParseByte(this string s, byte @default, NumberStyles style = NumberStyles.Any, IFormatProvider provider = null)
		//    => s.TryParseByte(style: style, provider: provider) ?? @default;

		//[DebuggerStepThrough]
		//public static decimal? TryParseDecimal(this string s, decimal? @default = null, NumberStyles style = NumberStyles.Any, IFormatProvider provider = null)
		//{
		//    decimal value;
		//    if (Decimal.TryParse(s, style, provider, out value))
		//        return value;

		//    return @default;
		//}

		//[DebuggerStepThrough]
		//public static decimal TryParseDecimal(this string s, decimal @default, NumberStyles style = NumberStyles.Any, IFormatProvider provider = null)
		//    => s.TryParseDecimal(style: style, provider: provider) ?? @default;

		//[DebuggerStepThrough]
		//public static double? TryParseDouble(this string s, double? @default = null, NumberStyles style = NumberStyles.Any, IFormatProvider provider = null)
		//{
		//    double value;
		//    if (Double.TryParse(s, style, provider, out value))
		//        return value;

		//    return @default;
		//}

		//[DebuggerStepThrough]
		//public static double TryParseDouble(this string s, double @default, NumberStyles style = NumberStyles.Any, IFormatProvider provider = null)
		//    => s.TryParseDouble(style: style, provider: provider) ?? @default;

		//[DebuggerStepThrough]
		//public static bool? TryParseBool(this string s, bool? @default = null)
		//{
		//    bool value;
		//    return Boolean.TryParse(s, out value) ? value : @default;
		//}

		//[DebuggerStepThrough]
		//public static bool TryParseBool(this string s, bool @default)
		//    => s.TryParseBool() ?? @default;

		//[DebuggerStepThrough]
		//public static DateTime? TryParseDateTime(this string s, DateTime? @default = null, DateTimeStyles style = DateTimeStyles.None, IFormatProvider provider = null)
		//{
		//    DateTime value;
		//    if (DateTime.TryParse(s, provider, style, out value))
		//        return value;

		//    return @default;
		//}

		//[DebuggerStepThrough]
		//public static DateTime TryParseDateTime(this string s, DateTime @default, DateTimeStyles style = DateTimeStyles.None, IFormatProvider provider = null)
		//    => s.TryParseDateTime(style: style, provider: provider) ?? @default;

		//[DebuggerStepThrough]
		//public static DateTime? TryParseDateTimeExact(this string s, string format, DateTime? @default = null, DateTimeStyles style = DateTimeStyles.None, IFormatProvider provider = null)
		//{
		//    DateTime value;
		//    if (DateTime.TryParseExact(s, format, provider, style, out value))
		//        return value;

		//    return @default;
		//}

		//[DebuggerStepThrough]
		//public static DateTime TryParseDateTimeExact(this string s, string format, DateTime @default, DateTimeStyles style = DateTimeStyles.None, IFormatProvider provider = null)
		//    => s.TryParseDateTimeExact(format: format, style: style, provider: provider) ?? @default;

		//[DebuggerStepThrough]   //TODO: Support for bit flagged enums
		//public static TEnum? TryParseEnum<TEnum>(this string s, Func<TEnum, string> @using = null, bool ignoreCase = true) where TEnum : struct
		//    => TryParseEnum(s, @using, null, ignoreCase);

		//[DebuggerStepThrough]   //TODO: Support for bit flagged enums
		//public static TEnum? TryParseEnum<TEnum>(this string s, Func<TEnum, string> @using, Func<TEnum?, bool> onMoreThanOneFound, bool ignoreCase = true) where TEnum : struct
		//{
		//    if (!typeof(TEnum).IsEnum)
		//        throw new InvalidOperationException("Type parameter TEnum must be an enum");

		//    if (String.IsNullOrWhiteSpace(s))
		//        return null;

		//    @using = @using ?? (e => e.ToString());

		//    Func<TEnum, bool> predicate =
		//        e => String.Compare(@using(e), s, ignoreCase) == 0;

		//    return
		//         Enum.GetValues(typeof(TEnum)).OfType<TEnum>()
		//        .Where(predicate)
		//        .Cast<TEnum?>()   //This is required, otherwise the default value of any enum is 0 
		//        .FirstOrDefault(onMoreThanOneFound ?? (_ => true));
		//}

		internal static string SurroundWith(this string str, string with)
		{
			if (str == null || with == null)
				return str;

			return $"{with}{str}{with}";
		}

		internal static string SurroundWith(this string str, string startWith, string endWith)
		{
			if (str == null || startWith == null || endWith == null)
				return str;

			return $"{startWith}{str}{endWith}";
		}
	
		//public static string SafeSubstring(this string str, int from, int? length = null)
		//{
		//    if (from < 0) 
		//        from = 0;

		//    if (String.IsNullOrEmpty(str) || (str.Length - from <= 0)) 
		//        return "";

		//    if (length < 0) 
		//        length = 0;

		//    if (length == null || (str.Length - from) < length) 
		//        length = str.Length - from;

		//    return str.Substring(from, length.Value);
		//}

		//public static TValue GetOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
		//{
		//    if (dictionary == null || key == null || !dictionary.ContainsKey(key))
		//        return default(TValue);

		//    return dictionary[key];
		//}
		
		public static IEnumerable<T> Intersperse<T>(this IEnumerable<T> source, T with)
		{
			source.ThrowIfNull(nameof(source));

			var first = true;
			foreach (var item in source)
			{
				if (!first)
					yield return with;
				yield return item;
				first = false;
			}
		}
		
		public static T Clamp<T>(this T value, T between, T and)
			where T : IComparable
		{
			if (value == null || between == null || and == null)
				return value;

			var min = between.CompareTo(and) < 0 ? between : and;
			var max = between.CompareTo(and) > 0 ? between : and;

			if (value.CompareTo(min) < 0)
				return min;
			if (value.CompareTo(max) > 0)
				return max;

			return value;
		}
	}
}
