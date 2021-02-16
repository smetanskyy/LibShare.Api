using System;
using System.ComponentModel.DataAnnotations;

namespace LibShare.Api.Data.Entities
{
    public abstract class BaseEntity<T>
    {
        [Key]
        public virtual T Id { get; set; }
        public virtual DateTime DateCreate { get; set; }
        public virtual DateTime? DateModify { get; set; }
        public virtual bool IsDelete { get; set; }
        public virtual DateTime? DateDelete { get; set; }
        public virtual string DeletionReason { get; set; }

        public BaseEntity()
        {
            DateCreate = DateTime.Now;
            DateModify = null;
            IsDelete = false;
            DateDelete = null;
            DeletionReason = null;
        }
    }
}
