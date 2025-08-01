using NutritionTracker.Api.Core.Filters;
using NutritionTracker.Api.Data;
using System.Linq.Expressions;

namespace NutritionTracker.Api.Core.Helpers
{
    public static class UserPredicateBuilder
    {
        public static Expression<Func<User, bool>> BuildPredicates(UserFiltersDTO filters)
        {
            Expression<Func<User, bool>> expression = u => true;  // Default start with true

            if (!string.IsNullOrEmpty(filters.Username))
                expression = Combine(expression, u => u.Username == filters.Username);

            if (!string.IsNullOrEmpty(filters.Email))
                expression = Combine(expression, u => u.Email == filters.Email);

            if (!string.IsNullOrEmpty(filters.UserRole))
                expression = Combine(expression, u => u.UserRole.ToString() == filters.UserRole);

            return expression;
        }

        public static Expression<Func<User, bool>> Combine(Expression<Func<User, bool>> first, Expression<Func<User, bool>> second)
        {
            var parameter = Expression.Parameter(typeof(User));

            var combined = Expression.AndAlso(
                Expression.Invoke(first, parameter),
                Expression.Invoke(second, parameter)
            );

            return Expression.Lambda<Func<User, bool>>(combined, parameter);
        }

        //TODO: Think if i should use Linqkit nuget packege for this instead of combine to write it more cleanly with predicate.And()
    }
}

   
