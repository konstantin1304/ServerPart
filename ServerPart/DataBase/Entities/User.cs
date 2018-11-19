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
    [Table("Users")]
    public class User : DbEntity
    {
        [StringLength(64)]
        [Index(IsUnique = true)]
        public string Nickname { get; set; }

        [StringLength(64)]
        public string Password { get; set; }

        public virtual List<Message> SentMessages { get; set; }
        public virtual List<Message> ReceivedMessages { get; set; }
    }

}
