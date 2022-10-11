using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medirect.Domain
{
    public class User
    {
        [Key, Column("PK_UserId")]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        public ICollection<ExchangeRatesHistory> ExchangeRatesHistory { get; set; }
    }
}