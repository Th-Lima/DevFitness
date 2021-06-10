using System;

namespace DevFitness.API.Core.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; private set; }
        
        public DateTime CreateAt { get; private set; }
        
        public bool Active { get; private set; }

        protected BaseEntity()
        {
            CreateAt = DateTime.Now;
            Active = true;
        }

        public void Deactivate()
        {
            Active = false;
        }
    }
}
