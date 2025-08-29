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
                var typeOfContract = JobDTO.typeOfContract.HasValue ? await _jobRepository.GetTypeOfContract(JobDTO.typeOfContract.Value) : null;
                var interestLevel = JobDTO.interestLevel.HasValue ? await _jobRepository.GetInterestLevel(JobDTO.interestLevel.Value) : null;

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
                    VacancyStatusId = status.Id,
                    TypeOfContractId = typeOfContract?.Id,
                    InterestLevelId = interestLevel?.Id,
                    Salary = JobDTO.salary,
                    Location = JobDTO.location,
                    Workload = JobDTO.workload
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
                job.Salary = updateJobDTO.salary;
                job.Location = updateJobDTO.location;
                job.Workload = updateJobDTO.workload;
                job.ModalityId = updateJobDTO.modality;
                job.VacancyStatusId = updateJobDTO.status;
                job.InterestLevelId = updateJobDTO.interestLevel;
                job.TypeOfContractId = updateJobDTO.typeOfContract;
                job.UpdatedAt = DateTime.Now;

                await _jobRepository.Edit(job);

                return _responseBuilder.OK(job, "Vaga editada com sucesso!");

            }
            catch (Exception ex)
            {
                return _responseBuilder.InternalError($"Ocorreu um erro interno: {ex.Message}");
            }
        }

        public async Task<ResponseDTO> GetAllById(UserPageRequestDTO userPage)
        {
            try
            {
                List<JobResponseDTO> jobslist = new List<JobResponseDTO>();

                var jobs = await _jobRepository.GetAllById(userPage);

                if (jobs == null)
                {
                    return _responseBuilder.NotFound("Nenhuma vaga foi encontrada!");
                }

                foreach (var item in jobs)
                {
                    var status = await _jobRepository.GetStatusById(item.VacancyStatusId);
                    var modality = await _jobRepository.GetModalityById(item.ModalityId);
                    var typeOfContract = item.TypeOfContractId.HasValue
                        ? await _jobRepository.GetTypeOfContract(item.TypeOfContractId.Value)
                        : null;
                    var interestLevel = item.InterestLevelId.HasValue
                        ? await _jobRepository.GetInterestLevel(item.InterestLevelId.Value)
                        : null;

                    jobslist.Add(new JobResponseDTO
                    {
                        id = item.Id,
                        title = item.Title,
                        link = item.Link,
                        enterpriseName = item.EnterpriseName,
                        location = item.Location,
                        salary = item.Salary,
                        workload = item.Workload,
                        typeOfContract = typeOfContract?.Name,
                        typeOfContractId = typeOfContract?.Id,
                        interestLevel = interestLevel?.Name,
                        interestLevelId = interestLevel?.Id,
                        status = status.Name,
                        statusId = status.Id,
                        modality = modality.Name,
                        modalityId = modality.Id,
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
                var typeOfContract = job.TypeOfContractId.HasValue ? await _jobRepository.GetTypeOfContract(job.TypeOfContractId.Value) : null;
                var interestLevel = job.InterestLevelId.HasValue ? await _jobRepository.GetInterestLevel(job.InterestLevelId.Value) : null;

                JobResponseByIdDTO jobInfo = new()
                {
                    id = job.Id,
                    title = job.Title,
                    link = job.Link,
                    enterpriseName = job.EnterpriseName,
                    location = job?.Location,
                    salary = job?.Salary,
                    workload = job?.Workload,
                    status = status.Id,
                    modality = modality.Id,
                    typeOfContract = typeOfContract?.Id,
                    interestLevel = interestLevel?.Id,
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

        public async Task<ResponseDTO> GetJobByTitle(SearchForUserJobs search)
        {
            try
            {
                List<JobResponseDTO> jobslist = new List<JobResponseDTO>();

                var jobs = await _jobRepository.GetJobByTitle(search);

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

                return _responseBuilder.OK(jobslist, "Vagas encontradas com sucesso!");
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

        public async Task<ResponseDTO> GetModalityById(ModalityIdUserIdRequestDTO modalityIdUserId)
        {
            try
            {
                List<JobResponseDTO> jobList = new List<JobResponseDTO>();

                var jobs = await _jobRepository.GetJobByModality(modalityIdUserId);

                if (jobs == null)
                {
                    return _responseBuilder.NotFound("Nenhum dado foi encontrado!");
                }

                foreach (var item in jobs)
                {
                    var status = await _jobRepository.GetStatusById(item.VacancyStatusId);
                    var modality = await _jobRepository.GetModalityById(item.ModalityId);

                    jobList.Add(new JobResponseDTO
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

                return _responseBuilder.OK(jobList, "Vagas encontradas com sucesso!");
            }
            catch (Exception ex)
            {
                return _responseBuilder.InternalError($"Ocorreu um erro interno: {ex.Message}");
            }
        }

        public async Task<ResponseDTO> GetAllInterestLevel()
        {
            try
            {
                List<AllInterestLevelResponseDTO> allInterestLevels = new List<AllInterestLevelResponseDTO>();

                var interestLevelList = await _jobRepository.AllInterestLevel();

                if (interestLevelList == null)
                {
                    return _responseBuilder.NotFound("Nenhum dado foi encontrado!");
                }

                foreach (var item in interestLevelList)
                {
                    allInterestLevels.Add(new AllInterestLevelResponseDTO
                    {
                        id = item.Id,
                        name = item.Name
                    });
                }

                return _responseBuilder.OK(allInterestLevels, "Dados encontrados com sucesso!");
            }
            catch (Exception ex)
            {
                return _responseBuilder.InternalError($"Ocorreu um erro interno: {ex.Message}");
            }
        }

        public async Task<ResponseDTO> GetAllTypeOfContract()
        {
            try
            {
                List<AllStatusResponseDTO> allTypeOfContract = new List<AllStatusResponseDTO>();

                var allTypeOfContractList = await _jobRepository.AllTypeOfContract();

                if (allTypeOfContractList == null)
                {
                    return _responseBuilder.NotFound("Nenhum dado foi encontrado!");
                }

                foreach (var item in allTypeOfContractList)
                {
                    allTypeOfContract.Add(new AllStatusResponseDTO
                    {
                        id = item.Id,
                        name = item.Name
                    });
                }

                return _responseBuilder.OK(allTypeOfContract, "Dados encontrados com sucesso!");
            }
            catch (Exception ex)
            {
                return _responseBuilder.InternalError($"Ocorreu um erro interno: {ex.Message}");
            }
        }
    }
}