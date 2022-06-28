using Newtonsoft.Json;
using System.Collections.Generic;

namespace ExperianTest.Model
{
    public class CompaniesList
    {
        public string CompanyName { get; set; }
        public string CompanyNumber { get; set; }
        public string CompanyStatus { get; set; }
        public CompanyAddress Address { get; set; }

    }

    public class CompanySummaryResponse
    {
        public string companyName { get; set; }

        public string companyNumber { get; set; }

        public string companyStatus { get; set; }
        public CompanyAddress registered_office_address { get; set; }
        public List<string> SIC_Codes { get; set; }

        public Accounts accounts { get; set; }
        public string dateOfCreation { get; set; }

    }


    public class CompanyAddress
    {
        [JsonProperty(PropertyName = "address_line_1")]
        public string AddressLine1
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "address_line_2")]
        public string AddressLine2
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "care_of")]
        public string CareOf
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "country")]
        public string Country
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "locality")]
        public string Locality
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "po_box")]
        public string PoBox
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "postal_code")]
        public string PostalCode
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "Premises")]
        public string Premises
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "region")]
        public string Region
        {
            get;
            set;
        }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class AccountingReferenceDate
    {
        public string month { get; set; }
        public string day { get; set; }
    }

    public class Accounts
    {
        public string overdue { get; set; }
        public string due_on { get; set; }

        public LastAccounts last_accounts { get; set; }
        public AccountingReferenceDate accounting_reference_date { get; set; }
    }

    public class LastAccounts
    {
        public string type { get; set; }
        public string period_end_on { get; set; }
        public string made_up_to { get; set; }
    }

    public class Links
    {
        public string self { get; set; }
        public string filing_history { get; set; }
        public string officers { get; set; }
        public string persons_with_significant_control { get; set; }
    }

    public class RegisteredOfficeAddress
    {
        public string postal_code { get; set; }
        public string address_line_1 { get; set; }
        public string country { get; set; }
        public string locality { get; set; }
    }

    public class CompanySummaryReceived
    {
        public List<string> sic_codes { get; set; }
        public RegisteredOfficeAddress registered_office_address { get; set; }
        public string jurisdiction { get; set; }
        public string last_full_members_list_date { get; set; }
        public bool has_been_liquidated { get; set; }
        public bool undeliverable_registered_office_address { get; set; }
        public string company_number { get; set; }
        public string company_name { get; set; }
        public Accounts accounts { get; set; }
        public string date_of_creation { get; set; }
        public string type { get; set; }
        public string etag { get; set; }
        public bool has_insolvency_history { get; set; }
        public string company_status { get; set; }
        public bool has_charges { get; set; }
        public Links links { get; set; }
        public bool registered_office_is_in_dispute { get; set; }
        public string date_of_cessation { get; set; }
        public bool can_file { get; set; }
    }





}
