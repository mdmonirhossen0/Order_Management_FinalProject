using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OrderManagementAPI.Data;
using OrderManagementClassLibrary.DTOsForAPI;
using OrderManagementClassLibrary.Models;

namespace OrderManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcurementsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProcurementsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Procurements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Procurement>>> GetProcurements()
        {
            return await _context.Procurements.Include(x=>x.ProcurementDetails).ToListAsync();
        }

        // GET: api/Procurements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Procurement>> GetProcurement(int id)
        {
            var procurement = await _context.Procurements.FindAsync(id);

            if (procurement == null)
            {
                return NotFound();
            }

            return procurement;
        }

        // PUT: api/Procurements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProcurement(int id, Procurement procurement)
        {
            if (id != procurement.ProcurementId)
            {
                return BadRequest();
            }

            _context.Entry(procurement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProcurementExists(id))
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

        // POST: api/Procurements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Procurement>> PostProcurement([FromBody] ProcurementDTO pdate)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                Procurement procurement = new Procurement
                {
                    ProcurementDate = pdate.ProDate,
                    RequisitionDate = pdate.ReqDate
                };

                _context.Procurements.Add(procurement);
                await _context.SaveChangesAsync();

                var pid = await _context.Procurements.FirstOrDefaultAsync(x => x.ProcurementDate == pdate.ProDate);
                var exsitReq = await _context.Requisitions.Where(x => x.RequisitionDate == pdate.ReqDate).ToListAsync();

                List<ProcurementDetails> details = JsonConvert.DeserializeObject<List<ProcurementDetails>>(pdate.ProcurementDetails);

                decimal itemtotal = 0;
                List<ProcurementDetails> detailsToAdd = new List<ProcurementDetails>();

                foreach (var item in exsitReq)
                {
                    var matchingDetail = details.FirstOrDefault(pro => pro.ItemId == item.ItemId);

                    if (matchingDetail != null)
                    {
                        ProcurementDetails det = new ProcurementDetails
                        {
                            ItemId = item.ItemId,
                            ProcurementId = pid.ProcurementId,
                            ItemUnitPrice = matchingDetail.ItemUnitPrice,
                            ProcurementQuantity=matchingDetail.ProcurementQuantity,
                            ItemTotalPrice = matchingDetail.ProcurementQuantity * matchingDetail.ItemUnitPrice
                        };

                        itemtotal += det.ItemTotalPrice;
                        detailsToAdd.Add(det);
                    }
                }

                _context.ProcurementDetails.AddRange(detailsToAdd);

                foreach (var item in details)
                {
                    var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.ItemId == item.ItemId);
                    if (stock != null)
                    {
                        stock.Quantity += item.ProcurementQuantity;
                    }
                    else
                    {
                        Stock s = new Stock
                        {
                            ItemId = item.ItemId,
                            Quantity = item.ProcurementQuantity
                        };

                        _context.Stocks.Add(s);
                    }
                }

                await _context.SaveChangesAsync();

                var procurementToUpdate = await _context.Procurements.FirstOrDefaultAsync(x => x.ProcurementId == pid.ProcurementId);
                if (procurementToUpdate != null)
                {
                    procurementToUpdate.Amount = itemtotal;
                    _context.Procurements.Update(procurementToUpdate);
                    await _context.SaveChangesAsync();
                }

                await _context.Database.CommitTransactionAsync();

                return Ok(new { message = "Procurement Posted" });
            }
            catch (Exception ex) 
            {
                await _context.Database.RollbackTransactionAsync();
                return BadRequest(ex.Message); //Return have to change.
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcurement(int id)
        {
            var procurement = await _context.Procurements.FindAsync(id);
            if (procurement == null)
            {
                return NotFound();
            }

            _context.Procurements.Remove(procurement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProcurementExists(int id)
        {
            return _context.Procurements.Any(e => e.ProcurementId == id);
        }
    }
}
