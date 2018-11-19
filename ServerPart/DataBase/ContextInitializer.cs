using ServerPart.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerPart.DataBase
{
    class ContextInitializer:CreateDatabaseIfNotExists<Context>
    {
        protected override void Seed(Context context)
        {
            base.Seed(context);
            context.Users.Add(new User() { Nickname = "Vasya123", Password = "123Soft" });
            context.Users.Add(new User() { Nickname = "Petya456", Password = "123Soft" });

        }
    }
}
