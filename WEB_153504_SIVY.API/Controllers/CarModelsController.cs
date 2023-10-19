using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_153504_SIVY.API.Data;
using WEB_153504_SIVY.API.Services.CarModelService;
using WEB_153504_SIVY.Domain.Entities;
using WEB_153504_SIVY.Domain.Models;

namespace WEB_153504_SIVY.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CarModelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly string _imagesPath;
        private readonly string _appUri;
        private readonly ICarModelService _carModelService;

        public CarModelsController(ApplicationDbContext context,
            IWebHostEnvironment env,
            ICarModelService carModelService,
            IConfiguration config)
        {
            _context = context;
            _carModelService = carModelService;
            _imagesPath = Path.Combine(env.WebRootPath, "Images");
            _appUri = config.GetSection("applicationUrl").Value;

        }

        [HttpGet]
        [HttpGet("{category}")]
        [HttpGet("page{pageNo:int}")]
        [HttpGet("{category}/page{pageNo:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseData<List<CarModel>>>> GetCarModels(string? category, int pageNo = 1, int pageSize = 3)
        {
            return Ok(await _carModelService.GetCarModelListAsync(category, pageNo, pageSize));
        }

        // GET: api/CarModels/5
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<CarModel>> GetCarModel(int id)
        {
          if (_context.CarModels == null)
          {
              return NotFound();
          }
            var carModel = await _context.CarModels.FindAsync(id);

            if (carModel == null)
            {
                return NotFound();
            }

            return carModel;
        }

        // PUT: api/CarModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutCarModel(int id, CarModel carModel)
        {
            if (id != carModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(carModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarModelExists(id))
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

        // POST: api/CarModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarModel>> PostCarModel(CarModel carModel)
        {
          if (_context.CarModels == null)
          {
              return Problem("Entity set 'ApplicationDbContext.CarModels'  is null.");
          }
            _context.CarModels.Add(carModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarModel", new { id = carModel.Id }, carModel);
        }

        // DELETE: api/CarModels/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCarModel(int id)
        {
            if (_context.CarModels == null)
            {
                return NotFound();
            }
            var carModel = await _context.CarModels.FindAsync(id);
            if (carModel == null)
            {
                return NotFound();
            }

            _context.CarModels.Remove(carModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarModelExists(int id)
        {
            return (_context.CarModels?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<ResponseData<string>>> PostImage(int id, IFormFile formFile)
        {
            var response = await _carModelService.SaveImageAsync(id, formFile);
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response);
        }
    }
}
