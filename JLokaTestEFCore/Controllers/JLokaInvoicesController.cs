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
                    .Where(t1 => t1.Status == status)
                    .OrderByDescending(x => x.InvoiceDate)
                    .Skip((page -1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }

        // GET: api/JLokaInvoices/:id
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

        // PUT: api/JLokaInvoices/:id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoice(Guid id, Invoice invoice)
        {

            var t1 = await _context.Invoices.FindAsync(id);

            if (t1 == null)
            {
                return NotFound();
            }

            if (id != invoice.Id)
            {
                return BadRequest();
            }

            t1.Status = invoice.Status;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/JLokaInvoices
        [HttpPost]
        public async Task<ActionResult<Invoice>> PostInvoice(Invoice invoice)
        {
            if(_context.Invoices == null)
            {
                return Problem("Invoices is null");
            }

            var t1 = await _context.Invoices.FindAsync(invoice.Id);

            if(t1 != null)
            {
                return Problem("Id is duplicated", statusCode: 400);
            }

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInvoice", new { id = invoice.Id }, invoice);
        }

        // DELETE: api/JLokaInvoices/:id
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
