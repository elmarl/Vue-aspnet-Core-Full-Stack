using AspNetCoreVueStarter.Models;
using System.Collections.Generic;

namespace AspNetCoreVueStarter.Service
{
    public interface IEventService
    {
        void AddEvent(EventsModel eventModel);
        ParticipantModel AddParticipant(int id, ParticipantModel participantModel);
        EventsModel DeleteEvent(int id);
        ParticipantModel DeleteParticipant(int id);
        EventsModel GetEvent(int id);
        List<EventsModel> GetEvents();
        ParticipantModel GetParticipant(int id);
        List<ParticipantModel> GetParticipants(int id);
        ParticipantModel UpdateParticipant(int id, ParticipantModel participantModel);
    }
}