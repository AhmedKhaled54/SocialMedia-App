using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ConfiqurationDependancies
{
    public class NotificationConfiquration : IEntityTypeConfiguration<Notification>
    {   
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            
            builder.HasKey(x => x.Id);

            builder.HasOne(c=>c.Recipient)
                .WithMany(c=>c.Notifications)
                .HasForeignKey(c=>c.RecipientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Sender)
                .WithMany(c => c.SendeNotication)
                .HasForeignKey(c => c.SenderId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
