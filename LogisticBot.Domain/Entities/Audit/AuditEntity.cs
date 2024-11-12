using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticBot.Domain.Entities.Audit
{
    public abstract class AuditEntity
    {
        public DateTime CreateAt { get; set; }=DateTime.UtcNow.AddHours(5);
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow.AddHours(5);
    }
}
