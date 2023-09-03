namespace Doodor.Utils.Utilities.Collections
{
    public interface IReadOnlyStringKeyValuePairs
    {
        string this[string key] { get; }
        bool CanCheckContainsKey { get; }
        bool ContainsKey(string key);
    }
}