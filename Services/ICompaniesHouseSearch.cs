 
using ExperianTest.Model;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExperianTest.Services
{
    public interface ICompaniesHouseSearch
    {
         Task<List<CompaniesList>> GetCompany(string companyName);
        Task<CompanySummaryResponse> GetCompanySummary(string companyNumber);

    }
}
