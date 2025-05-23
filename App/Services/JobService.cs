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

        public JobService (IJobRepository jobRepository, IUserRepository userRepository, IResponseBuilder responseBuilder)
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

                Console.WriteLine(modality);
                Console.WriteLine(status);
                Console.WriteLine(user);
                

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
                return _responseBuilder.InternalError( $"Ocorreu um erro interno: {ex.Message}");
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

                return _responseBuilder.OKNoObject("Vaga deletada com sucesso!");
            }
            catch (Exception ex)
            {
                return _responseBuilder.InternalError( $"Ocorreu um erro interno: {ex.Message}");
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
                return _responseBuilder.InternalError( $"Ocorreu um erro interno: {ex.Message}");
            }
        }

        public async Task<ResponseDTO> CreateStatus(TypesDTO typesDTO)
        {
            try
            {
                var statusList = _jobRepository.AllStatus();

                VacancyStatus status = new()
                {
                    Name = typesDTO.name
                };

                await _jobRepository.AddVacacyStatus(status);

                return _responseBuilder.OK(status, "Modalidade criada com sucesso!");
            }
            catch (Exception ex)
            {
                return _responseBuilder.InternalError( $"Ocorreu um erro interno: {ex.Message}");
            }
        }

        public async Task<ResponseDTO> GetAllStatus()
        {
            try
            {
                var status = await _jobRepository.AllStatus();

                if (status == null)
                {
                    return _responseBuilder.NotFound("Nenhum dado foi encontrado!");
                }

                return _responseBuilder.OK(status, "Dados encontrados com sucesso!");
            }
            catch (Exception ex)
            {
                return _responseBuilder.InternalError( $"Ocorreu um erro interno: {ex.Message}");
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
                return _responseBuilder.InternalError( $"Ocorreu um erro interno: {ex.Message}");
            }
        }

        public async Task<ResponseDTO> GetAllModality()
        {
            try
            {
                var modalityList = await _jobRepository.AllModality();

                if (modalityList == null)
                {
                    return _responseBuilder.NotFound("Nenhum dado foi encontrado!");
                }

                return _responseBuilder.OK(modalityList, "Dados encontrados com sucesso!");
            }
            catch (Exception ex)
            {
                return _responseBuilder.InternalError( $"Ocorreu um erro interno: {ex.Message}");
            }
        }
    }
}