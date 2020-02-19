using AspNetCoreVueStarter.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreVueStarter.Data
{
    public interface IDataContext
    {
        DbSet<EventModel> EventModel { get; set; }
        DbSet<ParticipantModel> ParticipantModel { get; set; }
    }
}