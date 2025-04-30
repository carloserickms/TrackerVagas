using App.DTOs;

namespace App.Helper
{
    public abstract class ResponseBase : IResponseBuilder
    {
        public abstract ResponseDTO Conflict(string message);

        public abstract ResponseDTO InternalError(string message);

        public abstract ResponseDTO NotFound(string message);

        public abstract ResponseDTO OK<T>(T data, string message);

        public abstract ResponseDTO OKNoObject(string message);
    }
}