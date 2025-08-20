using AdminPanel.Data.Interfaces;
using System.Linq.Expressions;

namespace AdminPanel.Data.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {
        }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; protected set; }

        public List<Expression<Func<T, object>>> Includes { get; } =
            new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }

        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
        //protected void AddCriteria(Expression<Func<T, bool>> newCriteria)
        //{
        //    // If there's no existing criteria, the new one becomes the main one.
        //    if (Criteria == null)
        //    {
        //        Criteria = newCriteria;
        //        return;
        //    }

        //    // Combine the existing criteria with the new one using Expression.AndAlso
        //    Criteria = Expression.Lambda<Func<T, bool>>(
        //        Expression.AndAlso(Criteria.Body, newCriteria.Body),
        //        Criteria.Parameters[0]
        //    );
        //}
        protected void AddCriteria(Expression<Func<T, bool>> newCriteria)
        {
            if (Criteria == null)
            {
                Criteria = newCriteria;
                return;
            }

            var param = Criteria.Parameters[0];
            var replacer = new ParameterReplaceVisitor(newCriteria.Parameters[0], param);
            var newBody = replacer.Visit(newCriteria.Body);
            Criteria = Expression.Lambda<Func<T, bool>>(Expression.AndAlso(Criteria.Body, newBody!), param);
        }

        private sealed class ParameterReplaceVisitor : ExpressionVisitor
        {
            private readonly ParameterExpression _from;
            private readonly ParameterExpression _to;
            public ParameterReplaceVisitor(ParameterExpression from, ParameterExpression to)
            {
                _from = from; _to = to;
            }
            protected override Expression VisitParameter(ParameterExpression node)
                => node == _from ? _to : base.VisitParameter(node);
        }
    }
}