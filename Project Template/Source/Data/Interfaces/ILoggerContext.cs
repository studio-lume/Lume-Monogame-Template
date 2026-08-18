using System.Collections.Generic;

namespace Project_Template.Source.Data.Interfaces {
    namespace _Project.Scripts.Data.Interfaces {
        public interface ILoggerContext {
            public Dictionary<string, Dictionary<string, object>> StackTrace { get; }

            /// <summary>
            ///     Switches the active section for subsequent Add calls.
            /// </summary>
            public ILoggerContext AddSection(string name);

            /// <summary>
            ///     Adds a key-value pair to the current active section.
            /// </summary>
            public ILoggerContext Add(string name, object value);
        }
    }
}