using JLokaTestEFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace JLokaTestEFCore.Data
{
    public class InvoiceDbContext(DbContextOptions<InvoiceDbContext> options): DbContext(options)
    {
        public DbSet<Invoice> Invoices => Set<Invoice>();
    }
}
