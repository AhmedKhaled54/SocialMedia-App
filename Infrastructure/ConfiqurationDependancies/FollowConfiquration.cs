using Data.Follower;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ConfiqurationDependancies
{
    public class FollowConfiquration : IEntityTypeConfiguration<Follow>
    {
        public void Configure(EntityTypeBuilder<Follow> builder)
        {
            builder.HasOne(c => c.Follower)
                .WithMany(c => c.Following)
                .HasForeignKey(c => c.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);
            // Don't allow deleting a user if they are following someone

            builder.HasOne(c => c.Following)
                .WithMany(c => c.Followers)
                .HasForeignKey(c => c.FollowingId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(c => new { c.FollowerId, c.FollowingId }).IsUnique();
        }
    }
}
