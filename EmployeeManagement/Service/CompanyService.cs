using AutoMapper;
using Contracts;
using Entities.Exceptions;
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

        public CompanyDto CreateCompany(CompanyForCreationDto company)
        {
            var companyEntity = _mapper.Map<Company>(company);
            _repositoryManager.Company.CreateCompany(companyEntity);
            _repositoryManager.Save();

            var companyToReturn = _mapper.Map<CompanyDto>(companyEntity);
            return companyToReturn;
        }

        public (IEnumerable<CompanyDto> companies, string ids) CreateCompanyCollection(IEnumerable<CompanyForCreationDto> companyCollection)
        {
            if (companyCollection is null)
                throw new CompanyCollectionBadRequest();

            var companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollection);
            foreach(var company in  companyEntities)
            {
                _repositoryManager.Company.CreateCompany(company);
            }
            _repositoryManager.Save();
            var companyCollectionToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
            var ids = string.Join(",", companyCollectionToReturn.Select(c => c.Id));
            return (companies: companyCollectionToReturn, ids: ids);
        }

        public void DeleteCompany(Guid companyId, bool trackchanges)
        {
            var company = _repositoryManager.Company.GetCompany(companyId, trackchanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);
            _repositoryManager.Company.DeleteCompany(company);
            _repositoryManager.Save();
        }

        public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
        {
            
                var companies = _repositoryManager.Company.GetAllCompanies(trackChanges);
                var companiesDto = _mapper.Map<IEnumerable< CompanyDto>>(companies);

            //companies.Select(c =>
            // new CompanyDto(c.Id, c.Name ?? "", string.Join(' ', c.Address, c.Country))).ToList();

         
                return companiesDto;
            
           
        }

        public IEnumerable<CompanyDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();
            var companyEntities = _repositoryManager.Company.GetByIds(ids, trackChanges);
            if (ids.Count() != companyEntities.Count())
                throw new CollectionByIdsBadRequestException();
            var companiesToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
            return companiesToReturn;
        }

        public CompanyDto GetCompany(Guid companyId, bool trackChanges)
        {
            var company = _repositoryManager.Company.GetCompany(companyId, trackChanges);
            if(company == null)
            {
                throw new CompanyNotFoundException(companyId);
            }
            var companyDto = _mapper.Map<CompanyDto>(company);
            return companyDto;

        }

        public void UpdateCompany(Guid companyId, CompanyForUpdateDto companyForUpdateDto,
                                  bool trackChanges)
        {

            var companyEntity = _repositoryManager.Company.GetCompany(companyId, trackChanges);
            if (companyEntity is null)
                throw new CompanyNotFoundException(companyId);
            _mapper.Map(companyForUpdateDto, companyEntity);
            _repositoryManager.Save();
        }
    }
}
