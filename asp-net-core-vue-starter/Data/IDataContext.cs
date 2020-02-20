using AspNetCoreVueStarter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AspNetCoreVueStarter.Data
{
    public interface IDataContext
    {
        DbSet<EventModel> EventSet { get; }
        DbSet<ParticipantModel> ParticipantSet { get; }
        DatabaseFacade Database { get; }
        public int SaveChanges();
    }
}