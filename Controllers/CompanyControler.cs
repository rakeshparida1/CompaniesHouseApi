
using ExperianTest.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using ExperianTest.Services;
using Microsoft.Extensions.Logging;

namespace ExperianTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyControler : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly ICompaniesHouseSearch companiesHouseSearch;
        private readonly ILogger<CompanyControler> _logger;

        public CompanyControler(IConfiguration configuration,
            ICompaniesHouseSearch companiesHouseSearch,
            ILogger<CompanyControler> logger)
        {
            this.configuration = configuration;
            this.companiesHouseSearch = companiesHouseSearch;
            _logger = logger;
        }
        [HttpGet("getstring")]
        public string get()
        {
            return "test";
        }

        [HttpGet("CompanySummary")]
        public async Task<IActionResult> CompanySummary(string companyNumber)
        {
            _logger.LogInformation($"Endpoint CompanySummary Started ");

            var result = await companiesHouseSearch.GetCompanySummary(companyNumber);

            return Ok(result);
        }

        [HttpGet("SearchCompany")]
        public async Task<IActionResult> GetCompany(string companyName)
        {
            _logger.LogInformation($"Endpoint Company Started ");
            var result = await companiesHouseSearch.GetCompany(companyName);
            return Ok(result);

        }

    }
}
