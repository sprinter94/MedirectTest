using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medirect.Domain
{
    public class ExchangeRatesHistory
    {
        [Key, Column("PK_Id")]
        public int Id { get; set; }

        [ForeignKey("FK_UserID"), Column("FK_UserID")]
        public int UserId { get; set; }

        public string BaseCurrency { get; set; }
        public decimal BaseAmount { get; set; }
        public string ToCurrency { get; set; }
        public decimal ToAmount { get; set; }
        public decimal ExchangeRate { get; set; }
        public DateTime DateInserted { get; set; }

        public User User { get; set; }
    }
}