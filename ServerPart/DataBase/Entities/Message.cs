using ServerPart.DataBase.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerPart.DataBase.Entities
{
    [Table("Messages")]
    public class Message : DbEntity
    {
        [StringLength(1000)]
        public string UserMessage { get; set; }
        public bool? isRead { get; set; }
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public virtual User Sender { get; set; }
        public virtual User Recipient { get; set; }
    }
}
