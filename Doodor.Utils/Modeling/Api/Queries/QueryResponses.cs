using System;

namespace Doodor.Utils.Utilities.Modeling.Api.Queries
{
    public static class QueryResponses
    {
        public static T OK<T>(Action<T> act = null)
            where T : QueryResponseDto, new() =>
                CreateResponse(QueryStatus.OK, act);


        public static T BadRequest<T>(
            BadRequestDto badReqReason = null, Action<T> act = null)
                where T : QueryResponseDto, new()
        {
            var resp = CreateResponse(QueryStatus.BadRequest, act);
            resp.BadRequestReason = badReqReason;
            return resp;
        }

        public static T Unauthorized<T>(
            BadRequestDto badReqReason = null, Action<T> act = null)
                where T : QueryResponseDto, new()
        {
            var resp = CreateResponse(QueryStatus.Unauthorized, act);
            resp.BadRequestReason = badReqReason;
            return resp;
        }

        public static T NotFound<T>(
            BadRequestDto badReqReason = null, Action<T> act = null)
                where T : QueryResponseDto, new()
        {
            var resp = CreateResponse(QueryStatus.NotFound, act);
            resp.BadRequestReason = badReqReason;
            return resp;
        }

        private static T CreateResponse<T>(
            QueryStatus status, Action<T> act = null)
                where T : QueryResponseDto, new()
        {
            var resp = new T { Status = status };
            act?.Invoke(resp);
            return resp;
        }
    }
}