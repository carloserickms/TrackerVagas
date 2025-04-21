using App.DTOs;
using App.Models;

namespace App.Helper.Builders
{
    public class JobResponseBuilder : IResponseBuilder<JobVacancy>
    {
        public ResponseDTO Conflict(string message)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO InternalError(string message)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO NotFound(string message)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO OK(object  date, string message)
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