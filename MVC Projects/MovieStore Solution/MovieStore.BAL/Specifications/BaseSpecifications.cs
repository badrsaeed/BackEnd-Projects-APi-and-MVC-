using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.BAL.Specifications
{
    public class BaseSpecifications<T> : ISpecification<T> where T : class
    {
        public Expression<Func<T, bool>> Criteria {  get; set; }
        public IEnumerable<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();

        public BaseSpecifications()
        {

        }

        public BaseSpecifications(Expression<Func<T, bool>> criteria)
        {
            Criteria= criteria;
        }
    }

    
}
