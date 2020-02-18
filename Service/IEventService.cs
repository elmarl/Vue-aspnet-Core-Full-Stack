using AspNetCoreVueStarter.Models;
using System.Collections.Generic;

namespace AspNetCoreVueStarter.Service
{
    public interface IEventService
    {
        void AddEvent(EventsModel eventModel);
        void AddParticipant(int id, ParticipantModel participantModel);
        EventsModel DeleteEvent(int id);
        ParticipantModel DeleteParticipant(int id);
        EventsModel GetEvent(int id);
        List<EventsModel> GetEvents();
        ParticipantModel GetParticipant(int id);
        List<ParticipantModel> GetParticipants(int id);
        void UpdateParticipant(int id, ParticipantModel participantModel);
    }
}