using AspNetCoreVueStarter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using AspNetCoreVueStarter.Models;

namespace AspNetCoreVueStarter.Service
{
    public class EventService : IEventService
    {
        private readonly IDataContext _context;
        public EventService(IDataContext context)
        {
            _context = context;
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
        public void AddEvent(EventModel eventModel)
        {
            _context.EventSet.Add(eventModel);
            _context.SaveChanges();
        }
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
                    eventModel.Participants.Add(participantModel);
                    _context.SaveChanges();
                    return participantModel;
                } else
                {
                    return null;
                }
            } else if (participantModel.ParticipantType == "company" && participantModel.NumParticipants != null)
            {
                EventModel eventModel = _context.EventSet.Find(id);
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
                ParticipantModel originalparticipant = _context.ParticipantSet.FirstOrDefault(o => o.Participantid == id);
                // participant type is not allowed to change on this site
                if(originalparticipant.ParticipantType != participantModel.ParticipantType) { return null; }

                // Dont allow family name to be empty on person participants
                if(participantModel.ParticipantType == "person" && participantModel.Familyname != null && participantModel.Familyname.Length>0) {
                    originalparticipant.Firstname = participantModel.Firstname;
                    originalparticipant.Familyname = participantModel.Familyname;
                    originalparticipant.Idcode = participantModel.Idcode;
                    originalparticipant.NumParticipants = participantModel.NumParticipants;
                    originalparticipant.DetailsPerson = participantModel.DetailsPerson;
                    _context.SaveChanges();
                    return participantModel;
                } // Dont allow number of participants to be null on company participants
                else if(participantModel.ParticipantType == "company" && participantModel.NumParticipants != null) {
                    originalparticipant.Firstname = participantModel.Firstname;
                    originalparticipant.Familyname = participantModel.Familyname;
                    originalparticipant.Idcode = participantModel.Idcode;
                    originalparticipant.NumParticipants = participantModel.NumParticipants;
                    originalparticipant.DetailsCompany = participantModel.DetailsCompany;
                    _context.SaveChanges();
                    return participantModel;
                }
                else
                {
                    return null;
                }
            } catch (Exception ex)
            {
                return null;
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
