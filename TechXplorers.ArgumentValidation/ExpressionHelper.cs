using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TechXplorers.ArgumentValidation
{
    internal static class ExpressionHelper
    {
        internal static string GetMemberName(Expression expr)
        {
            var memberNames = new List<string>();
            var memberExpr = GetMemberExpression(expr);

            while (memberExpr != null)
            {
                if (memberExpr.NodeType == ExpressionType.ArrayLength)
                    memberNames.Add("Length");
                if (memberExpr.NodeType == ExpressionType.MemberAccess)
                    memberNames.Add(memberExpr.Member.Name);

                memberExpr = GetMemberExpression(memberExpr.Expression);
            }

            memberNames =
                memberNames
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Reverse()
                    .ToList();

            return string.Join(".", memberNames);
        }

        private static MemberExpression GetMemberExpression(Expression expr)
        {
            if (expr == null)
                return null;

            if (expr is MemberExpression)
                return (MemberExpression) expr;

            var lambdaExpr = expr as LambdaExpression;
            return lambdaExpr?.Body as MemberExpression;
        }
    }
}
