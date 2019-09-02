using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class RoleMaster
    {
        public RoleMaster()
        {
            UserMaster = new HashSet<UserMaster>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool? Status { get; set; }

        public ICollection<UserMaster> UserMaster { get; set; }
    }
}
