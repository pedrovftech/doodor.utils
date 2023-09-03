using System;

namespace Doodor.Utils.Utilities
{
    public interface ITypeParser
    {
        Type TargetType { get; }
        bool TryParse(string value, out object result);
    }

    /// <summary>
    /// Facilitador para parsing em implementações utilitárias e genéricas.
    /// </summary>
    public interface ITypeParser<T> : ITypeParser
    {
        bool TryParse(string value, out T result);
    }
}