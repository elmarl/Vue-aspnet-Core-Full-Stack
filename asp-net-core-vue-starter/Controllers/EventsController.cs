using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreVueStarter.Models;
using AspNetCoreVueStarter.Service;

namespace AspNetCoreVueStarter.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        // Dependency injection defined at Startup.cs, injecting event service _service in controller
        private readonly IEventService _service;

        public EventsController(IEventService service)
        {
            _service = service;
        }
        // Get all events
        // GET: api/Events
        [HttpGet]
        public ActionResult<IEnumerable<EventModel>> GetEventModel()
        {
            return _service.GetEvents();
        }
        // Get specific event by id
        // GET: api/Events/5
        [HttpGet("{id}")]
        public ActionResult<EventModel> GetEventModel(int id)
        {
            EventModel item = _service.GetEvent(id);
            if (item != null)
            {
                return _service.GetEvent(id);
            } else
            {
                return NotFound();
            }
        }
        // POST: api/Events
        [HttpPost]
        public ActionResult<EventModel> PostEventModel(EventModel eventModel)
        {
            if (eventModel != null && ModelState.IsValid)
            {
                _service.AddEvent(eventModel);
                return CreatedAtAction("GetEventModel", new { id = eventModel.Eventid }, eventModel);
            }
            else
            {
                return BadRequest();
            }
        }
        // DELETE: api/Events/5
        // delete event by id, return ok result if deleted successfully, otherwise return notfound
        [HttpDelete("{id}")]
        public ActionResult<EventModel> DeleteEventModel(int id)
        {
            EventModel eventsModel = _service.DeleteEvent(id);
            if (eventsModel == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(eventsModel);
            }
        }
        // add new participant 
        [HttpPost("{id}/participants")]
        public ActionResult<ParticipantModel> PostParticipantModel(int id, ParticipantModel pModel)
        {
            if (pModel != null && ModelState.IsValid)
            {
               ParticipantModel pm = _service.AddParticipant(id, pModel);
                if (pm != null)
                {
                    return CreatedAtAction("GetEventModel", new { id = pModel.Participantid }, pModel);
                } else
                {
                    return BadRequest();
                }
            } else {
                return BadRequest();
            }
        }
        [HttpGet("{id}/participants")]
        public ActionResult<IEnumerable<ParticipantModel>> GetParticipantsModel(int id)
        {
            List<ParticipantModel> participants = _service.GetParticipants(id);
            if (participants != null)
            {
                return _service.GetParticipants(id);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("{id}/participants/{pid}")]
        public ActionResult<ParticipantModel> GetParticipantModel(int pid)
        {
            ParticipantModel participant = _service.GetParticipant(pid);
            if (participant != null)
            {
                return participant;
            }
            else
            {
                return NotFound();
            }
        }
        // PUT request to update participant table
        [HttpPut("{id}/participants/{pid}")]
        public ActionResult<ParticipantModel> PutParticipantModel(int pid, ParticipantModel pModel)
        {
            if (pModel != null && ModelState.IsValid)
            {
                ParticipantModel pModelUpdated = _service.UpdateParticipant(pid, pModel);
                if (pModelUpdated!= null)
                {
                    return Ok(pModel);
                } else
                {
                    return BadRequest();
                }
            } else
            {
                return BadRequest();
            }
        }
        // DELETE: api/Participants/5
        [HttpDelete("{id}/participants/{pid}")]
        public ActionResult<ParticipantModel> DeleteParticipantModel(int pid)
        {
            ParticipantModel pModel = _service.DeleteParticipant(pid);
            if (pModel == null)
            {
                return NotFound();
            } else
            {
                return Ok(pModel);
            }
        }
    }
}
