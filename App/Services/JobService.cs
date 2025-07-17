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
        private readonly IResponseBuilder _responseBuilder;

        public JobService(IJobRepository jobRepository, IUserRepository userRepository, IResponseBuilder responseBuilder)
        {
            _jobRepository = jobRepository;
            _userRepository = userRepository;
            _responseBuilder = responseBuilder;
        }


        public async Task<ResponseDTO> Add(JobDTO JobDTO)
        {
            try
            {
                ResponseDTO response = new();

                var user = await _userRepository.GetById(JobDTO.userId);
                var modality = await _jobRepository.GetModalityById(JobDTO.modality);
                var status = await _jobRepository.GetStatusById(JobDTO.status);

                if (user == null)
                {
                    return _responseBuilder.Conflict($"Nenhuma referencia do objeto usuario encontrada");
                }

                if (modality == null)
                {
                    return _responseBuilder.Conflict($"Nenhuma referencia do objeto modality encontrada");
                }

                if (status == null)
                {
                    return _responseBuilder.Conflict($"Nenhuma referencia do objeto status encontrada");
                }

                JobVacancy job = new()
                {
                    Title = JobDTO.title,
                    EnterpriseName = JobDTO.enterpriseName,
                    Link = JobDTO.link,
                    UserId = user.Id,
                    ModalityId = modality.Id,
                    VacancyStatusId = status.Id
                };

                await _jobRepository.Add(job);

                return _responseBuilder.OK(job, "Vaga adicionada com sucesso!");
            }
            catch (Exception ex)
            {
                return _responseBuilder.InternalError($"Ocorreu um erro interno: {ex.Message}");
            }
        }

        public async Task<ResponseDTO> Delete(UserActionDTO userAction)
        {
            try
            {
                var job = await _jobRepository.GetById(userAction.JobId);

                if (job == null)
                {
                    return _responseBuilder.Conflict("Vaga não encontrada!");
                }

                if (job.UserId != userAction.UserId)
                {
                    return _responseBuilder.Conflict("Usuário não possui permissão!");
                }

                await _jobRepository.Delete(job);

                return _responseBuilder.OKNoObject("Vaga deletada com sucesso!");
            }
            catch (Exception ex)
            {
                return _responseBuilder.InternalError($"Ocorreu um erro interno: {ex.Message}");
            }
        }

        public async Task<ResponseDTO> UpdateJob(UpdateJobDTO updateJobDTO)
        {
            try
            {
                var job = await _jobRepository.GetById(updateJobDTO.jobId);

                if (job == null)
                {
                    return _responseBuilder.Conflict("Vaga não encontrada!");
                }

                if (job.UserId != updateJobDTO.userId)
                {
                    return _responseBuilder.Conflict("Usuário não possui permissão!");
                }

                job.Title = updateJobDTO.title;
                job.Link = updateJobDTO.link;
                job.EnterpriseName = updateJobDTO.enterpriseName;
                job.ModalityId = updateJobDTO.modality;
                job.VacancyStatusId = updateJobDTO.status;
                job.UpdatedAt = DateTime.Now;

                await _jobRepository.Edit(job);

                return _responseBuilder.OK(job, "Vaga editada com sucesso!");

            }
            catch (Exception ex)
            {
                return _responseBuilder.InternalError($"Ocorreu um erro interno: {ex.Message}");
            }
        }

        public async Task<ResponseDTO> GetAllById(Guid searchById)
        {
            try
            {
                List<JobResponseDTO> jobslist = new List<JobResponseDTO>();

                var jobs = await _jobRepository.GetAllById(searchById);

                if (jobs == null)
                {
                    return _responseBuilder.NotFound("Nenhuma vaga foi encontrada!");
                }

                foreach (var item in jobs)
                {
                    var status = await _jobRepository.GetStatusById(item.VacancyStatusId);
                    var modality = await _jobRepository.GetModalityById(item.ModalityId);

                    jobslist.Add(new JobResponseDTO
                    {
                        id = item.Id,
                        title = item.Title,
                        link = item.Link,
                        enterpriseName = item.EnterpriseName,
                        status = status.Name,
                        modality = modality.Name,
                        createdAt = item.CreatedAt,
                        updatedAt = item.UpdatedAt
                    });
                }

                return _responseBuilder.OK(jobslist, "Todos as vagas foram encontradas");
            }
            catch (Exception ex)
            {
                return _responseBuilder.InternalError($"Ocorreu um erro interno: {ex.Message}");
            }
        }

        public async Task<ResponseDTO> GetJobById(UserActionDTO jobByIdDto)
        {
            try
            {
                var job = await _jobRepository.GetById(jobByIdDto.JobId);

                if (job == null)
                {
                    return _responseBuilder.NotFound("Nenhuma vaga foi encontrada!");
                }

                if (job.UserId != jobByIdDto.UserId)
                {
                    return _responseBuilder.Conflict("Usuário não possui permissão!");
                }

                var status = await _jobRepository.GetStatusById(job.VacancyStatusId);
                var modality = await _jobRepository.GetModalityById(job.ModalityId);

                JobResponseDTO jobInfo = new()
                {
                    id = job.Id,
                    title = job.Title,
                    link = job.Link,
                    enterpriseName = job.EnterpriseName,
                    status = status.Name,
                    modality = modality.Name,
                    createdAt = job.CreatedAt,
                    updatedAt = job.UpdatedAt
                };


                return _responseBuilder.OK(jobInfo, "Vaga encontrada com sucesso!");
            }
            catch (Exception ex)
            {
                return _responseBuilder.InternalError($"Ocorreu um erro interno: {ex.Message}");
            }
        }

        public async Task<ResponseDTO> CreateStatus(TypesDTO typesDTO)
        {
            try
            {
                var statusList = await _jobRepository.AllStatus();

                VacancyStatus status = new()
                {
                    Name = typesDTO.name
                };

                await _jobRepository.AddVacacyStatus(status);

                return _responseBuilder.OK(status, "Modalidade criada com sucesso!");
            }
            catch (Exception ex)
            {
                return _responseBuilder.InternalError($"Ocorreu um erro interno: {ex.Message}");
            }
        }

        public async Task<ResponseDTO> GetAllStatus()
        {
            try
            {
                List<AllStatusResponseDTO> allStatus = new List<AllStatusResponseDTO>();

                var status = await _jobRepository.AllStatus();

                if (status == null)
                {
                    return _responseBuilder.NotFound("Nenhum dado foi encontrado!");
                }

                foreach (var item in status)
                {
                    allStatus.Add(new AllStatusResponseDTO
                    {
                        id = item.Id,
                        name = item.Name
                    });
                }

                return _responseBuilder.OK(allStatus, "Dados encontrados com sucesso!");
            }
            catch (Exception ex)
            {
                return _responseBuilder.InternalError($"Ocorreu um erro interno: {ex.Message}");
            }
        }

        public async Task<ResponseDTO> CreateModality(TypesDTO typesDTO)
        {
            try
            {
                var modalityList = await _jobRepository.AllModality();

                Modality modality = new()
                {
                    Name = typesDTO.name
                };

                await _jobRepository.AddModality(modality);

                return _responseBuilder.OK(modality, "Modalidade criada com sucesso!");
            }
            catch (Exception ex)
            {
                return _responseBuilder.InternalError($"Ocorreu um erro interno: {ex.Message}");
            }
        }

        public async Task<ResponseDTO> GetAllModality()
        {
            try
            {
                List<AllModalityResponseDTO> allModality = new List<AllModalityResponseDTO>();

                var modalityList = await _jobRepository.AllModality();

                if (modalityList == null)
                {
                    return _responseBuilder.NotFound("Nenhum dado foi encontrado!");
                }

                foreach (var item in modalityList)
                {
                    allModality.Add(new AllModalityResponseDTO
                    {
                        id = item.Id,
                        name = item.Name
                    });
                }

                return _responseBuilder.OK(allModality, "Dados encontrados com sucesso!");
            }
            catch (Exception ex)
            {
                return _responseBuilder.InternalError($"Ocorreu um erro interno: {ex.Message}");
            }
        }
    }
}