using System;

namespace EntityRepo.CovidAppModels
{
    public class UserClaim
    {
        public Guid UserClaim1 { get; set; }
        public Guid UserIdFk { get; set; }
        public string ClaimType { get; set; }
        public bool ClaimValue { get; set; }

        public virtual User UserIdFkNavigation { get; set; }
    }
}
