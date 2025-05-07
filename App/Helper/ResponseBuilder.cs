using App.DTOs;

namespace App.Helper
{
    public class ResponseBuilder : ResponseBase
    {
        public override ResponseDTO Conflict(string message)
        {
            ResponseDTO response = new()
            {
                Message = message,
                Success = false,
            };

            return response;
        }

        public override ResponseDTO InternalError(string message)
        {
            ResponseDTO response = new()
            {
                Message = message,
                Success = false,
            };

            return response;
        }

        public override ResponseDTO NotFound(string message)
        {
            ResponseDTO response = new()
            {
                Message = message,
                Success = false,
            };

            return response;
        }

        public override ResponseDTO OK<T>(T data, string message)
        {
            ResponseDTO response = new()
            {
                Message = message,
                Success = true,
                Data = data
            };

            return response;
        }

        public override ResponseDTO OKNoObject(string message)
        {
            ResponseDTO response = new()
            {
                Message = message,
                Success = true,
            };

            return response;
        }
    }
}