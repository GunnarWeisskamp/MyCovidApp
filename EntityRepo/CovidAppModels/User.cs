using System;
using System.Collections.Generic;

namespace EntityRepo.CovidAppModels
{
    public class User
    {
        public User()
        {
            UserClaims = new HashSet<UserClaim>();
        }

        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }

        public virtual ICollection<UserClaim> UserClaims { get; set; }
    }
}
