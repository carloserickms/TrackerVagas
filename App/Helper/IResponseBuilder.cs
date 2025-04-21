using App.DTOs;

namespace App.Helper
{
    public interface IResponseBuilder<T>
    {
        ResponseDTO OK(object date, string message);
        ResponseDTO Conflict(string message);
        ResponseDTO NotFound(string message);
        ResponseDTO InternalError(string message);
    }   
}