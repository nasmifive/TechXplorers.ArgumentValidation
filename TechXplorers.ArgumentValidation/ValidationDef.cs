using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using static TechXplorers.ArgumentValidation.ExpressionHelper;

namespace TechXplorers.ArgumentValidation
{
	internal sealed class ValidationDef<T> : IValidationDef<T>
	{
		private ValidationDef(){}

		private T Value { get; set; }
		private Func<string> GetName { get; set; }
		private bool ContinueValidate { get; set; } = true;

		[DebuggerHidden, DebuggerStepThrough]
		public IValidationDef<T> Is(Func<T, bool> predicate, string message = null) => Is(predicate, message, Empty.ArrayOf<object>());

		[DebuggerHidden, DebuggerStepThrough]
		public IValidationDef<T> Is<TAny>(Func<T, bool> predicate, string message, params TAny[] validValues)
		{
			if (ContinueValidate && predicate(Value) == false)
				OnInvalid(message, validValues);

			return this;
		}

		[DebuggerHidden, DebuggerStepThrough]
		public IValidationDef<T> IsNot(Func<T, bool> predicate, string message = null) => IsNot(predicate, message, Empty.ArrayOf<object>());

		[DebuggerHidden, DebuggerStepThrough]
		public IValidationDef<T> IsNot<TAny>(Func<T, bool> predicate, string message, params TAny[] validValues)
		{
			if (ContinueValidate && predicate(Value))
				OnInvalid(message, validValues);

			return this;
		}

		[DebuggerHidden, DebuggerStepThrough]
		public IValidationDef<T> When(Func<T, bool> ifTrue) => When(ifTrue(Value));

	    [DebuggerHidden, DebuggerStepThrough]
		public IValidationDef<T> WhenNot(Func<T, bool> ifTrue) => When(!ifTrue(Value));

	    [DebuggerHidden, DebuggerStepThrough]
		public IValidationDef<T> When(bool ifTrue)
		{
			ContinueValidate = ifTrue;
			return this;
		}
		
		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<T> Create(T value, string namedAs) => new ValidationDef<T>
		{
			Value = value,
			GetName = () => namedAs
		};

		[DebuggerHidden, DebuggerStepThrough]
		public static IValidationDef<T> Create(Expression<Func<T>> expr)
		{
			T value;

			try { value = expr.Compile()(); }
			catch (Exception ex) 
			{
				throw new ArgumentNotValidException(string.Format("Encountered {0} while accessing {1}", ex.GetType().FullName, GetMemberName(expr)));
			}

			return new ValidationDef<T>
			{
				Value = value,
				GetName = () => GetMemberName(expr)
			};
		}

		[DebuggerHidden, DebuggerStepThrough]
		private void OnInvalid<TAny>(string message, params TAny[] validValues)
		{
			if (string.IsNullOrWhiteSpace(message))
				message = string.Format("{0} is not a valid value for {1}.", AsString(Value), GetName());

			message =
				message
					.Replace("$name$", GetName())
					.Replace("$actualValue$", AsString(Value))
					.Replace("$validValues$", validValues.IfNotNull(x => string.Join(",", x.Select(AsString))));

			throw new ArgumentNotValidException(message);
		}

		[DebuggerHidden, DebuggerStepThrough]
		private static string AsString<TAny>(TAny value)
		{
			if (default(TAny) == null && value == null) //if reference type and null
				return "NULL";

			if (typeof(TAny).In(typeof(string), typeof(char)))
				return value.ToString().SurroundWith("'");

			return value.ToString();
		}
	}
}

