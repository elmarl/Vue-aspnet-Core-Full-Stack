using AspNetCoreVueStarter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreVueStarter.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreVueStarter.Service
{
    public class ServiceLayer
    {
        private readonly DataContext _context;
        public ServiceLayer(DataContext context)
        {
            _context = context;
            if(_context != null)
            {
                _context.Database.EnsureCreated();
            }
        }
        // All Event table related actions
        // Get all the events, if no events in the database return an empty array
        public List<EventsModel> getEvents()
        {
            if (_context.EventModel != null) { 
                return _context.EventModel.ToList(); 
            } else
            {
                return new List<EventsModel>();
            }
            
        }
        public EventsModel GetEvent(int id)
        {
            return _context.EventModel.Find(id);
        }
        public void AddEvent(EventsModel eventModel)
        {
            _context.EventModel.Add(eventModel);
            _context.SaveChanges();
        }
        public EventsModel DeleteEvent(int id)
        {
            EventsModel eventsModel = _context.EventModel.Find(id);
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
            return _context.ParticipantModel.Where(a => a.EventModel.Eventid == id).ToList();
        }
        // Get specific participant
        public ParticipantModel GetParticipant(int id)
        {
            return _context.ParticipantModel.Where(a => a.Participantid == id).First();
        }
        // Add participant by adding it to the parent table, entity framework
        public void AddParticipant(int id, ParticipantModel participantModel)
        {
            EventsModel eventModel = _context.EventModel.Find(id);
            eventModel.Participants.Add(participantModel);
            _context.SaveChanges();
        }
        // Update Participant 
        public void UpdateParticipant(int id, ParticipantModel participantModel)
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
            _context.SaveChangesAsync();
            return pModel;
        }
    }
}
