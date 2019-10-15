using Detector.Database.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Detector.Database
{
    public class DetectorContext : DbContext
    {
        public DbSet<IntrudeRecordEntity> IntrudeRecords { get; set; }

        public DetectorContext(DbContextOptions<DetectorContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IntrudeRecordEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<IntrudeRecordEntity>().Property(x => x.IntrudeTime).HasDefaultValueSql("getutcdate()");
        }
    }
}
