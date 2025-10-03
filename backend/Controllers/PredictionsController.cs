using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBackend.Data;
using MyBackend.Models;

namespace MyBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PredictionsControllers : ControllerBase
    {
        private readonly SportsDbContext _context;

        public PredictionsControllers(SportsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prediction>>> GetPredictions()
        {
            return await _context.Predictions.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Prediction>> GetPredictionById(int id)
        {
            var prediction = await _context.Predictions.FindAsync(id);

            if (prediction == null)
            {
                return NotFound();
            }

            return prediction;

        }
    }
}