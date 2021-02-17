using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibShare.Api.Data.Entities
{
    [Table("tblAccessProhibited")]
    public class AccessProhibited
    {
        [Key, ForeignKey("User")]
        public string Id { get; set; }
        public virtual DbUser User { get; set; }
        public DateTime? DateDelete { get; set; }
        public string DeletionReason { get; set; }
    }
}
