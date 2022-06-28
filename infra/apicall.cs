using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;

namespace ExperianTest.infra
{
    public class apicall
    {
        
        public async void GetCompanySummary(string name)
        {
            try
            {
                string url = string.Format($"https://localhost:5001/CompanyControler/SearchCompany/?companyName={name}");
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                // Pass the handler to httpclient(from you are calling api)
                using (HttpClient client = new HttpClient(clientHandler))
                {

                    client.BaseAddress = new Uri(url);
                    using (HttpResponseMessage response = await client.GetAsync(url))
                    {
                        using (HttpContent content = response.Content)
                        {
                            string mycontent = await content.ReadAsStringAsync();

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
