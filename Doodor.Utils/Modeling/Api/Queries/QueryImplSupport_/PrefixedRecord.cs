using System.Collections.Generic;
using static Doodor.Utils.ExceptionHandling.Exceptions;

namespace Doodor.Utils.Utilities.Modeling.Api.Queries
{
    public class PrefixedFields
    {
        private readonly string _prefix;
        private readonly IDictionary<string, object> _targetRecord;

        public PrefixedFields(
            string prefix, IDictionary<string, object> targetRecord)
        {
            _prefix = string.IsNullOrEmpty(prefix)
                ? throw ArgMustBeProvidedEx(nameof(prefix))
                : prefix;

            _targetRecord = targetRecord ?? throw ArgNullEx(nameof(targetRecord));
        }

        public PrefixedFields(string prefix, dynamic targetRecord) :
            this(prefix, (IDictionary<string, object>)targetRecord)
        { }

        public T FieldValue<T>(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName)) throw ArgMustBeProvidedEx(nameof(fieldName));
            return (T)_targetRecord[_prefix + fieldName];
        }
    }
}