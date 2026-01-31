using GrpcDemo;

namespace JLokaGrpcService02.Services
{
    public class InvoiceService(ILogger<InvoiceService> logger) : Invoice.InvoiceBase
    {
    }
}
