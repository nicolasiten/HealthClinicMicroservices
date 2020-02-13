using PatientNotes.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PatientNotes.Common.Interfaces
{
    public interface INoSqlDbConnector<T> where T : BaseEntity
    {
        Task<T> InsertAsync(T entity);

        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetAllByFilterAsync(Expression<Func<T, bool>> filter);

        Task UpdateAsync(T entity);

        Task DeleteAsync(string id);
    }
}
