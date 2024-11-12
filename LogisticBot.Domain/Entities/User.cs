using LogisticBot.Domain.Entities.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticBot.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }=string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string TelegramUsername { get; set; } = string.Empty;

        public virtual List<Cargo> Cargos { get; set; }

    }
}
