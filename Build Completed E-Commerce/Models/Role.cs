using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Build_Completed_E_Commerce.Models
{
    [Table("Role")]
    public partial class Role
    {
        public Role()
        {
            RoleAccounts = new HashSet<RoleAccount>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<RoleAccount> RoleAccounts { get; set; }
    }
}
