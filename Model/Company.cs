using ExperianTest.Model;
using System.Collections.Generic;

namespace ExperianTest.Model
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
     

    public class Item
    {
        public string address_snippet { get; set; }
        public string company_status { get; set; }
        public string title { get; set; }
        public string company_number { get; set; }
        public string snippet { get; set; }
        public string description { get; set; }
        public Matches matches { get; set; }
        public string kind { get; set; }
        public string company_type { get; set; }
        public string date_of_creation { get; set; }
        public List<string> description_identifier { get; set; }
        public Links links { get; set; }
        public CompanyAddress address { get; set; }
        public string date_of_cessation { get; set; }
    }

    //public class Links
    //{
    //    public string self { get; set; }
    //}

    public class Matches
    {
        public List<int> snippet { get; set; }
        public List<int> title { get; set; }
    }

    public class CompanyReceived
    {
        public List<Item> items { get; set; }
    }


}
