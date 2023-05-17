using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SieteYMediaBack.Models;

namespace SieteYMediaBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardGamesController : ControllerBase
    {
        private readonly SieteYMediaContext _context;

        public CardGamesController(SieteYMediaContext context)
        {
            _context = context;
        }

        // GET: api/CardGames
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardGame>>> GetCardGames()
        {
          if (_context.CardGames == null)
          {
              return NotFound();
          }
            return await _context.CardGames.ToListAsync();
        }

        // GET: api/CardGames/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CardGame>> GetCardGame(decimal id)
        {
          if (_context.CardGames == null)
          {
              return NotFound();
          }
            var cardGame = await _context.CardGames.FindAsync(id);

            if (cardGame == null)
            {
                return NotFound();
            }

            return cardGame;
        }

        // PUT: api/CardGames/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCardGame(decimal id, CardGame cardGame)
        {
            if (id != cardGame.IdGame)
            {
                return BadRequest();
            }

            _context.Entry(cardGame).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardGameExists(id))
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

        // POST: api/CardGames
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CardGame>> PostCardGame(CardGame cardGame)
        {
          if (_context.CardGames == null)
          {
              return Problem("Entity set 'SieteYMediaContext.CardGames'  is null.");
          }
            _context.CardGames.Add(cardGame);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCardGame", new { id = cardGame.IdGame }, cardGame);
        }

        // DELETE: api/CardGames/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCardGame(decimal id)
        {
            if (_context.CardGames == null)
            {
                return NotFound();
            }
            var cardGame = await _context.CardGames.FindAsync(id);
            if (cardGame == null)
            {
                return NotFound();
            }

            _context.CardGames.Remove(cardGame);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CardGameExists(decimal id)
        {
            return (_context.CardGames?.Any(e => e.IdGame == id)).GetValueOrDefault();
        }
    }
}
