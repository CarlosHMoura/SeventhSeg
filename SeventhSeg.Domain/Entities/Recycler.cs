using SeventhSeg.Domain.Enums;
using SeventhSeg.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeventhSeg.Domain.Entities
{
    public class Recycler : Entity
    {
        public RecyclerStatusEnum Status { get; private set; }
        public int Days { get; set; }

        public Recycler(RecyclerStatusEnum status, int days)
        {
            DomainExceptionValidation.When(days <= 0, "Invalid days value");

            Id = Guid.NewGuid();
            Status = status;
            Days = days;
        }
    }
}
