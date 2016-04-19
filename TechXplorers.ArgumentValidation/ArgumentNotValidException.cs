using System;
using System.Linq;
using static System.Environment;

namespace TechXplorers.ArgumentValidation
{
    public class ArgumentNotValidException : ApplicationException
    {
        private static readonly string @namespace = typeof(ArgumentNotValidException).Namespace;
        private static readonly string @localLines = $"at {@namespace}.{nameof(ValidationDef<object>)}";

        public ArgumentNotValidException(string message)
            : base(message){}

        public override string StackTrace 
            => string.Join(NewLine, base.StackTrace.Split(new[] { NewLine }, StringSplitOptions.None).Where(x => !x.Contains(@namespace)));

        public override string ToString() 
            => string.Join(NewLine, base.ToString().Split(new[] { NewLine }, StringSplitOptions.None).Where(x => !x.Contains(@localLines)));
    }
}
