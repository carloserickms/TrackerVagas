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
        private readonly IMetaInfoRepository _metaInfoRepository;
        private readonly IResponseBuilder _responseBuilder;

        public AuthService(IUserRepository userRepository, ISessionRepository sessionRepository,
                IResponseBuilder responseBuilder, IMetaInfoRepository metaInfoRepository)
        {
            _userRepository = userRepository;
            _responseBuilder = responseBuilder;
            _sessionRepository = sessionRepository;
            _metaInfoRepository = metaInfoRepository;
        }


        public async Task<ResponseDTO> SingUp(UserDTO userDTO)
        {
            try
            {
                var existingUser = await _userRepository.GetByEmail(userDTO.email);

                if (existingUser != null)
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
                return _responseBuilder.InternalError($"Ocorreu um erro interno: {ex.Message}");
            }
        }


        public async Task<ResponseDTO> SingIn(UserSignInDTO userDTO)
        {
            Session session = new() { };
            UserLoginDTO userLogin = new();

            try
            {
                var user = await _userRepository.GetByEmail(userDTO.email);

                Console.WriteLine($">>>>>>> {user}");

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
                return _responseBuilder.InternalError($"Ocorreu um erro interno: {ex.Message}");
            }
        }

        public async Task<ResponseDTO> LoginWithGoogle(UserDTOGoogle userDTO)
        {
            try
            {
                if (userDTO.provider != "google")
                {
                    return _responseBuilder.Conflict("Provider não é reconhecido");
                }

                var existingUser = await _userRepository.GetByEmail(userDTO.email);

                if (existingUser == null)
                {
                    var newUser = new User
                    {
                        UserName = userDTO.userName,
                        Email = userDTO.email,
                        Password = userDTO.password
                    };

                    await _userRepository.Add(newUser);

                    var token = TokenService.GenerateToken(newUser);

                    var session = new Session
                    {
                        UserId = newUser.Id,
                        Token = token
                    };

                    await _sessionRepository.Add(session);

                    var metaInfo = new MetaInfo(userDTO.providerId, userDTO.provider)
                    {
                        UserId = newUser.Id
                    };

                    await _metaInfoRepository.Add(metaInfo);

                    var userLogin = new UserLoginDTO
                    {
                        Id = newUser.Id,
                        Email = newUser.Email,
                        UserName = newUser.UserName,
                        Session = session,
                        CreatedAt = newUser.CreatedAt,
                        UpdatedAt = newUser.UpdatedAt
                    };

                    return _responseBuilder.OK(userLogin, "Usuário criado com sucesso!");
                }
                else
                {
                    var existingSession = await _sessionRepository.GetById(existingUser.Id);

                    Console.WriteLine("SESSION:", existingSession);

                    if (existingSession == null)
                    {
                        var token = TokenService.GenerateToken(existingUser);

                        var session = new Session
                        {
                            UserId = existingUser.Id,
                            Token = token
                        };

                        await _sessionRepository.Add(session);

                        existingUser.Session = session;
                    }

                    var userLogin = new UserLoginDTO
                    {
                        Id = existingUser.Id,
                        Email = existingUser.Email,
                        UserName = existingUser.UserName,
                        Session = existingUser.Session,
                        CreatedAt = existingUser.CreatedAt,
                        UpdatedAt = existingUser.UpdatedAt
                    };

                    return _responseBuilder.OK(userLogin, "Usuário já existe");
                }
            }
            catch (Exception ex)
            {
                return _responseBuilder.InternalError($"Ocorreu um erro interno: {ex.Message}");
            }
        }

        public async Task<ResponseDTO> LogOutAccount(Guid userId)
        {
            try
            {
                var session = await _sessionRepository.GetById(userId);

                if (session == null)
                {
                    return _responseBuilder.Conflict("Sessão não existe!");
                }

                await _sessionRepository.Delete(session);

                return _responseBuilder.OKNoObject("Usuário deslogado do sistema com sucesso!");
            }
            catch (Exception ex)
            {
                return _responseBuilder.InternalError($"Ocorreu um erro interno: {ex.Message}");
            }
        }
    }
}
