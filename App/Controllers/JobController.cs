using App.DTOs;
using App.Models;
using App.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class JobController : Controller
    {
        private readonly JobService _jobService;

        public JobController(JobService jobService)
        {
            _jobService = jobService;
        }


        [HttpPost("create-job")]
        [Authorize]
        public async Task<ActionResult> CreateJob([FromBody] JobDTO jobDTO)
        {
            try
            {
                var tokenUserID = User.FindFirst("userId")?.Value;

                if (tokenUserID == null)
                {
                    return BadRequest("Usuario sem autorização");
                }

                var userId = Guid.Parse(tokenUserID);

                jobDTO.UserId = userId;
                var response = await _jobService.Add(jobDTO);

                return response.Success ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }

        [HttpDelete("delete-job")]
        [Authorize]
        public async Task<ActionResult> DeleteJob([FromBody] SearchByIdDTO searchByIdDTO)
        {
            try
            {
                var tokenUserID = User.FindFirst("userId")?.Value;

                if (tokenUserID == null)
                {
                    return BadRequest("Usuario sem autorização");
                }

                var userId = Guid.Parse(tokenUserID);
                var response = await _jobService.Delete(searchByIdDTO);

                return response.Success ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }

        [HttpGet("GetAll-jobs")]
        [Authorize]
        public async Task<ActionResult> GetAllById([FromBody] SearchByIdDTO searchByIdDTO)
        {
            try
            {
                var tokenUserID = User.FindFirst("userId")?.Value;

                if (tokenUserID == null)
                {
                    return BadRequest("Usuario sem autorização");
                }

                var userId = Guid.Parse(tokenUserID);
                var response = await _jobService.GetAllById(searchByIdDTO);

                return response.Success ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }
    }
}