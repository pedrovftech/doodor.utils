namespace Doodor.Utils.Modeling.Mapping
{
    public interface IMapper<in TSource, TTarget>
        where TSource : class
        where TTarget : class, new()
    {
        void Map(TSource source, TTarget target);
        TTarget Map(TSource source);
    }
}