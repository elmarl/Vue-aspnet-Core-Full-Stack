using AspNetCoreVueStarter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using AspNetCoreVueStarter.Models;
using System.Text.Encodings;
using System.Text.Unicode;

namespace AspNetCoreVueStarter.Service
{
    public class EventService : IEventService
    {
        private readonly IDataContext _context;
        public EventService(IDataContext context)
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
            return _context.EventSet.ToList();
        }
        public EventModel GetEvent(int id)
        {
            return _context.EventSet.Find(id);
        }
        // Add an event to the database
        public void AddEvent(EventModel eventModel)
        {
            _context.EventSet.Add(eventModel);
            _context.SaveChanges();
        }
        // Delete an event from the database
        public EventModel DeleteEvent(int id)
        {
            EventModel eventsModel = _context.EventSet.Find(id);
            if (eventsModel == null)
            {
                return null;
            }
            _context.EventSet.Remove(eventsModel);
            _context.SaveChanges();
            return eventsModel;
        }
        // All Participants related actions
        // Get Participants for event by event id
        public List<ParticipantModel> GetParticipants(int id)
        {
            try
            {
                return _context.ParticipantSet.Where(a => a.EventModel.Eventid == id).ToList();
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
                return _context.ParticipantSet.Where(a => a.Participantid == id).First();
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
            if(participantModel.ParticipantType == "person") {
                if (participantModel.Familyname.Length > 0)
                {
                    EventModel eventModel = _context.EventSet.Find(id);
                    if (eventModel == null) { return null; }
                    eventModel.Participants.Add(participantModel);
                    _context.SaveChanges();
                    return participantModel;
                } else
                {
                    return null; // Or throw exception with error details...
                }
            } else if (participantModel.ParticipantType == "company" && participantModel.NumParticipants != null)
            {
                EventModel eventModel = _context.EventSet.Find(id);
                if (eventModel == null) { throw new KeyNotFoundException(string.Format("Event with id: '{0}' was not found", id)); }
                eventModel.Participants.Add(participantModel);
                _context.SaveChanges();
                return participantModel;
            } else
            {
                return null;
            }
        }
        // Update Participant 
        public ParticipantModel UpdateParticipant(int id, ParticipantModel newParticipantModel)
        {
            try
            {
                ParticipantModel originalparticipant = _context.ParticipantSet.FirstOrDefault(o => o.Participantid == id);
                // participant type is not allowed to change on this site
                if(originalparticipant.ParticipantType != newParticipantModel.ParticipantType)
                {
                    throw new Exception("Osaleja tüüp ei saa muutuda");
                }
                // Dont allow family name to be empty on person participants. Only update columns for participant type person. All fields
                // listed below are required by the ParticipantModel except for familyname for person and numparticipants for company. These
                // need to be checked when updating if they are changed to null or not (this is due to having one table containing both types).
                if(newParticipantModel.ParticipantType == "person") {
                    originalparticipant.Firstname = newParticipantModel.Firstname;
                    originalparticipant.Familyname = (newParticipantModel.Familyname != null && newParticipantModel.Familyname.Length > 0) ? newParticipantModel.Familyname : originalparticipant.Familyname;
                    originalparticipant.Idcode = newParticipantModel.Idcode;
                    originalparticipant.DetailsPerson = newParticipantModel.DetailsPerson;
                    _context.SaveChanges();
                    return newParticipantModel;
                } // Dont allow number of participants to be null on company participants. Only update columns for participant type company
                else if(newParticipantModel.ParticipantType == "company") {
                    originalparticipant.Firstname = newParticipantModel.Firstname;
                    originalparticipant.Idcode = newParticipantModel.Idcode;
                    originalparticipant.NumParticipants = (newParticipantModel.NumParticipants != null) ? newParticipantModel.NumParticipants : originalparticipant.NumParticipants;
                    originalparticipant.DetailsCompany = newParticipantModel.DetailsCompany;
                    _context.SaveChanges();
                    return newParticipantModel;
                }
                else
                {
                    return null;
                }
            } catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }
        // Delete Participant
        public ParticipantModel DeleteParticipant(int id)
        {
            ParticipantModel pModel = _context.ParticipantSet.Find(id);
            if (pModel == null)
            {
                return null;
            }
            _context.ParticipantSet.Remove(pModel);
            _context.SaveChanges();
            return pModel;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
