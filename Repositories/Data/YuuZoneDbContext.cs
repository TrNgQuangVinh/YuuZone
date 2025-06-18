using Microsoft.EntityFrameworkCore;
using Repositories.Data.Entities;
using Repository.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Data
{
    public class YuuZoneDbContext : DbContext
    {
        public YuuZoneDbContext(DbContextOptions options) : base(options)
        {
        }

        public YuuZoneDbContext()
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Community> Communities { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seeding Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "Customer" }
            );

            // Seeding Genders
            modelBuilder.Entity<Gender>().HasData(
                new Gender { Id = 1, GenderTitle = "Male" },
                new Gender { Id = 2, GenderTitle = "Female" },
                new Gender { Id = 3, GenderTitle = "Other" }
            );

            // Seeding Statuses
            modelBuilder.Entity<Status>().HasData(
                new Status { Id = 1, StatusName = "Active" },
                new Status { Id = 2, StatusName = "Inactive" },
                new Status { Id = 3, StatusName = "Pending" }
            );

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(u => u.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Gender)
                .WithMany(u => u.Users)
                .HasForeignKey(u => u.GenderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Author)
                .WithMany(p => p.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Author)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Community>()
                .HasMany(c => c.Posts)
                .WithOne(c => c.Community)
                .HasForeignKey(c => c.CommunityId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Community>()
                .HasMany(c => c.Tags)
                .WithMany(c => c.Communities);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Status)
                .WithMany()
                .HasForeignKey(u => u.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Status)
                .WithMany()
                .HasForeignKey(p => p.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Community>()
                .HasOne(c => c.Status)
                .WithMany()
                .HasForeignKey(c => c.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Community>()
                .HasMany(c => c.Members)
                .WithMany(c => c.Communities)
                .UsingEntity<Dictionary<string, object>>("CommunityMember",
                    j => j
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade),
                    j => j
                    .HasOne<Community>()
                    .WithMany()
                    .HasForeignKey("CommunityId")
                    .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey("CommunityId", "UserId");
                        j.ToTable("CommunityMembers");
                    });

            modelBuilder.Entity<User>()
                .HasMany(u => u.Followers)
                .WithMany(u => u.Following)
                .UsingEntity<Dictionary<string, object>>("UserFollow",
                    j => j
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey("FollowerId")
                    .OnDelete(DeleteBehavior.Restrict),
                    j => j
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey("FollowingId")
                    .OnDelete(DeleteBehavior.Restrict),
                    j =>
                    {
                        j.HasKey("FollowerId", "FollowingId");
                        j.ToTable("UserFollows");
                    });

            modelBuilder.Entity<PostVote>()
                .HasKey(v => new { v.UserId, v.PostId });

            modelBuilder.Entity<PostVote>()
                .HasOne(v => v.Voters)
                .WithMany(v => v.PostVote)
                .HasForeignKey(pv => pv.UserId);

            modelBuilder.Entity<PostVote>()
                .HasOne(v => v.Voted)
                .WithMany(v => v.PostVote)
                .HasForeignKey(pv => pv.PostId);
            
            modelBuilder.Entity<CommentVote>()
                .HasKey(v => new { v.UserId, v.CommentId });

            modelBuilder.Entity<CommentVote>()
                .HasOne(v => v.Voters)
                .WithMany(v => v.CommentVote)
                .HasForeignKey(pv => pv.UserId);

            modelBuilder.Entity<CommentVote>()
                .HasOne(v => v.Voted)
                .WithMany(v => v.CommentVote)
                .HasForeignKey(pv => pv.CommentId);
        }
    }
}