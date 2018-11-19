using ServerPart.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerPart.DataBase
{
    public class Context : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                        .HasRequired(m => m.Sender)
                        .WithMany(t => t.SentMessages)
                        .HasForeignKey(m => m.SenderId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>()
                        .HasRequired(m => m.Recipient)
                        .WithMany(t => t.ReceivedMessages)
                        .HasForeignKey(m => m.RecipientId)
                        .WillCascadeOnDelete(false);
        }

        public Context() : base()
        {
            Database.SetInitializer<Context>(new ContextInitializer());
        }
    }
}
