namespace Doodor.Utils.Modeling.Models
{
    public abstract class ValueObject : Model
    {
        public abstract override int GetHashCode();
        public abstract override bool Equals(object obj);
    }
}