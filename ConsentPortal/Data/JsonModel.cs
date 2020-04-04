using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace ConsentPortal.Data
{
    public class Request
    {
        public string AccountNumber { get; set; }
    }    
    
    public class FinacleResponse
    {
        public string cif_id { get; set; }
        public string acctNumber { get; set; }
        public string acctName { get; set; }
        public string dormancy { get; set; }
        public string schmcode { get; set; }
        public string schmtype { get; set; }
        public string currycode { get; set; }
        public string branchcode { get; set; }
        public double ledgerbal { get; set; }
        public double availbal { get; set; }
        public double effavlbal { get; set; }
        public double lienamt { get; set; }
        public double drwpwrAmt { get; set; }
        public double accbal { get; set; }
        public double shadowbal { get; set; }
        public double futBal { get; set; }
    }

    public class UploadModel
    {
        [Required]
        public bool IsConsentReceived { get; set; } = false;
        [Required]
        public bool IsSignatureVerified { get; set; } = false;
        [Required]
        public ElementReference UploadedFile { get; set; }
    }
}
