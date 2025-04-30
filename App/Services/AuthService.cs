using App.DTOs;
using App.Helper;
using App.Models;
using App.Repositories.Interfaces;

namespace App.Service
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly IResponseBuilder _responseBuilder;

        public AuthService(IUserRepository userRepository, ISessionRepository sessionRepository,
                IResponseBuilder responseBuilder)
        {
            _userRepository = userRepository;
            _responseBuilder = responseBuilder;
            _sessionRepository = sessionRepository;
        }


        public async Task<ResponseDTO> SingUp(UserDTO userDTO)
        {
            try
            {
                var user = await _userRepository.GetByEmail(userDTO.email);

                if (user != null)
                {
                    return _responseBuilder.Conflict("O email inserido já possui cadastro.");
                }

                if (userDTO.password != userDTO.rePassword)
                {
                    return _responseBuilder.Conflict("As senhas inseridas não coincidem, verifique e tente novamente.");
                }

                User newUser = new()
                {
                    UserName = userDTO.userName,
                    Email = userDTO.email,
                    Password = userDTO.password
                };

                await _userRepository.Add(newUser);

                return _responseBuilder.OKNoObject("Cadastro feito com sucesso.");
            }
            catch (Exception ex)
            {
                return _responseBuilder.InternalError( $"Ocorreu um erro interno: {ex.Message}");
            }
        }

        public async Task<ResponseDTO> SingIn(UserSignInDTO userDTO)
        {
            Session session = new() { };
            UserLoginDTO userLogin = new();

            try
            {
                var user = await _userRepository.GetByEmail(userDTO.email);


                if (user == null)
                {
                    return _responseBuilder.NotFound("Email enserido não encontrado, Verifique e tente novamente.");
                }

                if (!BCrypt.Net.BCrypt.Verify(userDTO.password, user.Password))
                {
                    return _responseBuilder.Conflict("Senha invalida");
                }

                var token = TokenService.GenerateToken(user);

                var hasSession = await _sessionRepository.GetById(user.Id);

                if (hasSession == null)
                {
                    session = new()
                    {
                        UserId = user.Id,
                        Token = token
                    };

                    await _sessionRepository.Add(session);

                    userLogin = new()
                    {
                        Id = user.Id,
                        Email = user.Email,
                        UserName = user.UserName,
                        Session = user.Session,
                        CreatedAt = user.CreatedAt,
                        UpdatedAt = user.UpdatedAt
                    };

                    return _responseBuilder.OK(userLogin, "Usuário foi logado com sucesso!");
                }

            return _responseBuilder.OK(user, "Usuário foi logado com sucesso!");
            
            }
            catch (Exception ex)
            {
                return _responseBuilder.InternalError( $"Ocorreu um erro interno: {ex.Message}");
            }
        }
    }
}