namespace FunctionalProgrammingInCSharp
{
    public interface IRepository<T>
    {
        Option<T> Get(int id);
        void Save(T t);
    }
}
