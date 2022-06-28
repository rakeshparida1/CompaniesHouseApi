
using ExperianTest.Model;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ExperianTest.Services
{
    public class CompaniesHouseSearch : ICompaniesHouseSearch
    {
        private readonly AppSetting _appSettings;
        private readonly ILogger<CompaniesHouseSearch> _logger;
        public CompaniesHouseSearch(
            IOptions<AppSetting> app,
            ILogger<CompaniesHouseSearch> logger)
        {
            this._appSettings = app.Value;
            _logger = logger;
        }


        public async Task<List<CompaniesList>> GetCompany(string companyName)
        {
            try
            {
                _logger.LogInformation($"Get Company method called with parameter {companyName}");

                List<CompaniesList> listCompanyResponse = new List<CompaniesList>();
                string url = string.Format("https://api.company-information.service.gov.uk/search/companies/?q={0}", companyName);
                CompanyReceived company = new CompanyReceived();

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
                    client.DefaultRequestHeaders.Add("Authorization", _appSettings.apiKey);
                    client.BaseAddress = new Uri(url);
                    using (HttpResponseMessage response = await client.GetAsync(url))
                    {
                        _logger.LogInformation($"Company house api is calling");

                        using (HttpContent content = response.Content)
                        {
                            _logger.LogInformation($"Company house api is called");

                            string mycontent = await content.ReadAsStringAsync();
                            _logger.LogInformation($"Company house api data has received");

                            company = JsonConvert.DeserializeObject<CompanyReceived>(content.ReadAsStringAsync().Result);
                            _logger.LogInformation($"Company house api data has DeserializeObject");

                        }
                    }
                    if (company.items.Any())
                    {
                        foreach (var item in company.items)
                        {
                            CompaniesList companyList = new CompaniesList();
                            companyList.CompanyName = item.title;
                            companyList.CompanyNumber = item.company_number;
                            companyList.CompanyStatus = item.company_status;
                            companyList.Address = item.address;
                            listCompanyResponse.Add(companyList);
                        }
                        return listCompanyResponse;
                    }
                    else
                        return null;

                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Api exception occur {ex.InnerException}");
                throw ex;
            }
        }

        public async Task<CompanySummaryResponse> GetCompanySummary(string companyNumber)
        {
            try
            {
                _logger.LogInformation($"Get Company method called with parameter {companyNumber}");
                string url = string.Format("https://api.company-information.service.gov.uk/company/{0}", companyNumber);
                CompanySummaryResponse objCompanyResponse = new CompanySummaryResponse();
                using (HttpClient client = new HttpClient())
                {
                    CompanySummaryReceived companySummaryReceived = new CompanySummaryReceived();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
                    client.DefaultRequestHeaders.Add("Authorization", _appSettings.apiKey);

                    client.BaseAddress = new Uri(url);
                    using (HttpResponseMessage response = await client.GetAsync(url))
                    {
                        _logger.LogInformation($"Company house api is calling");
                        using (HttpContent content = response.Content)
                        {
                            _logger.LogInformation($"Company house api is called");
                            //CompanySummary mycontent = await content.ReadAsStringAsync<CompanySummary>();
                            companySummaryReceived = JsonConvert.DeserializeObject<CompanySummaryReceived>(content.ReadAsStringAsync().Result);
                            _logger.LogInformation($"Company house api data has DeserializeObject");

                        }
                    }
                    if (!string.IsNullOrEmpty(companySummaryReceived.company_name) &&
                        !string.IsNullOrEmpty(companySummaryReceived.company_number)&&
                        !string.IsNullOrEmpty(companySummaryReceived.company_status))
                    {
                        objCompanyResponse.companyName = companySummaryReceived.company_name;
                        objCompanyResponse.companyNumber = companySummaryReceived.company_number;
                        objCompanyResponse.companyStatus = companySummaryReceived.company_status;
                        objCompanyResponse.registered_office_address = new CompanyAddress();
                        objCompanyResponse.registered_office_address.AddressLine1 = companySummaryReceived.registered_office_address.address_line_1;
                        objCompanyResponse.registered_office_address.Country = companySummaryReceived.registered_office_address.country;
                        objCompanyResponse.registered_office_address.PostalCode = companySummaryReceived.registered_office_address.postal_code;
                        objCompanyResponse.registered_office_address.Locality = companySummaryReceived.registered_office_address.locality;
                        objCompanyResponse.accounts = new Accounts();
                        objCompanyResponse.accounts.overdue = companySummaryReceived.accounts.overdue;
                        objCompanyResponse.accounts.due_on = companySummaryReceived.accounts.due_on;
                        objCompanyResponse.dateOfCreation = companySummaryReceived.date_of_creation;
                        objCompanyResponse.SIC_Codes = companySummaryReceived.sic_codes;

                    }
                }


                return objCompanyResponse;

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"api exception occur {ex.Message}");
                throw ex;
            }
        }

    }
}