using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace JLokaTestEFCore.Models
{
    public class InvoiceItem
    {
        [JsonIgnore]
        public Guid Id {  get; set; }
        public string Name { get; set; } = String.Empty;
        public string? Description { get; set; }
        [Precision(8, 2)]
        public decimal UnitPrice { get; set; }
        [Precision(8, 2)]
        public decimal Quantity { get; set; }
        [Precision(18, 2)]
        public decimal Amount { get; set; }
        [JsonIgnore]
        public Guid InvoiceId { get; set; }
        [JsonIgnore]
        public Invoice? Invoice { get; set; }
    }
}
