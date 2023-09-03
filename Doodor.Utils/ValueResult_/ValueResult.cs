using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Doodor.Utils.ExceptionHandling.Exceptions;

namespace Doodor.Utils.Utilities
{
    public struct ValueResult
    {
        private static readonly ValueFailureDetail[] EmptyFailureDetails = new ValueFailureDetail[0];

        public bool Succeeded { get; private set; }
        public IReadOnlyCollection<ValueFailureDetail> FailureDetails { get; private set; }

        public static ValueResult Success() =>
            new ValueResult { Succeeded = true, FailureDetails = EmptyFailureDetails };

        public static ValueResult Failure(IEnumerable<ValueFailureDetail> failureDetails) =>
            new ValueResult { Succeeded = false, FailureDetails = (failureDetails?.ToArray()) ?? EmptyFailureDetails };

        public static ValueResult Failure() =>
            new ValueResult { Succeeded = false, FailureDetails = EmptyFailureDetails };

        public static ValueResult Failure(params string[] descriptions) =>
            Failure(descriptions?.Select(x => (ValueFailureDetail)x));

        public static implicit operator bool(ValueResult valueResult) => valueResult.Succeeded;
    }

    /// <summary>
    /// Suporta situações onde é indesejável lançar exceptions como validação de regras de negócio em web servers (overhead).
    /// </summary>
    /// <typeparam name="TResultValue"></typeparam>
    public struct ValueResult<TResultValue>
    {
        private static readonly ValueFailureDetail[] EmptyFailureDetails = new ValueFailureDetail[0];

        public bool Succeeded { get; private set; }
        public TResultValue Value { get; private set; }
        public IReadOnlyCollection<ValueFailureDetail> FailureDetails { get; private set; }

        public ValueResult AsValueResult() =>
            this
                ? ValueResult.Success()
                : ValueResult.Failure(this.FailureDetails);


        public static ValueResult<TResultValue> Success(TResultValue value) =>
            new ValueResult<TResultValue> { Succeeded = true, Value = value, FailureDetails = EmptyFailureDetails };


        public static ValueResult<TResultValue> Failure() =>
            new ValueResult<TResultValue> { Succeeded = false, FailureDetails = EmptyFailureDetails };


        public static ValueResult<TResultValue> Failure(IEnumerable<ValueFailureDetail> detalhesFalhas) =>
            new ValueResult<TResultValue>
            { Succeeded = false, FailureDetails = (detalhesFalhas?.ToArray()) ?? EmptyFailureDetails };


        public static ValueResult<TResultValue> Failure(
            TResultValue value, IEnumerable<ValueFailureDetail> failureDetails) =>
            new ValueResult<TResultValue>
            { Succeeded = false, Value = value, FailureDetails = (failureDetails?.ToArray()) ?? EmptyFailureDetails };


        public static ValueResult<TResultValue> Failure(params string[] descricoes) =>
            Failure(descricoes?.Select(x => (ValueFailureDetail)x));


        public static implicit operator bool(ValueResult<TResultValue> valueResult) =>
            valueResult.Succeeded;


        public static implicit operator ValueResult(ValueResult<TResultValue> valueResult) =>
            valueResult.AsValueResult();
    }

    public static class ValueResultExtensions
    {
        public static Task<ValueResult> AsCompletedTask(this ValueResult @this) =>
            Task.FromResult(@this);


        public static Task<ValueResult<TResultValue>> AsCompletedTask<TResultValue>(
            this ValueResult<TResultValue> @this) =>
            Task.FromResult(@this);


        public static ValueTask<ValueResult> AsCompletedValueTask(this ValueResult @this) =>
            new ValueTask<ValueResult>(@this);


        public static ValueTask<ValueResult<TResultValue>> AsCompletedValueTask<TResultValue>(
            this ValueResult<TResultValue> @this) =>
            new ValueTask<ValueResult<TResultValue>>(@this);


        public static string Inline(this IEnumerable<ValueFailureDetail> @this) =>
            string.Join(
                Environment.NewLine,
                (@this ?? throw ArgNullEx(nameof(@this))).Select(x => $"{x.Tag} - {x.Description}"));
    }
}
