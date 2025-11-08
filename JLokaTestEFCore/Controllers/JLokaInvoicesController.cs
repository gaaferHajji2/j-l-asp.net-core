using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JLokaTestEFCore.Data;
using JLokaTestEFCore.Models;

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
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
            return await _context.Invoices.ToListAsync();
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
                var t1 = await InvoiceExists(id);

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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Invoice>> PostInvoice(Invoice invoice)
        {
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

        private async Task<bool> InvoiceExists(Guid id)
        {
            var invoice = await _context.Invoices.FindAsync(id);

            if(invoice == null)
            {
                return false;
            }

            return true;
        }
    }
}
