using System.Collections;
using System.Text;

namespace Project_Template.Source.Services.LoggerService {
    public static class LoggerStringify {
        public static string Stringify(object value) => value switch {
            string stringValue => stringValue,
            IDictionary dictionary => BuildDictionaryString(dictionary),
            IEnumerable collection => BuildCollectionString(collection),
            _ => value.ToString() ?? "null"
        };

        static string BuildCollectionString(IEnumerable collection) {
            var stringBuilder = new StringBuilder("[");
            foreach (var item in collection) {
                stringBuilder.Append(Stringify(item)).Append(',');
            }

            // Clean up trailing comma for cleaner output
            return stringBuilder.ToString().TrimEnd(',') + "]";
        }

        static string BuildDictionaryString(IDictionary dictionary) {
            var stringBuilder = new StringBuilder("{");
            foreach (DictionaryEntry entry in dictionary) {
                stringBuilder.Append($"[{Stringify(entry.Key)}: {Stringify(entry.Value)}],");
            }

            return stringBuilder.ToString().TrimEnd(',') + "}";
        }
    }
}