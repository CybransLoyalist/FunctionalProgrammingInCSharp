namespace FunctionalApp
{
    public class Person
    {
        public int? Id { get; }
        public string Name { get; }
        public int Year { get; }

        public Person(string name, int year, int? id = null)
        {
            Name = name;
            Year = year;
            Id = id;
        }

        public override string ToString()
        {
            return $"Person of Name {Name}, born in {Year}, with Id {Id}";
        }
    }
}
