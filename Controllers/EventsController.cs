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
        public async Task<ActionResult<IEnumerable<EventModel>>> GetEventModel()
        {
            //EventModel ev1 = new EventModel()
            //{
            //    Id = 0,
            //    EventName = "test",
            //    EventDate = DateTime.Now,
            //    Location = "testloc",
            //    Details = "testdet",
            //};
            //EventModel ev2 = new EventModel()
            //{
            //    Id = 1,
            //    EventName = "test",
            //    EventDate = DateTime.Now,
            //    Location = "testloc",
            //    Details = "testdet",
            //};
            //EventModel ev3 = new EventModel()
            //{
            //    Id = 1,
            //    EventName = "testtest",
            //    EventDate = DateTime.Now,
            //    Location = "testloc",
            //    Details = "testdet",
            //};
            //IEnumerable en;
            //List<EventModel> ml = new List<EventModel>();
            //ml.Add(ev1);
            //ml.Add(ev2);
            //ml.Add(ev3);
            if (_context.EventModel == null)
            {
                return NotFound();
            }
            return await _context.EventModel.ToListAsync();
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventModel>> GetEventModel(int id)
        {
            var eventModel = await _context.EventModel.FindAsync(id);
            if (eventModel == null)
            {
                return NotFound();
            }
            return eventModel;
        }
        [HttpPost("{id}/Participants")]
        public async Task<ActionResult<EventModel>> PostParticipantModel(ParticipantModel pModel)
        {
            if (ModelState.IsValid)
            {
               // _context.EventModel.Add(eventModel);
               // await _context.SaveChangesAsync();
            }
            //return CreatedAtAction("GetEventModel", new { id = eventModel.Id }, eventModel);
            return Ok(pModel);
        }

        // PUT: api/Events/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventModel(int id, EventModel eventModel)
        {
            if (id != eventModel.Id)
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
        public async Task<ActionResult<EventModel>> PostEventModel(EventModel eventModel)
        {
            if (ModelState.IsValid) 
            { 
                _context.EventModel.Add(eventModel);
                await _context.SaveChangesAsync();
            }
            return CreatedAtAction("GetEventModel", new { id = eventModel.Id }, eventModel);
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EventModel>> DeleteEventModel(int id)
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
            return _context.EventModel.Any(e => e.Id == id);
        }
    }
}
