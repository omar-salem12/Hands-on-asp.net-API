using AutoMapper;
using Contracts;
using Entities.Models;
using Shard.DataTransferObjects;

namespace EmployeeManagement.Service
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CompanyService(IRepositoryManager repositoryManager,ILoggerManager logger, IMapper mapper)
        {
              _repositoryManager = repositoryManager;
              _logger = logger;
             _mapper = mapper;
        }
        public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
        {
            try
            {
                var companies = _repositoryManager.Company.GetAllCompanies(trackChanges);
                var companiesDto = _mapper.Map<IEnumerable< CompanyDto>>(companies);
                    
                    //companies.Select(c =>
                              // new CompanyDto(c.Id, c.Name ?? "", string.Join(' ', c.Address, c.Country))).ToList();

                return companiesDto;
            }
            catch (Exception ex)
            {
                 _logger.logError($"Something went wrong in the{ nameof(GetAllCompanies)} service method { ex}");
                 throw;
            }
        }
    }
}
