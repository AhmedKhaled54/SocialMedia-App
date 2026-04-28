using Data.Entity;
using Data.Entity.Comments;
using Data.Entity.Posts;
using Data.Entity.Stories;
using Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public  class AppDbContext:IdentityDbContext<User
        ,Role,int,IdentityUserClaim<int>,IdentityUserRole<int>,IdentityUserLogin<int>,
        IdentityRoleClaim<int>,IdentityUserToken<int>>
    {
        public AppDbContext()
        {
             
        }


        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        {
             
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        public DbSet<User> Users { get;set; }
        public DbSet<RefreshToken> RefreshTokens { get;set; }
        public DbSet<Follow> Follow {  get;set; }
        public DbSet<Post> Posts {  get;set; }
        public DbSet<PrivateMessage> PrivateMessages {  get;set; }
        public DbSet<Like> Likes {  get;set; }
        public DbSet<Comment> Comments { get;set; }
        public DbSet<Notification> Notification { get;set; }
        public DbSet<Story> Stories { get;set; }    
        public DbSet<PostMedia> PostMedias { get;set; }
        public DbSet<CommentMedia> CommentMedias { get;set; }

    }
}
