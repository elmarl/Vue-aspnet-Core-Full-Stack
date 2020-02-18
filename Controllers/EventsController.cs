using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreVueStarter.Models;
using AspNetCoreVueStarter.Service;

namespace AspNetCoreVueStarter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _service;

        public EventsController(IEventService service)
        {
            _service = service;
        }
        // Get all events
        // GET: api/Events
        [HttpGet]
        public ActionResult<IEnumerable<EventsModel>> GetEventModel()
        {
            return _service.GetEvents();
        }
        // Get specific event by id
        // GET: api/Events/5
        [HttpGet("{id}")]
        public ActionResult<EventsModel> GetEventModel(int id)
        {
            EventsModel item = _service.GetEvent(id);
            if (item != null)
            {
                return _service.GetEvent(id);
            } else
            {
                return NotFound();
            }
        }
        // POST: api/Events
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<EventsModel>> PostEventModel(EventsModel eventModel)
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
        [HttpDelete("{id}")]
        public async Task<ActionResult<EventsModel>> DeleteEventModel(int id)
        {
            EventsModel eventsModel = _service.DeleteEvent(id);
            if (eventsModel == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(eventsModel);
            }
        }
        [HttpPost("{id}/Participants")]
        public ActionResult<ParticipantModel> PostParticipantModel(int id, ParticipantModel pModel)
        {
            if (pModel != null && ModelState.IsValid)
            {
                _service.AddParticipant(id, pModel);
                return CreatedAtAction("GetEventModel", new { id = pModel.Participantid }, pModel);
            } else {
                return BadRequest();
            }
        }
        [HttpGet("{id}/Participants")]
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
        [HttpGet("{id}/Participants/{pid}")]
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
        [HttpPut("{id}/Participants/{pid}")]
        public ActionResult<ParticipantModel> PutParticipantModel(int pid, ParticipantModel pModel)
        {
            if (pModel != null && ModelState.IsValid)
            {
                _service.UpdateParticipant(pid, pModel);
                return Ok(pModel);
            } else
            {
                return BadRequest();
            }
        }
        // DELETE: api/Participants/5
        [HttpDelete("{id}/Participants/{pid}")]
        public async Task<ActionResult<ParticipantModel>> DeleteParticipantModel(int pid)
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
