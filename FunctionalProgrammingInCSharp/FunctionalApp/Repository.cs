using FunctionalProgrammingInCSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Immutable;
using static FunctionalProgrammingInCSharp.OptionCreators;

namespace FunctionalApp
{
    public interface IChange
    {
        int Id { get; }
        DateTime TimeStamp { get; }
    }

    public class PersonCreated : IChange
    {
        public Person Person { get; }
        private int _newId;

        public PersonCreated(Person t, int newId)
        {
            Person = t;
            _newId = newId;
        }

        public int Id => _newId;

        public DateTime TimeStamp => DateTime.Now;
    }

    public class PersonUpdated : IChange
    {
        public Person Person { get; }

        public PersonUpdated(Person t)
        {
            Person = t;
        }

        public int Id => Person.Id.Value;

        public DateTime TimeStamp => DateTime.Now;
    }

    public class PersonDeleted: IChange
    {
       private int _id;

        public PersonDeleted(int id)
        {
            _id = id;
        }

        public int Id => _id;

        public DateTime TimeStamp => DateTime.Now;
    }

    internal class Repository : IRepository<Person> 
    {
        private ImmutableList<IChange> _store = ImmutableList<IChange>.Empty;
        public Option<Person> Get(int id)
        {
            var events = _store
                .Where(a => a.Id == id)
                .OrderBy(a => a.TimeStamp);

            return events.Any() && !events.Any(e => (e is PersonDeleted)) ?
                Some(new Person(
                    events.Aggregate("", (acc, next) => NameReducer(acc, next)),
                    events.Aggregate(0, (acc, next) => YearReducer(acc, next)),
                    events.Aggregate((int?)null, (acc, next) => IdReducer(acc, next)))) :
                    None;
        }

        private string NameReducer(string old, IChange arg2)
        {
            if(arg2 is PersonCreated pc)
            {
                return pc.Person.Name;
            }
            if (arg2 is PersonUpdated pu)
            {
                return pu.Person.Name;
            }
            return old;
        }

        private int? IdReducer(int? old, IChange arg2)
        {
            if (arg2 is PersonCreated pc)
            {
                return pc.Id;
            }
            if (arg2 is PersonUpdated pu)
            {
                return pu.Id;
            }
            return old;
        }
        private int YearReducer(int old, IChange arg2)
        {
            if (arg2 is PersonCreated pc)
            {
                return pc.Person.Year;
            }
            if (arg2 is PersonUpdated pu)
            {
                return pu.Person.Year;
            }
            return old;
        }

        public void Save(Person t)
        {
            if (t.Id == null)
            {
                var newId = DateTime.Now.Millisecond; //todo
                _store = _store.Add(new PersonCreated(t, newId));
            }
            else
            {
                _store = _store.Add(new PersonUpdated(t));
            }
        }

        public IEnumerable<Person> GetAll()
        {
            return _store
                .Select(a => a.Id)
                .Distinct()
                .Select(id => Get(id).Match(() => null, p => p))
                .Where(p => p != null);
        }

        public void Delete(int value)
        {
            _store = _store.Add(new PersonDeleted(value));
        }
    }
}