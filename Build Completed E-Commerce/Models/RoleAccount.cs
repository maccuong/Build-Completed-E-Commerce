using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Build_Completed_E_Commerce.Models
{
    [Table("RoleAccount")]
    public partial class RoleAccount
    {
        public int RoleId { get; set; }
        public int Accountid { get; set; }
        public bool? Status { get; set; }

        public virtual Account Account { get; set; }
        public virtual Role Role { get; set; }
    }
}
