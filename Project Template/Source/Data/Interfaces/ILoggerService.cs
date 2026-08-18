using System.Runtime.CompilerServices;
using Project_Template.Source.Data.Enums;
using Project_Template.Source.Data.Interfaces._Project.Scripts.Data.Interfaces;

namespace Project_Template.Source.Data.Interfaces {
    public interface ILoggerService {
        public void Log(LogLevel level, LogCategory category, string message, ILoggerContext context);
        public void SetCategory(LogCategory category, bool isEnabled);

        public void IsNotNull<T, TClass>(
            LogCategory category,
            T reference,
            TClass parentClass = null,
            [CallerArgumentExpression("reference")]
            string referenceName = null,
            [CallerFilePath] string callerFilePath = null,
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMemberName = null
        ) where TClass : class;
    }
}