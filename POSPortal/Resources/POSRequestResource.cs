using System;

namespace POSPortal.Resources
{
    public class POSRequestResource
    {

        public string RequestId { get; set; }
        public string MerchantName { get; set; }
        public string RCNumber { get; set; }
        public string TerminalLocation { get; set; }
        public int NumberOfTerminals { get; set; }
        public string AccountNumber { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }      
        public DateTime DateSubmitted { get; set; }      
        public DateTime DateUpdated { get; set; }      

    }
}
