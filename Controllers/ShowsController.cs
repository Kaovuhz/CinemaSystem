using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CinemaSystem_HE161378.Models;

namespace CinemaSystem_HE161378.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        private readonly CinemaContext _context;

        public ShowsController(CinemaContext context)
        {
            _context = context;
        }

        // GET: api/Shows
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Show>>> GetShows(DateTime? date = null, int? roomId = null)
        {
            var query = _context.Shows.AsQueryable();

            if (date.HasValue)
            {
                query = query.Where(s => s.ShowDate.Date == date.Value.Date);
            }

            if (roomId.HasValue)
            {
                query = query.Where(s => s.RoomId == roomId);
            }

            var shows = await query.ToListAsync();

            

            return shows; // Trả về danh sách các buổi chiếu
        }

        // GET: api/Shows/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Show>> GetShow(int id)
        {
          if (_context.Shows == null)
          {
              return NotFound();
          }
            var show = await _context.Shows.FindAsync(id);

            if (show == null)
            {
                return NotFound();
            }

            return show;
        }

        // PUT: api/Shows/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShow(int id, Show show)
        {
            if (id != show.ShowId)
            {
                return BadRequest();
            }

            _context.Entry(show).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShowExists(id))
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

        // POST: api/Shows
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Show>> PostShow(Show show)
        {
          if (_context.Shows == null)
          {
              return Problem("Entity set 'CinemaContext.Shows'  is null.");
          }
            _context.Shows.Add(show);
            show.Film = null;
            show.Room = null;

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShow", new { id = show.ShowId }, show);
        }

        // DELETE: api/Shows/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShow(int id)
        {
            if (_context.Shows == null)
            {
                return NotFound();
            }
            var show = await _context.Shows.FindAsync(id);
            if (show == null)
            {
                return NotFound();
            }

            _context.Shows.Remove(show);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("OccupiedSlots")]
        public async Task<ActionResult<IEnumerable<int>>> GetOccupiedSlots(int filmId, int roomId, DateTime showDate)
        {
            // Lógica để lấy slot đã tồn tại từ cơ sở dữ liệu
            var occupiedSlots = await _context.Shows
                .Where(s => s.FilmId == filmId && s.RoomId == roomId && s.ShowDate.Date == showDate.Date && s.Status == true)
                .Select(s => s.Slot)
                .ToListAsync();
            var availableSlots = Enumerable.Range(0, 10).Except(occupiedSlots).ToList();
 ;           return Ok(availableSlots);
        }


        private bool ShowExists(int id)
        {
            return (_context.Shows?.Any(e => e.ShowId == id)).GetValueOrDefault();
        }
    }
}
