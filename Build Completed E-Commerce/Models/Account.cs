using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Build_Completed_E_Commerce.Models
{
    [Table("Account")]
    public partial class Account
    {
        public Account()
        {
            RoleAccounts = new HashSet<RoleAccount>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<RoleAccount> RoleAccounts { get; set; }
    }
}
