using App.DTOs;
using App.Models;
using App.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [ApiController]
    [Route("Api/v1")]
    public class UserController : Controller
    {
        private readonly UserProfileService _userProfileService;
        private readonly AuthService _authService;

        public UserController(AuthService authService, UserProfileService userProfileService)
        {
            _authService = authService;
            _userProfileService = userProfileService;
        }

        [HttpPost("create-account")]
        public async Task<ActionResult> CreateAccount([FromBody] UserDTO userDTO)
        {
            try
            {
                var response = await _authService.SingUp(userDTO);

                return response.Success ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }

        [HttpPost("login-account")]
        public async Task<ActionResult> LoginAccount([FromBody] UserSignInDTO userDTO)
        {
            try
            {
                var response = await _authService.SingIn(userDTO);

                return response.Success ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }

        [HttpDelete("delete-account")]
        [Authorize]
        public async Task<ActionResult> DeleteAccount()
        {
            try
            {
                var tokenUserID = User.FindFirst("UserId")?.Value;

                if (tokenUserID == null)
                {
                    return BadRequest("Usuario sem autorização");
                }

                var userId = Guid.Parse(tokenUserID);
                var response = await _userProfileService.DeleteUser(userId);

                return response.Success ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }

        [HttpPut("change-information")]
        [Authorize]
        public async Task<ActionResult> ChangeUserInformation([FromBody] ChengeAccountDTO chengeAccountDTO)
        {
            try
            {
                var tokenUserID = User.FindFirst("UserId")?.Value;

                if(tokenUserID == null)
                {
                    return BadRequest("Usuario sem autorização");
                }

                var userId = Guid.Parse(tokenUserID);
                var response = await _userProfileService.ChangeUserInformation(userId ,chengeAccountDTO);

                return response.Success ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }
    }
}