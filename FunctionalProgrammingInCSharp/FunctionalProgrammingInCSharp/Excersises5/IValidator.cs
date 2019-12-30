namespace FunctionalProgrammingInCSharp
{
    public interface IValidator<T>
    {
        Option<T> Validate(T t);
    }
}
