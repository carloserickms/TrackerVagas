using App.DTOs;
using App.Helper;
using App.Models;
using App.Repositories.Interfaces;


namespace App.Service
{
    public class JobService
    {
        private readonly IJobRepository _jobRepository;
        private readonly IUserRepository _userRepository;
        private readonly IResponseBuilder<JobVacancy> _responseBuilder;

        public JobService (IJobRepository jobRepository, IUserRepository userRepository)
        {
            _jobRepository = jobRepository;
            _userRepository = userRepository;
        }

        public async Task<ResponseDTO> Add(JobDTO JobDTO)
        {
            try
            {
                ResponseDTO response = new();   
                
                var user = _userRepository.GetById(JobDTO.UserId);

                if (user== null)
                {
                    return _responseBuilder.NotFound("Usuario não encontrado!");
                }

                JobVacancy job = new()
                {
                    Title = JobDTO.Title,
                    EnterpriseName = JobDTO.EnterpriseName,
                    Link = JobDTO.Link,
                    Modality = JobDTO.Modality,
                    VacancyStatusId = JobDTO.Status,
                    UserId = JobDTO.UserId
                };

                await _jobRepository.Add(job);

                return _responseBuilder.OK(job, "Vaga adicionada com sucesso!");
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

        public async Task<ResponseDTO> Delete(SearchByIdDTO searchByIdDTO)
        {
            try
            {
                var job = await _jobRepository.GetById(searchByIdDTO.Id);

                if(job == null)
                {
                    return _responseBuilder.Conflict("Vaga não encontrada!");
                }

                await _jobRepository.Delete(job);

                return _responseBuilder.OK(null, "Vaga deletada com sucesso!");
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

        public async Task<ResponseDTO> GetAllById(SearchByIdDTO searchByIdDTO)
        {
            try
            {
                var jobs = await _jobRepository.GetAllById(searchByIdDTO.Id);

                if(jobs == null)
                {
                    return _responseBuilder.NotFound("Nenhuma vaga foi encontrada!");
                }

                return _responseBuilder.OK(jobs, "Todos as vagas foram encontradas");
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