using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ConfiqurationDependancies
{
    public class LikeConfiquration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.HasKey(x => x.Id);

            //builder.HasOne(c => c.User)
            //    .WithMany(c => c.Likes)
            //    .HasForeignKey(c => c.UsertId)
            //    .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.Comment)
                .WithMany(c=>c.Likes)
                .HasForeignKey(c=>c.CommentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c=>c.Post)
                .WithMany(c=>c.Likes)
                .HasForeignKey(c=>c.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            //constrain Unduplication   => user can like in post only one 
            builder.HasIndex(c => new { c.UsertId, c.PostId })
                .IsUnique()
                .HasFilter("[PostId] IS NOT NULL"); //=> igonore null postid row 

            builder.HasIndex(c => new { c.UsertId, c.CommentId })
                .IsUnique()
                .HasFilter("[CommentId] IS NOT NULL");

            builder.HasIndex(c => c.PostId);
            builder.HasIndex(c => c.LikeType);

            
        }
    }
}
