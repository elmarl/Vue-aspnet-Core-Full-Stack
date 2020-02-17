using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspNetCoreVueStarter.Data;
using AspNetCoreVueStarter.Models;
using System.Collections;

namespace AspNetCoreVueStarter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly DataContext _context;

        public EventsController(DataContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventsModel>>> GetEventModel()
        {
            if (_context.EventModel == null)
            {
                string res = "";
                return NotFound();
            }
            return await _context.EventModel.ToListAsync();
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventsModel>> GetEventModel(int id)
        {
            var eventModel = await _context.EventModel.FindAsync(id);
            if (eventModel == null)
            {
                return NotFound();
            }
            return eventModel;
        }
        [HttpPost("{id}/Participants")]
        public async Task<ActionResult<ParticipantModel>> PostParticipantModel(int id, ParticipantModel pModel)
        {
            if(pModel != null) { 
            if (ModelState.IsValid)
            {
                var eventModel = await _context.EventModel.FindAsync(id);
                
                eventModel.Participants.Add(pModel);
                //_context.EventModel.Update(eventModel);
                await _context.SaveChangesAsync();
            }
            return CreatedAtAction("GetEventModel", new { id = pModel.Participantid }, pModel);
            }
            return BadRequest();
        }
        // PUT request to update participant table
        [HttpPut("{id}/Participants/{pid}")]
        public async Task<ActionResult<ParticipantModel>> PutParticipantModel(int pid, ParticipantModel pModel)
        {
            if (pModel != null)
            {
                if (ModelState.IsValid)
                {
                    var originalparticipant = _context.ParticipantModel.FirstOrDefault(o => o.Participantid == pid );
                    originalparticipant.Firstname = pModel.Firstname;
                    originalparticipant.Familyname = pModel.Familyname;
                    originalparticipant.Idcode = pModel.Idcode;
                    originalparticipant.NumParticipants = pModel.NumParticipants;
                    originalparticipant.Details = pModel.Details;
                    //_context.Entry(originalparticipant).CurrentValues.SetValues(pModel);
                    // _context.Entry<ParticipantModel>(pModel).State = EntityState.Modified;
                    _context.SaveChangesAsync();
                }
                return Ok();
            }
            return BadRequest();
        }
        [HttpGet("{id}/Participants")]
        public async Task<ActionResult<IEnumerable<ParticipantModel>>> GetParticipantsModel(int id)
        {
            //var pModel = await _context.ParticipantModel.FindAsync(id);
             // (a => a.Eventid == id);
            if (_context.ParticipantModel == null)
            {
                return NotFound();
            }
            // ASP.NET does not have a synchronization context, no need for configureawait 
            return await _context.ParticipantModel.Where(a => a.EventModel.Eventid == id).ToListAsync(); ;
        }
        [HttpGet("{id}/Participants/{pid}")]
        public async Task<ActionResult<ParticipantModel>> GetParticipantModel(int pid)
        {
            if (_context.ParticipantModel == null)
            {
                return NotFound();
            }
            // ASP.NET does not have a synchronization context, no need for configureawait 
            return await _context.ParticipantModel.Where(a => a.Participantid == pid).FirstAsync();
        }
        // PUT: api/Events/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventModel(int id, EventsModel eventModel)
        {
            if (id != eventModel.Eventid)
            {
                return BadRequest();
            }

            _context.Entry(eventModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Events
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<EventsModel>> PostEventModel(EventsModel eventModel)
        {
            if (ModelState.IsValid) 
            { 
                _context.EventModel.Add(eventModel);
                await _context.SaveChangesAsync();
            }
            return CreatedAtAction("GetEventModel", new { id = eventModel.Eventid }, eventModel);
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EventsModel>> DeleteEventModel(int id)
        {
            var eventModel = await _context.EventModel.FindAsync(id);
            if (eventModel == null)
            {
                return NotFound();
            }

            _context.EventModel.Remove(eventModel);
            await _context.SaveChangesAsync();

            return eventModel;
        }

        private bool EventModelExists(int id)
        {
            return _context.EventModel.Any(e => e.Eventid == id);
        }
    }
}
