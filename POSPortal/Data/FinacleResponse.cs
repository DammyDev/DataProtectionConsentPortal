using System.ComponentModel.DataAnnotations;

namespace POSPortal.Data
{
    public class FinacleResponse
    { 
        public Customer customer { get; set; }
    }   
    
    public class Customer
    {       
        public string accountnumber { get; set; }
        public string account_status { get; set; }
        public string branchcode { get; set; }
        public string cif { get; set; }
        public string currency_code { get; set; }
        public string customer_mobileno { get; set; }
        public string customer_email { get; set; }
    }
}