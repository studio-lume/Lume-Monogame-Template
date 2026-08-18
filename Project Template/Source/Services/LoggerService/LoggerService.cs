using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Project_Template.Source.Data.Enums;
using Project_Template.Source.Data.Interfaces;
using Project_Template.Source.Data.Interfaces._Project.Scripts.Data.Interfaces;

namespace Project_Template.Source.Services.LoggerService {
    public class LoggerService : ILoggerService {
        readonly HashSet<LogCategory> enabledCategories = [];

        public LoggerService() {
            foreach (var category in Enum.GetValues<LogCategory>()) {
                enabledCategories.Add(category);
            }
        }
        
        /// <summary>
        ///     Toggles the runtime visibility of specific log categories. Editor-only.
        /// </summary>
        public void SetCategory(LogCategory category, bool isEnabled) {
            if (isEnabled) {
                enabledCategories.Add(category);
            } else {
                enabledCategories.Remove(category);
            } 
        }

        /// <summary>
        ///     Formats and dispatches a log message to the Unity Console based on its severity and category.
        /// </summary>
        /// <param name="level">The severity level (Info, Warning, Error).</param>
        /// <param name="category">The system category associated with the log.</param>
        /// <param name="message">The primary log message.</param>
        /// <param name="context">An <see cref="ILoggerContext" /> containing structured metadata to append to the output.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if an undefined <see cref="LogLevel" /> is provided.</exception>
        public void Log(LogLevel level, LogCategory category, string message, ILoggerContext context) {
            if (level == LogLevel.Info && !enabledCategories.Contains(category)) {
                return;
            }

            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"[{category}] {message}");

            foreach (var section in context.StackTrace) {
                if (section.Value.Count == 0) {
                    continue;
                }

                var sectionName = section.Key.ToUpper().Replace(' ', '_');
                stringBuilder.Append($"{Environment.NewLine}{sectionName}:");

                foreach (var instance in section.Value) {
                    var stringifiedValue = LoggerStringify.Stringify(instance.Value);
                    stringBuilder.Append($"{Environment.NewLine}   {instance.Key}: {stringifiedValue}");
                }
            }

            stringBuilder.Append(Environment.NewLine);

            var output = stringBuilder.ToString();
            switch (level)
            {
                case LogLevel.Info:
                    Console.WriteLine(output);
                    break;

                case LogLevel.Error:
                    Console.Error.WriteLine($"[ERROR] {output}");
                    break;

                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(level),
                        level,
                        "Unknown log level.");
            }
        }

        /// <summary>
        ///     Performs a "Sanity Check" on an object reference. If the object is null, it logs
        ///     a comprehensive error report, including file/line number information.
        /// </summary>
        /// <typeparam name="T">The type of the reference to check.</typeparam>
        /// <typeparam name="TClass">The type of the parent class (for context).</typeparam>
        /// <param name="category">The category to log under if the check fails.</param>
        /// <param name="reference">The object to validate.</param>
        /// <param name="parentClass">Optional reference to the calling class instance.</param>
        /// <param name="referenceName">Injected by compiler: The name of the variable being checked.</param>
        /// <param name="callerFilePath">Injected by compiler: Source file path.</param>
        /// <param name="callerLineNumber">Injected by compiler: Line number.</param>
        /// <param name="callerMemberName">Injected by compiler: Method name.</param>
        public void IsNotNull<T, TClass>(
            LogCategory category,
            T reference,
            TClass parentClass = null,
            [CallerArgumentExpression("reference")]
            string referenceName = null,
            [CallerFilePath] string callerFilePath = null,
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMemberName = null
        ) where TClass : class {
            if (reference is null) {
                return;
            }

            var loggerContext = new LoggerContext()
                .AddSection("Variable Information")
                .Add("Variable Name", referenceName)
                .AddSection("Call Site")
                .Add("File", Path.GetFileName(callerFilePath))
                .Add("Line", callerLineNumber)
                .Add("Method", callerMemberName);

            if (parentClass is not null) {
                loggerContext
                    .AddSection("Parent Class Information")
                    .Add("Class Name", parentClass.GetType());
            }

            Log(
                LogLevel.Error,
                category,
                $"variable of {referenceName} is null",
                loggerContext
            );
        }
    }
}