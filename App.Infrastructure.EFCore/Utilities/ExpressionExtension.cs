using System;
using System.Linq.Expressions;

namespace App.Infrastructure.Utilities
{
    public static class ExpressionExtension
    {
        public static object GetRightConstant<TFirstparam, TOut>(this Expression<Func<TFirstparam, TOut>> predicate)
        {
            BinaryExpression operation = (BinaryExpression)predicate.Body;
            ConstantExpression right = (ConstantExpression)operation.Right;
            return right.Value;
        }
    }
}
