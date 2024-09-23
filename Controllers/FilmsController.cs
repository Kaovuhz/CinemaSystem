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
    public class FilmsController : ControllerBase
    {
        private readonly CinemaContext _context;

        public FilmsController(CinemaContext context)
        {
            _context = context;
        }

        // GET: api/Films
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Film>>> GetFilms(string? title)
        {
            if (_context.Films == null)
            {
                return NotFound();
            }

            IQueryable<Film> films = _context.Films;

            // Kiểm tra nếu có tiêu đề để tìm kiếm
            if (!string.IsNullOrEmpty(title))
            {
                // Use EF.Functions.Like for case-insensitive filtering
                films = films.Where(f => EF.Functions.Like(f.Title, $"%{title}%"));
            }

            return await films.ToListAsync();
        }

        // GET: api/Films/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Film>> GetFilm(int id)
        {
            if (_context.Films == null)
            {
                return NotFound();
            }

            var film = await _context.Films.FindAsync(id);

            if (film == null)
            {
                return NotFound();
            }

            return film;
        }

        // PUT: api/Films/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilm(int id, Film film)
        {
            if (id != film.FilmId)
            {
                return BadRequest();
            }

            _context.Entry(film).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmExists(id))
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

        // POST: api/Films
        [HttpPost]
        public async Task<ActionResult<Film>> PostFilm(Film film)
        {
            if (_context.Films == null)
            {
                return Problem("Entity set 'CinemaContext.Films' is null.");
            }

            // Kiểm tra các trường cần thiết
            if (film.GenreId == 0 || string.IsNullOrEmpty(film.CountryCode))
            {
                return BadRequest(new { message = "GenreId and CountryCode are required." });
            }

            // Loại bỏ navigation properties khỏi POST request
            film.Genre = null;
            film.CountryCodeNavigation = null;

            _context.Films.Add(film);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFilm", new { id = film.FilmId }, film);
        }

        // DELETE: api/Films/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilm(int id)
        {
            if (_context.Films == null)
            {
                return NotFound();
            }

            var film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }

            _context.Films.Remove(film);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FilmExists(int id)
        {
            return (_context.Films?.Any(e => e.FilmId == id)).GetValueOrDefault();
        }
    }
}
