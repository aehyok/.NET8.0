using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.EntityFrameworkCore.Repository
{
    public class Specifications<TEntity> : Specification<TEntity>
    {
        public static Specifications<TEntity> Create()
        {
            return new Specifications<TEntity>();
        }

        public new ISpecificationBuilder<TEntity> Query => base.Query;
    }

    public class Specifications<TEntity, TResult> : Specification<TEntity, TResult>
    {
        public static Specifications<TEntity, TResult> Create()
        {
            return new Specifications<TEntity, TResult>();
        }

        public new ISpecificationBuilder<TEntity, TResult> Query => base.Query;
    }
}
