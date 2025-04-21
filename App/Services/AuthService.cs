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
        private readonly IResponseBuilder<UserLoginDTO> _responseBuilderLogin;
        private readonly IResponseBuilder<User> _responseBuilderAuth;

        public AuthService(IUserRepository userRepository, ISessionRepository sessionRepository,
                IResponseBuilder<UserLoginDTO> responseBuilderLogin, IResponseBuilder<User> responseBuilderAuth)
        {
            _userRepository = userRepository;
            _responseBuilderLogin = responseBuilderLogin;
            _sessionRepository = sessionRepository;
            _responseBuilderAuth = responseBuilderAuth;
        }


        public async Task<ResponseDTO> SingUp(UserDTO userDTO)
        {
            try
            {
                var user = await _userRepository.GetByEmail(userDTO.Email);

                if (user != null)
                {
                    return _responseBuilderAuth.Conflict("O email inserido já possui cadastro.");
                }

                if (userDTO.Password != userDTO.RePassword)
                {
                    return _responseBuilderAuth.Conflict("As senhas inseridas não coincidem, verifique e tente novamente.");
                }

                User newUser = new()
                {
                    UserName = userDTO.UserName,
                    Email = userDTO.Email,
                    Password = userDTO.Password
                };

                await _userRepository.Add(newUser);

                return _responseBuilderAuth.OK(newUser, "Cadastro feito com sucesso.");
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    Success = false,
                    Message = $"Ocorreu um erro interno: {ex.Message}",
                    Date = null
                };
            }
        }

        public async Task<ResponseDTO> SingIn(UserDTO userDTO)
        {
            Session session = new() { };
            UserLoginDTO userLogin = new();

            try
            {
                var user = await _userRepository.GetByEmail(userDTO.Email);

                if (user == null)
                {
                    return _responseBuilderLogin.NotFound("Email enserido não encontrado, Verifique e tente novamente.");
                }

                if (userDTO.Password != userDTO.RePassword)
                {
                    return _responseBuilderLogin.Conflict("As senhas inseridas não coincidem, verifique e tente novamente.");
                }

                var token = TokenService.GenerateToken(user);

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

                return _responseBuilderLogin.OK(userLogin, "Usuário foi logado com sucesso!");
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    Success = false,
                    Message = $"Ocorreu um erro interno: {ex.Message}",
                    Date = null
                };
            }
        }
    }
}