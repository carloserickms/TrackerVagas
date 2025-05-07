using App.DTOs;

namespace App.Helper
{
    public interface IResponseBuilder : IResponseBuilderOk
    {
        ResponseDTO Conflict(string message);
        ResponseDTO NotFound(string message);
        ResponseDTO InternalError(string message);
        ResponseDTO OKNoObject(string message);
    }   
}