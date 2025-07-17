using App.DTOs;
using App.Models;
using App.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace App.Controllers
{
    [ApiController]
    [Route("api/v1")]
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
                var tokenUserID = User.FindFirst("UserId")?.Value;

                if (tokenUserID == null)
                {
                    return BadRequest($"Usuario sem autorização {tokenUserID}");
                }

                var userId = Guid.Parse(tokenUserID);

                jobDTO.userId = userId;
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
        public async Task<ActionResult> DeleteJob([FromQuery] Guid jobId)
        {
            try
            {
                var tokenUserID = User.FindFirst("UserId")?.Value;

                if (tokenUserID == null)
                {
                    return BadRequest("Usuario sem autorização");
                }

                var userId = Guid.Parse(tokenUserID);

                Console.WriteLine(userId);

                UserActionDTO userAction = new()
                {
                    JobId = jobId,
                    UserId = userId
                };

                var response = await _jobService.Delete(userAction);

                return response.Success ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }

        [HttpPut("update-job")]
        [Authorize]
        public async Task<ActionResult> UpdateJob([FromBody] UpdateJobDTO updateJobDTO)
        {
            try
            {
                var tokenUserID = User.FindFirst("UserId")?.Value;

                if (tokenUserID == null)
                {
                    return BadRequest($"Usuario sem autorização {tokenUserID}");
                }

                var userId = Guid.Parse(tokenUserID);
                updateJobDTO.userId = userId;

                var response = await _jobService.UpdateJob(updateJobDTO);

                return response.Success ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }


        [HttpGet("get-all-jobs")]
        [Authorize]
        public async Task<ActionResult> GetAllById()
        {
            try
            {
                var tokenUserID = User.FindFirst("UserId")?.Value;

                if (tokenUserID == null)
                {
                    return BadRequest("Usuario sem autorização");
                }

                var userId = Guid.Parse(tokenUserID);

                var response = await _jobService.GetAllById(userId);

                return response.Success ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }

        [HttpGet("get-job-byId")]
        [Authorize]
        public async Task<ActionResult> GetJobById([FromQuery] Guid jobId)
        {
            try
            {
                Console.WriteLine(jobId);

                var tokenUserID = User.FindFirst("UserId")?.Value;

                if (tokenUserID == null)
                {
                    return BadRequest("Usuario sem autorização");
                }

                var userId = Guid.Parse(tokenUserID);

                UserActionDTO JobUserID = new UserActionDTO
                {
                    JobId = jobId,
                    UserId = userId
                };

                var response = await _jobService.GetJobById(JobUserID);

                return response.Success ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }

        [HttpGet("get-all-status")]
        [Authorize]
        public async Task<ActionResult> GetAllStatus()
        {
            try
            {
                var response = await _jobService.GetAllStatus();

                return response.Success ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }

        [HttpPost("create-status")]
        [Authorize]
        public async Task<ActionResult> CreateStatus([FromBody] TypesDTO typesDTO)
        {
            try
            {
                var response = await _jobService.CreateStatus(typesDTO);

                return response.Success ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }

        [HttpGet("get-all-modality")]
        [Authorize]
        public async Task<ActionResult> GetAllModality()
        {
            try
            {
                var response = await _jobService.GetAllModality();

                return response.Success ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }

        [HttpPost("create-modality")]
        [Authorize]
        public async Task<ActionResult> CreateModality([FromBody] TypesDTO typesDTO)
        {
            try
            {
                var response = await _jobService.CreateModality(typesDTO);

                return response.Success ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }
    }
}