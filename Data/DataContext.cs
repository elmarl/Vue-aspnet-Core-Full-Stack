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

        public DbSet<EventModel> EventModel { get; set; }
        public DbSet<ParticipantModel> ParticipantModel { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
