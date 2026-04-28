using Data.Entity.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ConfiqurationDependancies
{
    public class PostConfiquration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasOne(c => c.user)
                .WithMany(c => c.Posts)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c=>c.Comments)
                .WithOne(c=>c.Post)
                .HasForeignKey(c=>c.PostId)
                .OnDelete(DeleteBehavior.Cascade);
           
            builder.HasMany(p => p.Media)
                .WithOne(p => p.Post)
                .HasForeignKey(p => p.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p=>p.Likes)
                .WithOne(l=>l.Post)
                .HasForeignKey(p=>p.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(c => c.Caption)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(p => new { p.UserId , p.CreatedAt } )
                .IsDescending(false,true);


        }
    }
}
