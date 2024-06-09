using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagementAPI.Data;
using OrderManagementClassLibrary.Models;

namespace OrderManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcurementDetailsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProcurementDetailsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ProcurementDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProcurementDetails>>> GetProcurementDetails()
        {
            return await _context.ProcurementDetails.Include(x => x.Item).Include(x => x.Procurement).ToListAsync();
        }

        // GET: api/ProcurementDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProcurementDetails>> GetProcurementDetails(int id)
        {
            var procurementDetails = await _context.ProcurementDetails.FindAsync(id);

            if (procurementDetails == null)
            {
                return NotFound();
            }

            return procurementDetails;
        }

        // PUT: api/ProcurementDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProcurementDetails(int id, ProcurementDetails procurementDetails)
        {
            if (id != procurementDetails.ProcurementDetailsId)
            {
                return BadRequest();
            }

            _context.Entry(procurementDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProcurementDetailsExists(id))
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

        // POST: api/ProcurementDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProcurementDetails>> PostProcurementDetails(ProcurementDetails procurementDetails)
        {
            _context.ProcurementDetails.Add(procurementDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProcurementDetails", new { id = procurementDetails.ProcurementDetailsId }, procurementDetails);
        }

        // DELETE: api/ProcurementDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcurementDetails(int id)
        {
            var procurementDetails = await _context.ProcurementDetails.FindAsync(id);
            if (procurementDetails == null)
            {
                return NotFound();
            }

            _context.ProcurementDetails.Remove(procurementDetails);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProcurementDetailsExists(int id)
        {
            return _context.ProcurementDetails.Any(e => e.ProcurementDetailsId == id);
        }
    }
}
