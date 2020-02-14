using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AspNetCoreVueStarter.Models;

namespace AspNetCoreVueStarter.Data
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<AspNetCoreVueStarter.Models.EventModel> EventModel { get; set; }
    }
}
