namespace FunctionalApp
{
    public class FormDto
    {
        public string Name { get; }
        public string Year { get; }
        public int? Id { get; }

        public FormDto(string name, string year, int? id)
        {
            Name = name;
            Year = year;
            Id = id;
        }
    }
}
