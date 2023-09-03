namespace Doodor.Utils.Utilities
{
    public struct ValueFailureDetail
    {
        public ValueFailureDetail(string description, string tag = null)
        {
            Tag = string.IsNullOrEmpty(tag) ? "__general__" : tag;
            Description = description;
        }

        public string Tag { get; }
        public string Description { get; private set; }

        public static implicit operator
            ValueFailureDetail(string description) =>
            new ValueFailureDetail(description);
    }
}