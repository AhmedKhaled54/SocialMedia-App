using Data.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ConfiqurationDependancies
{
    public class UserConfiquration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(c => c.Posts)
                .WithOne(c => c.user)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c=>c.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c=>c.UserId)
                .OnDelete(DeleteBehavior.NoAction);// SQl One Cacade Path

            builder.HasMany(c => c.Likes)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UsertId)
                .OnDelete(DeleteBehavior.NoAction);
            
        }
    }
}
