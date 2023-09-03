using AutoMapper;
using static Doodor.Utils.ExceptionHandling.Exceptions;


namespace Doodor.Utils.Modeling.Mapping
{
    public class AutoMapperMapper<TSource, TTarget> :
       IMapper<TSource, TTarget>
           where TSource : class
           where TTarget : class, new()
    {
        private readonly IMapper _autoMapper;

        public AutoMapperMapper(IMapper autoMapper) =>
            _autoMapper = autoMapper ?? throw ArgNullEx(nameof(autoMapper));


        public virtual void Map(TSource source, TTarget target) =>
              _autoMapper.Map(
                   (source ?? throw ArgNullEx(nameof(source))),
                   (target ?? throw ArgNullEx(nameof(target))));


        public virtual TTarget Map(TSource source)
        {
            var target = new TTarget();
            Map(source, target);
            return target;
        }
    }
}