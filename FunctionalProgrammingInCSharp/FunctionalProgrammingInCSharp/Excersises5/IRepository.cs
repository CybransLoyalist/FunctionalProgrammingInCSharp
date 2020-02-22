using System.Collections.Generic;

namespace FunctionalProgrammingInCSharp
{
    public interface IRepository<T>
    {
        Option<T> Get(int id);
        void Save(T t);
        IEnumerable<T> GetAll();
        void Delete(int value);
    }
}
