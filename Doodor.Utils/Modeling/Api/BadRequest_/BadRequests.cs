using Doodor.Utils.Utilities.Collections;
using Doodor.Utils.Utilities.Modeling.Api.Queries;
using Doodor.Utils.Utilities.Modeling.Api.Validation;
using System.Collections.Generic;
using System.Linq;
using static Doodor.Utils.ExceptionHandling.Exceptions;

namespace Doodor.Utils.Utilities.Modeling.Api
{
    public static class BadRequests
    {
        public static BadRequestDto BadRequestReasonFrom(string descricao) =>
            new BadRequestDto { new BadRequestDetailDto(descricao, null) };


        public static BadRequestDto BadRequestReasonFrom(
            IEnumerable<string> descricoes) =>
                new BadRequestDto(descricoes?.Select(x => new BadRequestDetailDto(x, null)));


        public static BadRequestDto BadRequestReasonFrom(
            params string[] descricoes) =>
                BadRequestReasonFrom((IEnumerable<string>)descricoes);


        public static BadRequestDto BadRequestReasonFrom(
            IEnumerable<(string descricao, string tag)> descricoes) =>
                new BadRequestDto(descricoes?.Select(x => new BadRequestDetailDto(x.descricao, x.tag)));


        public static BadRequestDto BadRequestReasonFrom(
            params (string descricao, string tag)[] descricoes) =>
                BadRequestReasonFrom((IEnumerable<(string, string)>)descricoes);



        // BadRequests relacionados a Input/DtoValidation 

        public static BadRequestDto BadRequestReasonFrom(
            IEnumerable<DtoValidationDetail> dtoValidationDets) =>
                new BadRequestDto(dtoValidationDets?
                    .Select(x => new BadRequestDetailDto(x.Message, x.MemberName)));

        public static BadRequestDto BadRequestReasonFrom(
            IReadOnlyCollection<ValueFailureDetail> failureDetails) =>
                new BadRequestDto(failureDetails?.Select(x => new BadRequestDetailDto(x.Description, x.Tag)));


        // BadRequests relacioandos a FieldSortConfigParseResult<TField>

        public static BadRequestDto BadRequestReasonFrom<TField>(
            FieldSortConfigParseResult<TField> fscpr)
        {
            if (fscpr == null)
                throw ArgNullEx(nameof(fscpr));

            if (fscpr.Succeeded)
                throw ArgEx(
                    nameof(fscpr),
                    $"'{nameof(fscpr)}' com sucesso, não é possível montar '{nameof(BadRequestDto)}'");

            IReadOnlyCollection<string>
                specifiedFieldNames(IEnumerable<FieldSortConfig<TField>> fieldConfigs) =>
                    fieldConfigs
                        .Select(x => x.SpecifiedFieldText)
                            .ToReadOnlyCollection();

            string message = "Parâmetros de ordenação inválidos";
            string messageCompl = null;
            IReadOnlyCollection<string> fieldNames = null;

            if (fscpr.HasFailedFieldParsings)
            {
                messageCompl = ", campos inválidos";
                fieldNames = specifiedFieldNames(
                    fscpr.AllFieldSortConfigs.Where(x => x.FieldParsingFailed));
            }
            else if (fscpr.HasUnexpectedFields)
            {
                messageCompl = ", campos inesperados";
                fieldNames = specifiedFieldNames(
                    fscpr.AllFieldSortConfigs.Where(x => x.UnexpectedField));
            }
            else if (fscpr.HasUndefinedSortDirections)
            {
                messageCompl = ", sinais de ordenação (+, -) indefinidos";
                fieldNames = specifiedFieldNames(
                    fscpr.AllFieldSortConfigs.Where(x => x.UndefinedDirection));
            }
            else if (fscpr.HasNonUniqueFields)
            {
                messageCompl = ", campos não únicos";
                fieldNames = specifiedFieldNames(
                    fscpr.AllFieldSortConfigs.Where(x => x.NonUniqueField));
            }

            if (fieldNames?.Any() ?? false)
                message = $"{message}{messageCompl}: {string.Join(", ", fieldNames)}";

            return BadRequestReasonFrom(message);
        }
    }
}
