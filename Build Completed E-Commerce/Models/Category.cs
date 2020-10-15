using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Build_Completed_E_Commerce.Models
{
    [Table("Category")]
    public partial class Category
    {
        //public Category()
        //{
        //    InverseParents = new HashSet<Category>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public bool Status { get; set; }

        public virtual Category Parent { get; set; }
        public virtual ICollection<Category> InverseParents { get; set; }
    }
}
