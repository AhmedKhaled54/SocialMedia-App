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
    public class CommentMediaConfiquration : IEntityTypeConfiguration<CommentMedia>
    {
        public void Configure(EntityTypeBuilder<CommentMedia> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c=>c.Url).IsRequired();
            
            builder.Property(c=>c.Type).IsRequired();

            builder.HasOne(c=>c.Comment)
                .WithMany(c=>c.Media)
                .HasForeignKey(c=>c.CommentId)
                .OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}
