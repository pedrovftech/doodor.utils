namespace Doodor.Utils.Modeling.Models
{
    public abstract class Entity<TID> : Entity
    {
        public virtual TID Id { get; set; }
        public override object UntypedId => Id;
    }
}