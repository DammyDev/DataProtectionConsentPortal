using System;

namespace POSPortal.Resources
{
    public class LoginResponseResource
    {
        public bool Success { get; set; }
        public string token { get; set; }
        public User user { get; set; }
        public ScopeLevel scopeLevel { get; set; }
    }

    public class User
    {
        public string sAMAccountName { get; set; }
        public string branch { get; set; }
        public string physicalDeliveryOfficeName { get; set; }
        public string department { get; set; }
    }

    public class ScopeLevel
    {
        public string branchcode { get; set; }
    }
}
