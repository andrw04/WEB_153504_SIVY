using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_153504_SIVY.API.Data;
using WEB_153504_SIVY.API.Services.CarBodyService;
using WEB_153504_SIVY.Domain.Entities;

namespace WEB_153504_SIVY.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarBodyTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICarBodyService _carBodyService;

        public CarBodyTypesController(ApplicationDbContext context, ICarBodyService carBodyService)
        {
            _context = context;
            _carBodyService = carBodyService;
        }

        // GET: api/CarBodyTypes
        /*        [HttpGet]
                public async Task<ActionResult<IEnumerable<CarBodyType>>> GetCarBodyTypes()
                {
                  if (_context.CarBodyTypes == null)
                  {
                      return NotFound();
                  }
                    return await _context.CarBodyTypes.ToListAsync();
                }*/

        [HttpGet]
        public async Task<ActionResult<List<CarBodyType>>> GetCarBodyTypes()
        {
            return Ok(await _carBodyService.GetCarBodyTypeListAsync());
        }

        // GET: api/CarBodyTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarBodyType>> GetCarBodyType(int id)
        {
          if (_context.CarBodyTypes == null)
          {
              return NotFound();
          }
            var carBodyType = await _context.CarBodyTypes.FindAsync(id);

            if (carBodyType == null)
            {
                return NotFound();
            }

            return carBodyType;
        }

        // PUT: api/CarBodyTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarBodyType(int id, CarBodyType carBodyType)
        {
            if (id != carBodyType.Id)
            {
                return BadRequest();
            }

            _context.Entry(carBodyType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarBodyTypeExists(id))
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

        // POST: api/CarBodyTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarBodyType>> PostCarBodyType(CarBodyType carBodyType)
        {
          if (_context.CarBodyTypes == null)
          {
              return Problem("Entity set 'ApplicationDbContext.CarBodyTypes'  is null.");
          }
            _context.CarBodyTypes.Add(carBodyType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarBodyType", new { id = carBodyType.Id }, carBodyType);
        }

        // DELETE: api/CarBodyTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarBodyType(int id)
        {
            if (_context.CarBodyTypes == null)
            {
                return NotFound();
            }
            var carBodyType = await _context.CarBodyTypes.FindAsync(id);
            if (carBodyType == null)
            {
                return NotFound();
            }

            _context.CarBodyTypes.Remove(carBodyType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarBodyTypeExists(int id)
        {
            return (_context.CarBodyTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
