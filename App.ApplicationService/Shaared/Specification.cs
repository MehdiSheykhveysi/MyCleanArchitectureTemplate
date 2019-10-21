using System;
using System.Linq.Expressions;

namespace App.ApplicationService.Shaared
{
    public abstract class Specification<TEntity, Tresult> where TEntity : class
    {
        protected bool IsSatisfiedBy(TEntity entity)
        {
            Func<TEntity, Tresult> predicate = ToExpression().Compile();
            return predicate(entity) is Tresult;
        }

        public abstract Expression<Func<TEntity, Tresult>> ToExpression();
    }
}
