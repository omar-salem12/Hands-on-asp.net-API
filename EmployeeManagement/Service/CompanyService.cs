﻿using AutoMapper;
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

        public async Task<CompanyDto> CreateCompanyAsync(CompanyForCreationDto company)
        {
            var companyEntity = _mapper.Map<Company>(company);
            _repositoryManager.Company.CreateCompany(companyEntity);
           await _repositoryManager.SaveAsync();

            var companyToReturn = _mapper.Map<CompanyDto>(companyEntity);
            return companyToReturn;
        }

        public async Task<(IEnumerable<CompanyDto> companies, string ids)> CreateCompanyCollectionAsync(IEnumerable<CompanyForCreationDto> companyCollection)
        {
            if (companyCollection is null)
                throw new CompanyCollectionBadRequest();

            var companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollection);
            foreach(var company in  companyEntities)
            {
                _repositoryManager.Company.CreateCompany(company);
            }
            await _repositoryManager.SaveAsync();
            var companyCollectionToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
            var ids = string.Join(",", companyCollectionToReturn.Select(c => c.Id));
            return (companies: companyCollectionToReturn, ids: ids);
        }

        public async Task DeleteCompanyAsync(Guid companyId, bool trackchanges)
        {
            var company = await _repositoryManager.Company.GetCompanyAsync(companyId, trackchanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);
            _repositoryManager.Company.DeleteCompany(company);
            await _repositoryManager.SaveAsync();
        }

      

        public async Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync(bool trackChanges)
        {
            
                var companies =await _repositoryManager.Company.GetAllCompaniesAsync(trackChanges);
                var companiesDto = _mapper.Map<IEnumerable< CompanyDto>>(companies);

            //companies.Select(c =>
            // new CompanyDto(c.Id, c.Name ?? "", string.Join(' ', c.Address, c.Country))).ToList();

         
                return companiesDto;
            
           
        }

        public async Task<IEnumerable<CompanyDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();
            var companyEntities =await _repositoryManager.Company.GetByIdsAsync(ids, trackChanges);
            if (ids.Count() != companyEntities.Count())
                throw new CollectionByIdsBadRequestException();
            var companiesToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
            return companiesToReturn;
        }

        public async Task<CompanyDto> GetCompanyAsync(Guid companyId, bool trackChanges)
        {
            var company =await _repositoryManager.Company.GetCompanyAsync(companyId, trackChanges);
            if(company == null)
            {
                throw new CompanyNotFoundException(companyId);
            }
            var companyDto = _mapper.Map<CompanyDto>(company);
            return companyDto;

        }

        public async Task UpdateCompanyAsync(Guid companyId, CompanyForUpdateDto companyForUpdateDto,
                                  bool trackChanges)
        {

            var companyEntity = await _repositoryManager.Company.GetCompanyAsync(companyId, trackChanges);
            if (companyEntity is null)
                throw new CompanyNotFoundException(companyId);
            _mapper.Map(companyForUpdateDto, companyEntity);
          await  _repositoryManager.SaveAsync();
        }
    }
}
