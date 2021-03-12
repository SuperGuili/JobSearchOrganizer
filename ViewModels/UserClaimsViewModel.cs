using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobSearchOrganizer.ViewModels
{
    public class UserClaimsViewModel
    {
        public UserClaimsViewModel()
        {
            claims = new List<UserClaim>();
        }

        public string UserId { get; set; }
        public List<UserClaim> claims { get; set; }
    }
}
