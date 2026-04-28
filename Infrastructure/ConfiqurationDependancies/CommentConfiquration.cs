using Data.Entity.Comments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ConfiqurationDependancies
{
    public class CommentConfiquration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c=>c.Id);
            
            builder.Property(c => c.Content)
                .HasMaxLength(100);

            //user 
            builder.HasOne(c=>c.User)
                .WithMany(c=>c.Comments)
                .HasForeignKey(c=>c.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            //post
            builder.HasOne(c=>c.Post)
                .WithMany(c=>c.Comments)
                .HasForeignKey(c=>c.PostId)
                .OnDelete(DeleteBehavior.Cascade);
            //likes 
            builder.HasMany(c => c.Likes)
                .WithOne(c => c.Comment)
                .HasForeignKey(c => c.CommentId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
