using AspNetCoreVueStarter.Models;
using System.Collections.Generic;

namespace AspNetCoreVueStarter.Service
{
    public interface IEventService
    {
        void AddEvent(EventModel eventModel);
        ParticipantModel AddParticipant(int id, ParticipantModel participantModel);
        EventModel DeleteEvent(int id);
        ParticipantModel DeleteParticipant(int id);
        EventModel GetEvent(int id);
        List<EventModel> GetEvents();
        ParticipantModel GetParticipant(int id);
        List<ParticipantModel> GetParticipants(int id);
        ParticipantModel UpdateParticipant(int id, ParticipantModel participantModel);
        void SaveChanges();
    }
}