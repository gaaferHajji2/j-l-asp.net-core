using JLokaTestEFCore.Data;
using JLokaTestEFCore.enums;
using JLokaTestEFCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JLokaTestEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JLokaInvoicesController : ControllerBase
    {
        private readonly InvoiceDbContext _context;

        public JLokaInvoicesController(InvoiceDbContext context)
        {
            _context = context;
        }

        // GET: api/JLokaInvoices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices(
            int page = 1, int pageSize = 10, InvoiceStatus? status = null
        )
        {
            return await _context
                    .Invoices
                    .AsQueryable()
                    .Where(t1 => t1.Status == null || t1.Status == status)
                    .OrderByDescending(x => x.InvoiceDate)
                    .Skip((page -1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }

        // GET: api/JLokaInvoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(Guid id)
        {
            var invoice = await _context.Invoices.FindAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            return invoice;
        }

        // PUT: api/JLokaInvoices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoice(Guid id, Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return BadRequest();
            }

            _context.Entry(invoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var t1 = InvoiceExists(id);

                if (!t1)
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

        // POST: api/JLokaInvoices
        [HttpPost]
        public async Task<ActionResult<Invoice>> PostInvoice(Invoice invoice)
        {
            var t1 = await _context.Invoices.FindAsync(invoice.Id);
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInvoice", new { id = invoice.Id }, invoice);
        }

        // DELETE: api/JLokaInvoices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(Guid id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InvoiceExists(Guid id)
        {
            return (_context.Invoices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
