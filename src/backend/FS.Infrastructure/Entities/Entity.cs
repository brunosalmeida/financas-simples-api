using System;

namespace FS.Infrastructure
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}