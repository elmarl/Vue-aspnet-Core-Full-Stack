using AspNetCoreVueStarter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using AspNetCoreVueStarter.Models;

namespace AspNetCoreVueStarter.Service
{
    public class EventService : IEventService
    {
        private readonly DataContext _context;
        public EventService(DataContext context)
        {
            _context = context;
            if (_context != null)
            {
                _context.Database.EnsureCreated();
            }
        }
        // All Event table related actions
        // Get all the events, if no events in the database return an empty array
        public List<EventModel> GetEvents()
        {
            if (_context.EventModel != null)
            {
                return _context.EventModel.ToList();
            }
            else
            {
                return new List<EventModel>();
            }
        }
        public EventModel GetEvent(int id)
        {
            return _context.EventModel.Find(id);
        }
        public void AddEvent(EventModel eventModel)
        {
            _context.EventModel.Add(eventModel);
            _context.SaveChanges();
        }
        public EventModel DeleteEvent(int id)
        {
            EventModel eventsModel = _context.EventModel.Find(id);
            if (eventsModel == null)
            {
                return null;
            }
            _context.EventModel.Remove(eventsModel);
            _context.SaveChanges();
            return eventsModel;
        }

        // All Participants related actions
        // Get Participants for event by event id
        public List<ParticipantModel> GetParticipants(int id)
        {
            try
            {
                return _context.ParticipantModel.Where(a => a.EventModel.Eventid == id).ToList();
            }
            catch (ArgumentNullException ex)
            {
                return null;
            }
        }
        // Get specific participant
        public ParticipantModel GetParticipant(int id)
        {
            try
            {
                return _context.ParticipantModel.Where(a => a.Participantid == id).First();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        // Add participant by adding it to the parent table, entity framework
        // Because only one table is used to store both companies and persons, validate for persons if
        // max details length has been surpassed or not. Validation for companies <5000 is already done
        // by the model.
        public ParticipantModel AddParticipant(int id, ParticipantModel participantModel)
        {
            if(participantModel.ParticipantType == "person" && participantModel.Familyname.Length>0) {
                if (participantModel.Details.Length < 1500)
                {
                    EventModel eventModel = _context.EventModel.Find(id);
                    eventModel.Participants.Add(participantModel);
                    _context.SaveChanges();
                    return participantModel;
                } else
                {
                    return null;
                }
            } else if (participantModel.ParticipantType == "company")
            {
                EventModel eventModel = _context.EventModel.Find(id);
                eventModel.Participants.Add(participantModel);
                _context.SaveChanges();
                return participantModel;
            } else
            {
                return null;
            }
        }
        // Update Participant 
        public ParticipantModel UpdateParticipant(int id, ParticipantModel participantModel)
        {
            try
            {
                ParticipantModel originalparticipant = _context.ParticipantModel.FirstOrDefault(o => o.Participantid == id);
                originalparticipant.Firstname = participantModel.Firstname;
                originalparticipant.Familyname = participantModel.Familyname;
                originalparticipant.Idcode = participantModel.Idcode;
                originalparticipant.NumParticipants = participantModel.NumParticipants;
                originalparticipant.Details = participantModel.Details;
                //_context.Entry(originalparticipant).CurrentValues.SetValues(pModel);
                // _context.Entry<ParticipantModel>(pModel).State = EntityState.Modified;
                _context.SaveChanges();
                return participantModel;
            } catch (Exception ex)
            {
                return null;
            }
        }
        // Delete Participant
        public ParticipantModel DeleteParticipant(int id)
        {
            ParticipantModel pModel = _context.ParticipantModel.Find(id);
            if (pModel == null)
            {
                return null;
            }
            _context.ParticipantModel.Remove(pModel);
            _context.SaveChanges();
            return pModel;
        }
    }
}
