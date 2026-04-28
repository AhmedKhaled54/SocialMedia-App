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
    public class PrivateMessageConfiquration : IEntityTypeConfiguration<PrivateMessage>
    {
        public void Configure(EntityTypeBuilder<PrivateMessage> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Body)
                .HasMaxLength(300)
                .IsRequired();

            builder.HasOne(c=>c.Sender)
                .WithMany(c=>c.SentMessages)
                .HasForeignKey(c => c.SendId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c=>c.Recived)
                .WithMany(c=>c.ReceivedMessages)
                .HasForeignKey(c=>c.RecivedId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(c => new { c.SendId, c.RecivedId });
            builder.HasIndex(c => c.CreatedAt);
        }
    }
}
