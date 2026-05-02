using Data.Follower;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ConfiqurationDependancies
{
    public class FollowRequestConfiquration : IEntityTypeConfiguration<FollowRequest>
    {
        public void Configure(EntityTypeBuilder<FollowRequest> builder)
        {
          
            builder.HasIndex(c => new { c.SenderId, c.ReceiveId })
                .IsUnique();

            builder.HasOne(c => c.Sender)
                .WithMany(c=>c.SendFollowRequests)
                .HasForeignKey(c => c.SenderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.Receive)
                .WithMany(c=>c.ReceiveFollowRequest)
                .HasForeignKey(c => c.ReceiveId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
