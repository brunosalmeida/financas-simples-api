using System;

namespace FS.Domain.Model
{
    public abstract class Base
    {
        public Base()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.Id = Guid.NewGuid();
        }

        protected Base(Guid id, DateTime createdOn, DateTime? updatedOn)
        {
            Id = id;
            CreatedOn = createdOn;
            UpdatedOn = updatedOn;
        }

        public Guid Id { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime? UpdatedOn { get; private set; }

        public void SetUpdateDate()
        {
            this.UpdatedOn = DateTime.UtcNow;
        }
    }
}