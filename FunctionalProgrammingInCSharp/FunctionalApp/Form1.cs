using FunctionalProgrammingInCSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static FunctionalProgrammingInCSharp.EitherCreators;
using static FunctionalProgrammingInCSharp.OptionCreators;
using Unit = System.ValueTuple;

namespace FunctionalApp
{

    public partial class Form1 : Form
    {
        private readonly IRepository<Person> _repository;
        public Form1()
        {
            _repository = new Repository();
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            int? idOrEmpty = GetIdFromForm();

            Right(new FormDto(nameTextBox.Text, yearTextBox.Text, idOrEmpty))
                .Bind(IsNameValid)
                .Bind(IsYearValid)
                .Map(dto => new Person(dto.Name, int.Parse(dto.Year), dto.Id))
            .Match(
                invalid => NotifyThatFormIsInvalid(invalid),
                valid => Save(valid));
        }

        private int? GetIdFromForm()
        {
            int? idOrEmpty = null;
            if (int.TryParse(idTextBox.Text, out int id))
            {
                idOrEmpty = id;
            }

            return idOrEmpty;
        }

        private Unit Save(Person valid)
        {
            _repository.Save(valid);
            errorTextBox.Text = $"Saved successfully: {_repository.Get(0).Match(() => "Problem occurred",p => p.ToString())}";
            listTextBox.Text = FormatList();
            return new Unit();
        }

        private string FormatList()
        {
            var all = _repository.GetAll();
            return string.Join("\r\n", Enumerable.Range(1, all.Count()).Zip(all, (num, elem) => $"{num}. {elem}"));
        }

        private Unit NotifyThatFormIsInvalid(string message)
        {
            errorTextBox.Text = message;
            return new Unit();
        }

        private Either<string, FormDto> IsNameValid(FormDto arg)
        {
            if(string.IsNullOrEmpty(arg.Name))
            {
                return "Name cannot be empty";
            }
            return arg;
        }

        private Either<string, FormDto> IsYearValid(FormDto arg)
        {
            if(!int.TryParse(arg.Year, out int year))
            {
                return $"Cannot parse {arg.Year} to a valid year";
            }
            if (year < 1900 || year > 2020)
            {
                return "Year must be between 1900 and 2020";
            }
            return arg;
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            int? idOrEmpty = GetIdFromForm();
            if(idOrEmpty != null)
            {
                _repository.Delete(idOrEmpty.Value);
                listTextBox.Text = FormatList();
            }
        }
    }
}
