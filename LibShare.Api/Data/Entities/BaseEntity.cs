using System;
using System.ComponentModel.DataAnnotations;

namespace LibShare.Api.Data.Entities
{
    public interface IBaseEntity
    {
        public DateTime DateCreate { get; set; }
        public DateTime? DateModify { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DateDelete { get; set; }
        public string DeletionReason { get; set; }
    }

    public abstract class BaseEntity<T> : IBaseEntity
    {
        [Key]
        public virtual T Id { get; set; }
        public virtual DateTime DateCreate { get; set; }
        public virtual DateTime? DateModify { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual DateTime? DateDelete { get; set; }
        public virtual string DeletionReason { get; set; }

        public BaseEntity()
        {
            DateCreate = DateTime.Now;
            DateModify = null;
            IsDeleted = false;
            DateDelete = null;
            DeletionReason = null;
        }
    }
}
