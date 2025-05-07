using App.DTOs;

namespace App.Helper
{
    public interface IResponseBuilderOk
    {
        ResponseDTO OK<T>(T data, string message);
    }
}