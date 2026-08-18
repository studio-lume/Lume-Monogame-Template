using System.Collections.Generic;
using Project_Template.Source.Data.Interfaces._Project.Scripts.Data.Interfaces;

namespace Project_Template.Source.Services.LoggerService {
    public class LoggerContext : ILoggerContext {
        // Nested dictionary structure: [SectionName] -> [Key -> Value]
        public Dictionary<string, Dictionary<string, object>> StackTrace { get; } = new();
        string currentSection;

        public LoggerContext() {
            AddSection("Context");
        }

        /// <summary>
        ///     Switches the active section for subsequent Add calls.
        /// </summary>
        public ILoggerContext AddSection(string name) {
            currentSection = name;
            StackTrace[currentSection] = new();
            return this;
        }

        /// <summary>
        ///     Adds a key-value pair to the current active section.
        /// </summary>
        public ILoggerContext Add(string name, object value) {
            StackTrace[currentSection][name] = value;
            return this;
        }
    }
}