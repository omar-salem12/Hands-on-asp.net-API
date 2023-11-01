﻿
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EmployeeManagement.Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RespositoryContext repositoryContext) : base(repositoryContext)
        {
            
        }

        public void CreateCompany(Company company)
        {
            Create(company);
           
        }

        public void DeleteCompany(Company company)
        {
            Delete(company);
        }

        public IEnumerable<Company> GetAllCompanies(bool trackChanges)
        {
                return FindAll(trackChanges).OrderBy(c => c.Name)
                                             .ToList();
        }

        public IEnumerable<Company> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
        {
            return FindByCondition(c => ids.Contains(c.Id), trackChanges);
        }

        public Company? GetCompany(Guid companyId, bool trackChanges)
        {
            return FindByCondition(c => c.Id == companyId, trackChanges).SingleOrDefault();
        } 


    }
}
