using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TechXplorers.ArgumentValidation
{
	public static class ValidationExtensions
	{
		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<T> IsNotNull<T>(this IValidationDef<T> validationDef) where T : class 
			=> validationDef.IsNot(
				predicate: arg => arg == null, 
				message: "Argument not valid, $name$ is null but expected to be non-null.");

		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<T> IsIn<T>(this IValidationDef<T> validationDef, params T[] values) 
			=> validationDef.Is(
				predicate: arg => arg.In(values), 
				message: "Argument not valid, $name$ is $actualValue$ but accepted values are $validValues$.", 
				validValues: values);

		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<string> Contains(this IValidationDef<string> validationDef, string value) 
			=> validationDef.Is(
				predicate: arg => arg != null && value != null && arg.Contains(value),
				message: "Argument not valid, $name$ is $actualValue$ but accepted value should contain $validValues$.",
				validValues: value);

		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<string> IsNotEmpty(this IValidationDef<string> validationDef) 
			=> validationDef.IsNot(
				predicate: string.IsNullOrEmpty,
				message: "Argument not valid, $name$ is $actualValue$ but expected to be non-empty.");

		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<string> IsNotWhitespace(this IValidationDef<string> validationDef) 
			=> validationDef.IsNot(
				predicate: string.IsNullOrWhiteSpace,
				message: "Argument not valid, $name$ is $actualValue$ but expected to be non-blank.");

		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<IEnumerable<T>> IsNotEmpty<T>(this IValidationDef<IEnumerable<T>> validationDef) 
			=> validationDef.Is(
				predicate: arg => arg != null && arg.Any(),
				message: "Argument not valid, $name$ is empty or null but expected to contain atleast one item.");

		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<IEnumerable<T>> ContainsEach<T>(this IValidationDef<IEnumerable<T>> validationDef, Func<T, bool> matching, string message = null) 
			=> validationDef.Is(
				predicate: arg => matching != null && arg != null && arg.All(matching),
				message: message ?? "Argument not valid, all of the items in $name$ are not meeting the given condition");

		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<IEnumerable<T>> ContainsAny<T>(this IValidationDef<IEnumerable<T>> validationDef, Func<T, bool> matching, string message = null) 
			=> validationDef.Is(
				predicate: arg => matching != null && arg != null && arg.Any(matching),
				message: message ?? "Argument not valid, none of the items in $name$ is meeting the given condition");

		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<int> IsGreaterThan(this IValidationDef<int> validationDef, int value)
			=> IsGreaterThanImpl(validationDef, value);

		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<int?> IsGreaterThan(this IValidationDef<int?> validationDef, int value)
			=> IsGreaterThanNullableImpl(validationDef, value);

		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<long> IsGreaterThan(this IValidationDef<long> validationDef, long value)
			=> IsGreaterThanImpl(validationDef, value);

		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<long?> IsGreaterThan(this IValidationDef<long?> validationDef, long value)
			=> IsGreaterThanNullableImpl(validationDef, value);

		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<float> IsGreaterThan(this IValidationDef<float> validationDef, float value)
			=> IsGreaterThanImpl(validationDef, value);

		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<float?> IsGreaterThan(this IValidationDef<float?> validationDef, float value)
			=> IsGreaterThanNullableImpl(validationDef, value);

		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<decimal> IsGreaterThan(this IValidationDef<decimal> validationDef, decimal value)
			=> IsGreaterThanImpl(validationDef, value);

		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<decimal?> IsGreaterThan(this IValidationDef<decimal?> validationDef, decimal value)
			=> IsGreaterThanNullableImpl(validationDef, value);

		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<double> IsGreaterThan(this IValidationDef<double> validationDef, double value)
			=> IsGreaterThanImpl(validationDef, value);

		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<double?> IsGreaterThan(this IValidationDef<double?> validationDef, double value)
			=> IsGreaterThanNullableImpl(validationDef, value);

		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<DateTime> IsGreaterThan(this IValidationDef<DateTime> validationDef, DateTime value)
			=> IsGreaterThanImpl(validationDef, value);
		
		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<DateTime?> IsGreaterThan(this IValidationDef<DateTime?> validationDef, DateTime value)
			=> IsGreaterThanNullableImpl(validationDef, value);
		
		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<int> IsLessThan(this IValidationDef<int> validationDef, int value)
			=> IsLessThanImpl(validationDef, value);

		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<int?> IsLessThan(this IValidationDef<int?> validationDef, int value)
			=> IsLessThanNullableImpl(validationDef, value);

		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<long> IsLessThan(this IValidationDef<long> validationDef, long value)
			=> IsLessThanImpl(validationDef, value);

		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<long?> IsLessThan(this IValidationDef<long?> validationDef, long value)
			=> IsLessThanNullableImpl(validationDef, value);

		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<float> IsLessThan(this IValidationDef<float> validationDef, float value)
			=> IsLessThanImpl(validationDef, value);

		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<float?> IsLessThan(this IValidationDef<float?> validationDef, float value)
			=> IsLessThanNullableImpl(validationDef, value);

		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<decimal> IsLessThan(this IValidationDef<decimal> validationDef, decimal value)
			=> IsLessThanImpl(validationDef, value);

		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<decimal?> IsLessThan(this IValidationDef<decimal?> validationDef, decimal value)
			=> IsLessThanNullableImpl(validationDef, value);

		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<double> IsLessThan(this IValidationDef<double> validationDef, double value)
			=> IsLessThanImpl(validationDef, value);

		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<double?> IsLessThan(this IValidationDef<double?> validationDef, double value)
			=> IsLessThanNullableImpl(validationDef, value);

		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<DateTime> IsLessThan(this IValidationDef<DateTime> validationDef, DateTime value)
			=> IsLessThanImpl(validationDef, value);
		
		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<DateTime?> IsLessThan(this IValidationDef<DateTime?> validationDef, DateTime value)
			=> IsLessThanNullableImpl(validationDef, value);

		[DebuggerHidden, DebuggerStepThrough]
		private static IValidationDef<T> IsGreaterThanImpl<T>(this IValidationDef<T> validationDef, T value)
			where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable 
			=> validationDef.Is(
				predicate: arg => Comparer<T>.Default.Compare(arg, value) > 0,
				message: "Argument not valid, $name$ is $actualValue$ but expected to be greater than $validValues$.", 
				validValues: value);

		[DebuggerHidden, DebuggerStepThrough]
		private static IValidationDef<T?> IsGreaterThanNullableImpl<T>(this IValidationDef<T?> validationDef, T value) 
			where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable 
			=> validationDef.Is(
				predicate: arg => arg.HasValue && Comparer<T>.Default.Compare(arg.Value, value) > 0,
				message: "Argument not valid, $name$ is $actualValue$ but expected to be greater than $validValues$.", 
				validValues: value);

		[DebuggerHidden, DebuggerStepThrough]
		private static IValidationDef<T> IsLessThanImpl<T>(this IValidationDef<T> validationDef, T value)
			where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable 
			=> validationDef.Is(
				predicate: arg => Comparer<T>.Default.Compare(arg, value) < 0,
				message: "Argument not valid, $name$ is $actualValue$ but expected to be less than $validValues$.", 
				validValues: value);

		[DebuggerHidden, DebuggerStepThrough]
		private static IValidationDef<T?> IsLessThanNullableImpl<T>(this IValidationDef<T?> validationDef, T value)
			where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable 
			=> validationDef.Is(
				predicate: arg => arg.HasValue && Comparer<T>.Default.Compare(arg.Value, value) < 0,
				message: "Argument not valid, $name$ is $actualValue$ but expected to be less than $validValues$.", 
				validValues: value);

		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<T> IsNotEqualTo<T>(this IValidationDef<T> validationDef, T value) 
			=> validationDef.IsNot(
				predicate:arg => EqualityComparer<T>.Default.Equals(arg, value),
				message: "Argument not valid, $name$ is not expected to be equal to $actualValue$.", 
				validValues: value);

		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<T?> IsNotEqualTo<T>(this IValidationDef<T?> validationDef, T value) where T : struct 
			=> validationDef.IsNot(
				predicate:arg => arg.HasValue && EqualityComparer<T>.Default.Equals(arg.Value, value),
				message: "Argument not valid, $name$ is not expected to be equal to $actualValue$.", 
				validValues: value);

		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<T> IsEqualTo<T>(this IValidationDef<T> validationDef, T value) 
			=> validationDef.Is(
				predicate:arg => EqualityComparer<T>.Default.Equals(arg, value),
				message: "Argument not valid, $name$ is $actualValue$ but expected to be equal to $validValues$.", 
				validValues: value);

		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<T?> IsEqualTo<T>(this IValidationDef<T?> validationDef, T value) where T : struct 
			=> validationDef.Is(
				predicate:arg => arg.HasValue && EqualityComparer<T>.Default.Equals(arg.Value, value),
				message: "Argument not valid, $name$ is $actualValue$ but expected to be equal to $validValues$.", 
				validValues: value);

		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<T> IsValidEnum<T>(this IValidationDef<T> validationDef)
			where T : struct
		{
			if (typeof (T).IsEnum == false)
				return validationDef.Is(
					predicate: _ => false,
					message: $"Argument not valid, $name$ is expected to be of an enum type. {typeof(T).Name} is not an enum");

			return validationDef.Is(
				predicate: arg => typeof(T).IsEnumDefined(arg),
				message: "Argument not valid, $name$ is $actualValue$ but expected to be one of $validValues$.", 
				validValues: typeof(T).GetEnumNames());
		}

		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<T?> IsValidEnum<T>(this IValidationDef<T?> validationDef)
			where T : struct
		{
			if (typeof (T).IsEnum == false)
				return validationDef.Is(
					predicate: _ => false,
					message: $"Argument not valid, $name$ is expected to be of an enum type. {typeof(T).Name} is not an enum");

			return validationDef.Is(
				predicate: arg => arg == null || typeof(T).IsEnumDefined(arg.Value),
				message: "Argument not valid, $name$ is $actualValue$ but expected to be one of $validValues$.", 
				validValues: typeof(T).GetEnumNames());
		}
	}
}
