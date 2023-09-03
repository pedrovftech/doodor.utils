using System;
using System.Collections.Generic;
using System.Linq;
using static Doodor.Utils.ExceptionHandling.Exceptions;

namespace Doodor.Utils.Utilities.Collections
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> Batches<T>(this IEnumerable<T> @this, int maxSize)
        {
            if (@this == null)
                throw ArgNullEx(nameof(@this));

            var enumerator = @this.GetEnumerator();
            var batch = new T[maxSize];
            var batchIndex = 0;
            var actualBatchLength = 0;

            while (enumerator.MoveNext())
            {
                batch[batchIndex++] = enumerator.Current;
                actualBatchLength++;

                if (batchIndex != maxSize) continue;
                else yield return batch;

                batch = new T[maxSize];
                batchIndex = 0;
                actualBatchLength = 0;
            }

            var ajustedLastBatch = batch.Take(actualBatchLength).ToArray();

            if (ajustedLastBatch.Any())
                yield return ajustedLastBatch;
        }

        public static void ForEach<T>(this IEnumerable<T> @this, Action<T> act)
        {
            if (@this == null) throw ArgNullEx(nameof(@this));
            if (act == null) throw ArgNullEx(nameof(act));

            foreach (var item in @this) act(item);
        }

        public static IReadOnlyList<T> ToReadOnlyList<T>(this IEnumerable<T> @this) =>
            (@this ?? throw ArgNullEx(nameof(@this))).ToList();


        public static IReadOnlyList<T> ToReadOnlyList<T>(this IList<T> @this) =>
            (IReadOnlyList<T>)(@this ?? throw ArgNullEx(nameof(@this)));


        public static IReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> @this) =>
            ToReadOnlyList(@this);


        public static IReadOnlyCollection<T> ToReadOnlyCollection<T>(this IList<T> @this) =>
            (IReadOnlyCollection<T>)(@this ?? throw ArgNullEx(nameof(@this)));
    }
}