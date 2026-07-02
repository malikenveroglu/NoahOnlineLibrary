using NoahOnlineLibrary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NoahOnlineLibrary.Application.Interfaces.IRepository
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Add(T entity);

        void Delete(T entity);   

        void Update(T entity);

        bool Any(Expression<Func<T, bool>> predicate);

        T? GetById(int id);

        List<T> GetAll();

        void SaveChanges();
    }
}
