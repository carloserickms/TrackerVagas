using App.DTOs;
using App.Helper;
using App.Models;
using App.Repositories.Interfaces;

namespace App.Service
{
    public class UserProfileService
    {
        private readonly IUserRepository _userRepository;
        private readonly IResponseBuilder _responseBuilder;
        public UserProfileService(IUserRepository userRepository, IResponseBuilder responseBuilder)
        {
            _userRepository = userRepository;
            _responseBuilder = responseBuilder;
        }

        
        public async Task<ResponseDTO> DeleteUser(Guid id)
        {
            ResponseDTO response = new();

            try
            {
                var user = await _userRepository.GetById(id);

                if (user == null)
                {
                    return new ResponseDTO
                    {
                        Success = false,
                        Message = "Usuario não encontrado!",
                        Data = null
                    };
                }

                await _userRepository.Delete(user);

                return new ResponseDTO
                {
                    Success = true,
                    Message = "Usuario deletado com sucesso!",
                    Data = user
                };
            }
            catch (Exception ex)
            {
                return _responseBuilder.InternalError( $"Ocorreu um erro interno: {ex.Message}");
            }
        }

        public async Task<ResponseDTO> ChangeUserInformation(Guid id, ChengeAccountDTO chengeAccountDTO)
        {

            try
            {
                var user = await _userRepository.GetById(id);

                if (user == null)
                {
                    return _responseBuilder.NotFound("Usuario não encontrado, Verifique e tente novamente.");
                }

                if (chengeAccountDTO.Password != chengeAccountDTO.RePassword)
                {
                    return _responseBuilder.Conflict("As senhas inseridas não coincidem, verifique e tente novamente.");
                }

                user.UserName = chengeAccountDTO.UserName;
                user.Password = chengeAccountDTO.Password;

                await _userRepository.Change();

                return _responseBuilder.OKNoObject("Informações alteradas com sucesso!");
            }
            catch (Exception ex)
            {
                return _responseBuilder.InternalError( $"Ocorreu um erro interno: {ex.Message}");
            }
        }

        public async Task<ResponseDTO> UserById(Guid id)
        {
            try
            {
                var user = await _userRepository.GetById(id);

                if (user == null)
                {
                    return _responseBuilder.NotFound("Usuario não encontrado, Verifique e tente novamente.");
                }

                return _responseBuilder.OK(user, "Informações encontradas com sucesso!");

            }
            catch (Exception ex)
            {
                return _responseBuilder.InternalError( $"Ocorreu um erro interno: {ex.Message}");
            }
        }
    }
}