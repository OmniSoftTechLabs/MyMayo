using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class UserMaster
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool? Status { get; set; }
        public int? RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Image { get; set; }
        public string Qualification { get; set; }
        public decimal? Rating { get; set; }

        public RoleMaster Role { get; set; }
    }
}
