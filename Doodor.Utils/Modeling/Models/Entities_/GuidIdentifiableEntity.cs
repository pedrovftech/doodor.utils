using System;

namespace Doodor.Utils.Modeling.Models
{
    public class GuidIdentifiableEntity : Entity<Guid>
    {
        public GuidIdentifiableEntity() =>
            Id = Guid.NewGuid();

        public override int GetHashCode() => Id.GetHashCode();

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (!(obj is GuidIdentifiableEntity objAsGuidIdEntity)) return false;
            if (GetUnproxiedType() != objAsGuidIdEntity.GetUnproxiedType()) return false;
            if (Id == objAsGuidIdEntity.Id) return true;

            return false;
        }
    }
}