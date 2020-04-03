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
        public int Id { get; set; }
        public string CIF { get; set; }
        public string Name { get; set; }
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
