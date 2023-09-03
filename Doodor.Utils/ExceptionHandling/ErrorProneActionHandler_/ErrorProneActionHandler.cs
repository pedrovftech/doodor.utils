using Doodor.Utils.Utilities.ExceptionHandling.ErrorProneActionHandler_;
using System;
using System.Threading.Tasks;
using static Doodor.Utils.ExceptionHandling.Exceptions;

namespace Doodor.Utils.Utilities.ExceptionHandling
{
    public static class ErrorProneActionHandler
    {
        public static TryResult Try(Action act)
        {
            if (act == null) throw ArgNullEx(nameof(act));

            var succeeded = false;
            Exception error = null;

            try
            {
                act();
                succeeded = true;
            }
            catch (Exception ex)
            {
                error = ex;
            }

            return new TryResult(succeeded, error);
        }

        public static TryResult<T> Try<T>(Func<T> act)
        {
            if (act == null) throw ArgNullEx(nameof(act));

            var succeeded = false;
            var value = default(T);
            Exception error = null;

            try
            {
                value = act();
                succeeded = true;
            }
            catch (Exception ex)
            {
                error = ex;
            }

            return new TryResult<T>(succeeded, value, error);
        }

        public static async Task<TryResult<T>> TryAsync<T>(Func<Task<T>> act)
        {
            if (act == null) throw ArgNullEx(nameof(act));

            var succeeded = false;
            var value = default(T);
            Exception error = null;

            try
            {
                value = await act();
                succeeded = true;
            }
            catch (Exception ex)
            {
                error = ex;
            }

            return new TryResult<T>(succeeded, value, error);
        }

        public static async Task<TryResult<T>> TryAsync<T>(Func<ValueTask<T>> act)
        {
            if (act == null) throw ArgNullEx(nameof(act));

            var succeeded = false;
            var value = default(T);
            Exception error = null;

            try
            {
                value = await act();
                succeeded = true;
            }
            catch (Exception ex)
            {
                error = ex;
            }

            return new TryResult<T>(succeeded, value, error);
        }

        public static async Task<TryResult> TryAsync(Func<Task> act)
        {
            if (act == null) throw ArgNullEx(nameof(act));

            var succeeded = false;
            Exception error = null;

            try
            {
                await act();
                succeeded = true;
            }
            catch (Exception ex)
            {
                error = ex;
            }

            return new TryResult(succeeded, error);
        }

        public static async Task<TryResult> TryAsync(Func<ValueTask> act)
        {
            if (act == null) throw ArgNullEx(nameof(act));

            var succeeded = false;
            Exception error = null;

            try
            {
                await act();
                succeeded = true;
            }
            catch (Exception ex)
            {
                error = ex;
            }

            return new TryResult(succeeded, error);
        }
    }
}