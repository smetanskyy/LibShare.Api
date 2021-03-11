using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibShare.Api.Data.Entities
{
    [Table("tblBooks")]
    public class Book : BaseEntity<string>
    {
        /// <summary>
        /// Title of book 
        /// </summary>
        [Required, StringLength(255)]
        public string Title { get; set; }

        /// <summary>
        /// Author of book 
        /// </summary>
        [StringLength(255)]
        public string Author { get; set; }

        /// <summary>
        /// Publisher of book 
        /// </summary>
        [StringLength(255)]
        public string Publisher { get; set; }

        /// <summary>
        /// When book was published 
        /// </summary>
        [StringLength(255)]
        public string Year { get; set; }

        /// <summary>
        /// Lanquage of book 
        /// </summary>
        [StringLength(100)]
        public string Language { get; set; }

        /// <summary>
        /// Description of book 
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Picture of book 
        /// </summary>
        [StringLength(255)]
        public string Image { get; set; }

        public bool IsEbook { get; set; }

        [StringLength(255)]
        public string File { get; set; }

        public int LookedRate { get; set; }

        public virtual string CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public virtual string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual DbUser DbUser { get; set; }
    }
}
