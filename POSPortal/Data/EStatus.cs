using System.ComponentModel;

namespace POSPortal.Data
{
    public enum EStatus : byte
    {
        [Description("Pending Review")]
        Submitted = 1,

        [Description("Pending Approval")]
        Reviewed = 2,        
        
        [Description("Declined")]
        Declined = 3,

        [Description("Pending Deployment")]
        Approved = 4,
        
        [Description("Pending Receipt Acknowledgement")]
        Deployed = 5,

        [Description("Completed")]
        Acknowledged = 6
    }
}
