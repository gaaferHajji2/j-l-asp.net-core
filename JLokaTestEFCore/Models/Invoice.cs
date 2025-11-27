using JLokaTestEFCore.enums;
using System.Text.Json.Serialization;

namespace JLokaTestEFCore.Models
{
    // [Table("Invoices")]
    public class Invoice
    {
        //[Column("Id")]
        //[Key]
        [JsonIgnore]
        public Guid Id { get; set; }
        //[Column(name: "InvoiceNumber", TypeName ="varchar(32)")]
        //[Required]
        public string InvoiceNumber { get; set; } = string.Empty;
        //[Column(name: "ContactName")]
        //[Required]
        //[MaxLength(32)]
        public string ContactName {  get; set; } = string.Empty;
        //[Column(name: "Description")]
        //[MaxLength(256)]
        public string? Description {  get; set; }
        //[Column("Amount")]
        //[Precision(18, 2)]
        //[Range(0, 999999999999.99)]
        public decimal Amount { get; set; }
        //[Column(name: "InvoiceDate", TypeName = "datetimeoffset")]
        public DateTimeOffset InvoiceDate { get; set; }
        //[Column(name: "DueDate", TypeName = "datetimeoffset")]
        public DateTimeOffset DueDate { get; set; }
        //[Column(name: "Status", TypeName = "varchar(16)")]
        public InvoiceStatus Status { get; set; }
        //[JsonIgnore]
        public List<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
    }
}
