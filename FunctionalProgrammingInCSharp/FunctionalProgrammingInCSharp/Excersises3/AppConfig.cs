using System;
using System.Collections.Specialized;
using System.Configuration;
using static FunctionalProgrammingInCSharp.OptionCreators;

namespace FunctionalProgrammingInCSharp
{
    public class AppConfig
    {
        NameValueCollection source;

        public AppConfig() : this(ConfigurationManager.AppSettings) { }

        public AppConfig(NameValueCollection source)
        {
            this.source = source;
        }

        public Option<T> Get<T>(string name)
        {
            return source[name] == null ? None : ConvertIfPossible<T>(name);
        }

        private Option<T> ConvertIfPossible<T>(string name)
        {
            try
            {
                return Some((T)Convert.ChangeType(source[name], typeof(T)));
            }
            catch (Exception)
            {
                return None;
            }
        }

        public T Get<T>(string name, T defaultValue)
        {
            return Get<T>(name).Match(
                () => defaultValue,
                (some) => some);
        }
    }
}
