using Data.Entity.Media;
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
    public class PostMediaConfiquration : IEntityTypeConfiguration<PostMedia>
    {
        public void Configure(EntityTypeBuilder<PostMedia> builder)
        {
            builder.HasOne(c => c.Post)
                .WithMany(c => c.Media)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Url).IsRequired();
            builder.Property(c => c.Type).IsRequired();

        }
    }
}
