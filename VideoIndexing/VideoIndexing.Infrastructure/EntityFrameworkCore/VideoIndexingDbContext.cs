using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using VideoIndexing.Domain.Entities;
using VideoIndexing.Infrastructure.EntityFrameworkCore.EntityConfigurations;

namespace VideoIndexing.Infrastructure.EntityFrameworkCore {
    public class VideoIndexingDbContext : DbContext {
        public DbSet<FrameHashBase> FrameHashes { get; set; }

        internal static VideoIndexingDbContext Create() {
            return new VideoIndexingDbContext();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new FrameHashEntityTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            //optionsBuilder.UseNpgsql(ConfigurationManager.ConnectionStrings["VideoIndexing_Db_Conn_Str"].ConnectionString);
            //optionsBuilder.UseNpgsql(Configuration.GetConnectionString("VideoIndexing_Db_Conn_Str"));
            optionsBuilder.UseNpgsql("Host=localhost;Database=VideoIndexingDb;Username=postgres;Password=root");
        }
    }
}
