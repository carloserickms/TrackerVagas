
using App.DTOs;
using App.Models;

namespace App.Helper.Builders
{
    public class AuthResponseBuilder : ResponseBase<User>, IResponseBuilder<User>
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

        public override ResponseDTO OK(object date, string message)
        {
            ResponseDTO response = new()
            {
                Message = message,
                Success = true,
                Date = date
            };

            return response;
        }
    }
}