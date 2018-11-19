using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ServerPart.DataBase.Domain.Abstract
{
    public interface IDbEntity
    {
        [Key]
        int Id { get; set; }
    }

}


