using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AspNetCoreVueStarter.Models;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreVueStarter.Data
{
    //Database representational model
    public class DataContext : DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
        public DbSet<EventModel> EventSet { get; set; }
        public DbSet<ParticipantModel> ParticipantSet { get; set; }

        int IDataContext.SaveChanges()
        {
            return base.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

    }
}
