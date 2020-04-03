using System;

namespace ConsentPortal.Data
{
    public class Consent
    {
        public int Id { get; set; }
        public string CustomerId { get; set; } = null;
        public bool IsNDPRAccepted { get; set; }
        public DateTime DateCreated { get; set; }
        public string CIF { get; set; }
        public string CustomerName { get; set; }
        public string FilePath { get; set; }
    }
}
