namespace Doodor.Utils.Utilities.Modeling.Api.Queries
{
    public class FieldSortConfig<TField>
    {
        public FieldSortConfig(
            TField field,
            string specifiedFieldText,
            int specifiedOrder,
            bool fieldParsingFailed,
            bool unexpectedField,
            FieldSortDirection direction)
        {
            Field = field;
            SpecifiedFieldText = specifiedFieldText;
            SpecifiedOrder = specifiedOrder;
            FieldParsingFailed = fieldParsingFailed;
            UnexpectedField = unexpectedField;
            Direction = direction;
        }

        public TField Field { get; private set; }
        public string SpecifiedFieldText { get; private set; }
        public int SpecifiedOrder { get; private set; }
        public bool FieldParsingFailed { get; private set; }
        public bool UnexpectedField { get; private set; }
        public FieldSortDirection Direction { get; private set; }
        public bool UndefinedDirection { get => Direction == FieldSortDirection.Undefined; }

        public bool NonUniqueField { get; internal set; }
    }
}