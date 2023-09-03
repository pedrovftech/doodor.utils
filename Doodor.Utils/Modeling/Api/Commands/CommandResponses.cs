using System;

namespace Doodor.Utils.Utilities.Modeling.Api.Commands
{
    public static class CommandResponses
    {
        public static T OK<T>(Action<T> act = null)
            where T : CommandResponseDto, new() =>
                CreateResponse(CommandStatus.OK, act);


        public static T Created<T>(Action<T> act = null)
            where T : CommandResponseDto, new() =>
                CreateResponse(CommandStatus.Created, act);


        public static T Accepted<T>(Action<T> act = null)
            where T : CommandResponseDto, new() =>
                CreateResponse(CommandStatus.Accepted, act);


        public static T NoContent<T>(Action<T> act = null)
            where T : CommandResponseDto, new() =>
                CreateResponse(CommandStatus.NoContent, act);


        public static T BadRequest<T>(
            BadRequestDto badReqReason = null, Action<T> act = null)
                where T : CommandResponseDto, new()
        {
            var resp = CreateResponse(CommandStatus.BadRequest, act);
            resp.BadRequestReason = badReqReason;
            return resp;
        }

        public static T Unauthorized<T>(
            BadRequestDto badReqReason = null, Action<T> act = null)
                where T : CommandResponseDto, new()
        {
            var resp = CreateResponse(CommandStatus.Unauthorized, act);
            resp.BadRequestReason = badReqReason;
            return resp;
        }

        public static T NotFound<T>(
            BadRequestDto badReqReason = null, Action<T> act = null)
                where T : CommandResponseDto, new()
        {
            var resp = CreateResponse(CommandStatus.NotFound, act);
            resp.BadRequestReason = badReqReason;
            return resp;
        }

        private static T CreateResponse<T>(
            CommandStatus status, Action<T> act = null)
                where T : CommandResponseDto, new()
        {
            var resp = new T { Status = status };
            act?.Invoke(resp);
            return resp;
        }
    }
}