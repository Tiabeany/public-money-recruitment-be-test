using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace VacationRental.Core.Specifications
{
    // Copied from https://enterprisecraftsmanship.com/posts/specification-pattern-c-implementation/ at august 8th 2021
    public abstract class Specification<T>
    {
        public abstract Expression<Func<T, bool>> ToExpression();

        public bool IsSatisfiedBy(T entity)
        {
            Func<T, bool> predicate = ToExpression().Compile();
            return predicate(entity);
        }
    }
}
