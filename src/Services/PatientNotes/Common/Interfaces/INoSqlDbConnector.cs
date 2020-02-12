using PatientNotes.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientNotes.Common.Interfaces
{
    public interface INoSqlDbConnector<T> where T : BaseEntity
    {
        Task<T> Insert(T entity);

        Task<IEnumerable<T>> GetAll(string id);
    }
}
