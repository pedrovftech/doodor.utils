using System;

namespace Doodor.Utils.Modeling.Models
{
    public abstract class Entity : Model
    {
        // TODO: Entity: Tratar Domain Events.

        public abstract object UntypedId { get; }

        public Type GetUnproxiedType() => GetType();

        public abstract override int GetHashCode();
        public abstract override bool Equals(object obj);
    }
}