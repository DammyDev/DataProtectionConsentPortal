using POSPortal.Data;
using POSPortal.Data.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace POSPortal.Resources
{
    public class SavePOSRequestResource
    {
        [Required]
        public string MerchantName { get; set; }
        [Required]
        public string RCNumber { get; set; }
        [Required]
        public string TerminalLocation { get; set; }
        [Required]
        public int NumberOfTerminals { get; set; }
        [Required]
        public string AccountNumber { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string LGA { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string NearestBusStop { get; set; }
        [Required]
        public string Landmark { get; set; }
        [Required]
        public DateTime BusinessStartDate { get; set; }
        [Required]
        public string TradeName { get; set; }
        public string CorporateAddress { get; set; }
        [Required]
        public string ReceiptHeader { get; set; }
        [Required]
        public string ReceiptFooter { get; set; }
        [Required]
        public string PhoneNo { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [StringRange]
        public string BusinessSegment { get; set; }
        [Required]
        public string PrincipalContactName { get; set; }
        [Required]
        public string PrincipalContactAddress { get; set; }
        [Required]
        public string PrincipalContactEmail { get; set; }
        [Required]
        public string PrincipalContactStateOfOrigin { get; set; }
        [Required]
        public string PrincipalContactPosition { get; set; }
        [Required]
        public string PrincipalContactPhoneNo { get; set; }
        [Required]
        public string ContactNameAtTerminalLocation { get; set; }
        [Required]
        public string ContactAddressAtTerminalLocation { get; set; }
        [Required]
        public string ContactPositionAtTerminalLocation { get; set; }
        [Required]
        public string ContactPhoneNoAtTerminalLocation { get; set; }
        public string BranchCode { get; set; }
        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage = "The Declaration checkbox must be checked.")]
        public bool Declaration { get; set; }
        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage = "The Agreement checkbox must be checked.")]
        public bool Agreement { get; set; }
        [Required]
    
        public EStatus Status { get; set; }
        public DateTime DateSubmitted { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; } = DateTime.Now;
    }
}
