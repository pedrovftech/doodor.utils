namespace Doodor.Utils.Utilities.Modeling.Api.Validation
{
    public interface IDtoValidator<in TDto> where TDto : Dto
    {
        DtoValidationResult TryValidate(TDto target);
    }
}