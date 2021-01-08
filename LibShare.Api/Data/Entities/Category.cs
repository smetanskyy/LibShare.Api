using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibShare.Api.Data.Entities
{
    [Table("tblCategories")]
    public class Category : BaseEntity<long>
    {
        /// <summary>
        /// Name of category
        /// </summary>
        [Required, StringLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// Picture of category 
        /// </summary>
        [StringLength(255)]
        public string Image { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        [ForeignKey("Parent")]
        public virtual long? ParentId { get; set; }
        public virtual Category Parent { get; set; }
    }
}
